using Xunit;

namespace Hein.Framework.Messaging.SQS.Tests
{
    public class SendToSQSTests
    {
        [Fact]
        public void Should_place_message_on_queue_successfully()
        {
            var queue = new TestQueue();
            queue.Send("Im a message");
        }
    }
}
