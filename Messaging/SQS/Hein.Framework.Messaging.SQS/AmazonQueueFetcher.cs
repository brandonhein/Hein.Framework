using Amazon;
using Amazon.SQS;

namespace Hein.Framework.Messaging.SQS
{
    public abstract class AmazonQueueFetcher
    {
        private static AmazonSQSClient _client;

        public abstract RegionEndpoint Region { get; }
        public abstract string QueueName { get; }

        protected string QueueUrl { get; private set; }

        protected IAmazonSQS GetSQS()
        {
            if (_client == null)
            {
                var config = new AmazonSQSConfig();
                config.RegionEndpoint = Region;

                _client = new AmazonSQSClient(config);
            }

            if (string.IsNullOrEmpty(QueueUrl))
            {
                var queueInfo = _client.CreateQueueAsync(this.QueueName).Result;
                QueueUrl = queueInfo.QueueUrl;
            }

            return _client;
        }
    }
}
