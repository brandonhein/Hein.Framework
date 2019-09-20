using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Hein.Framework.Dynamo.Command;

namespace Hein.Framework.Dynamo.Criterion
{
    public abstract class DynamoQueryCollection : IDynamoCommand
    {
        protected QueryRequest _queryRequest;

        public AmazonDynamoDBRequest DynamoCommand { get { return _queryRequest; } }

        protected void SetupRequest<T>()
        {
            if (_queryRequest == null)
            {
                var table = typeof(T).DynamoTableName();
                var attributeNames = typeof(T).ExpressionAttriubtes();

                _queryRequest = new QueryRequest
                {
                    TableName = table,
                    ProjectionExpression = string.Join(", ", attributeNames.Keys),
                    ExpressionAttributeNames = attributeNames
                };
            }
        }
    }
}
