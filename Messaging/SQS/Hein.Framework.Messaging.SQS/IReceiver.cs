using System;
using System.Threading.Tasks;

namespace Hein.Framework.Messaging.SQS
{
    public interface IReceiver : IQueue, IDisposable
    {
        event EventHandler<MessageReceivedEventArgs> MessageReceived;
        Task StartListening();
    }
}
