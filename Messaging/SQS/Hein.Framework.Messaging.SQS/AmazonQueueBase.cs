using Amazon.SQS.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hein.Framework.Messaging.SQS
{
    public abstract class AmazonQueueBase : AmazonQueueFetcher, IAmazonQueue
    {
        private bool _stopListening = false;

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public void Send(string message)
        {
            var queue = base.GetSQS();
            var request = new SendMessageRequest(base.QueueUrl, message);
            queue.SendMessageAsync(request);
        }

        public Task StartListening()
        {
            return Task.Run(() =>
            {
                Listen();
            });
        }

        private void Listen()
        {
            var queue = base.GetSQS();
            while (true)
            {
                var result = queue.ReceiveMessageAsync(base.QueueUrl).Result;
                if (result != null && result.Messages.Any())
                {
                    foreach (var message in result.Messages)
                    {
                        Invoke(new MessageReceivedEventArgs(message.Body));

                        var delete = new DeleteMessageRequest();
                        delete.QueueUrl = base.QueueUrl;
                        delete.ReceiptHandle = message.ReceiptHandle;
                        queue.DeleteMessageAsync(delete);
                    }
                }

                if (_stopListening)
                {
                    break;
                }
            }
        }

        private void Invoke(MessageReceivedEventArgs e)
        {
            MessageReceived.Invoke(this, e);
        }

        public void Dispose()
        {
            _stopListening = true;
            base.GetSQS().Dispose();
        }
    }
}
