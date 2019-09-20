using Amazon.DynamoDBv2;

namespace Hein.Framework.Dynamo.Command
{
    public abstract class DynamoCommandBase : IDynamoCommand
    {
        public abstract AmazonDynamoDBRequest DynamoCommand { get; }
    }
}
