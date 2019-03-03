using Hein.Framework.Dynamo.Criterion;
using System.Collections.Generic;
using System.Linq;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace Hein.Framework.Dynamo.Command
{
    public class DynamoTransactionCommand<T> : DynamoCommandBase where T : IRepositoryItem
    {
        private DynamoTransactionType _transactionType;
        private T _item;

        public override AmazonDynamoDBRequest DynamoCommand
        {
            get
            {
                return BuildCommand();
            }
        }

        public DynamoTransactionCommand<T> Insert(T item)
        {
            _transactionType = DynamoTransactionType.Insert;
            _item = item;

            return this;
        }

        public DynamoTransactionCommand<T> Update(T item)
        {
            _transactionType = DynamoTransactionType.Update;
            _item = item;

            return this;
        }

        public DynamoTransactionCommand<T> Delete(T item)
        {
            _transactionType = DynamoTransactionType.Delete;
            _item = item;

            return this;
        }

        private AmazonDynamoDBRequest BuildCommand()
        {
            if (_transactionType == DynamoTransactionType.Insert)
            {
                var request = new PutItemRequest()
                {
                    TableName = typeof(T).DynamoTableName(),
                    Item = _item.Map(true)
                };

                return request;
            }

            if (_transactionType == DynamoTransactionType.Update)
            {
                var request = new UpdateItemRequest()
                {
                    TableName = typeof(T).DynamoTableName(),
                    AttributeUpdates = _item
                    .Map()
                    .Select(x => x.MapToAttributeValueUpdate())
                    .ToDictionary(x => x.Key, x => x.Value),
                    Key = _item.GetKey()
                };

                return request;
            }

            if (_transactionType == DynamoTransactionType.Delete)
            {
                var request = new DeleteItemRequest()
                {
                    TableName = typeof(T).DynamoTableName(),
                    Key = _item.GetKey()
                };
            }

            return null;
        }
    }

    internal static class KeyValuePairExtensions
    {
        internal static KeyValuePair<string, AttributeValueUpdate> MapToAttributeValueUpdate(this KeyValuePair<string, AttributeValue> keyValuePair)
        {
            return new KeyValuePair<string, AttributeValueUpdate>(keyValuePair.Key, new AttributeValueUpdate(keyValuePair.Value, AttributeAction.PUT));
        }
    }
}
