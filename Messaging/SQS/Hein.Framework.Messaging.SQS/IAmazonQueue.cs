using Amazon;

namespace Hein.Framework.Messaging.SQS
{
    public interface IAmazonQueue : ISender, IReceiver
    {
        RegionEndpoint Region { get; }
    }
}
