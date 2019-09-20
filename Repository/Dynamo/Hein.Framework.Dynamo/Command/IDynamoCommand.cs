using Amazon.DynamoDBv2;

namespace Hein.Framework.Dynamo.Command
{
    public interface IDynamoCommand
    {
        AmazonDynamoDBRequest DynamoCommand { get; }
    }
}
