using Amazon;

namespace Hein.Framework.Messaging.SQS.Tests
{
    public class TestQueue : AmazonQueueBase
    {
        public override string QueueName => "Test-Queue-Sample"; //SQS Queue Name
        public override RegionEndpoint Region => RegionEndpoint.USEast2; //Region where the SQS queue lives
    }
}
