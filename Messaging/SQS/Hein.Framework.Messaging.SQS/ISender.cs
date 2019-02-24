namespace Hein.Framework.Messaging.SQS
{
    public interface ISender : IQueue
    {
        void Send(string message);
    }
}
