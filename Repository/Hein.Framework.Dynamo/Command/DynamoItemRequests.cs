using Amazon.DynamoDBv2.Model;

namespace Hein.Framework.Dynamo.Command
{
    public static class DynamoItemRequests
    {
        public static Update Convert(this UpdateItemRequest request)
        {
            if (request == null)
                return new Update();

            return new Update
            {
                ConditionExpression = request.ConditionExpression,
                ExpressionAttributeNames = request.ExpressionAttributeNames,
                ExpressionAttributeValues = request.ExpressionAttributeValues,
                Key = request.Key,
                TableName = request.TableName,
                UpdateExpression = request.UpdateExpression
            };
        }

        public static Delete Convert(this DeleteItemRequest request)
        {
            if (request == null)
                return new Delete();

            return new Delete
            {
                ConditionExpression = request.ConditionExpression,
                ExpressionAttributeNames = request.ExpressionAttributeNames,
                ExpressionAttributeValues = request.ExpressionAttributeValues,
                Key = request.Key,
                TableName = request.TableName
            };
        }

        public static Put Convert(this PutItemRequest request)
        {
            if (request == null)
                return new Put();

            return new Put
            {
                ConditionExpression = request.ConditionExpression,
                ExpressionAttributeNames = request.ExpressionAttributeNames,
                ExpressionAttributeValues = request.ExpressionAttributeValues,
                Item = request.Item,
                TableName = request.TableName
            };
        }
    }
}
