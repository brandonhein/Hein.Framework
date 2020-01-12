using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Hein.Framework.Dynamo.Command;
using Hein.Framework.Dynamo.Criterion;
using System.Collections.Generic;
using System.Linq;

namespace Hein.Framework.Dynamo
{
    public class RepositoryContext : DynamoRepositoryBase, IRepositoryContext
    {
        private List<TransactWriteItem> _transactionItems;

        public RepositoryContext(RegionEndpoint endpoint) : base(endpoint)
        {
            _transactionItems = new List<TransactWriteItem>();
        }

        public bool IsTranactionOpen { get; private set; }

        public int NumberOfCommits { get; private set; }

        public void Execute(AmazonDynamoDBRequest dynamoRequest)
        {
            var item = new TransactWriteItem();
            if (dynamoRequest is UpdateItemRequest)
            {
                item.Update = ((UpdateItemRequest)dynamoRequest).Convert();
            }
            if (dynamoRequest is PutItemRequest)
            {
                item.Put = ((PutItemRequest)dynamoRequest).Convert();
            }
            if (dynamoRequest is DeleteItemRequest)
            {
                item.Delete = ((DeleteItemRequest)dynamoRequest).Convert();
            }

            _transactionItems.Add(item);
        }

        public IEnumerable<T> Query<T>(AmazonDynamoDBRequest query)
        {
            var queryResult = base.GetDynamo()
                .QueryAsync((QueryRequest)query)
                .Result;

            return queryResult
                .Items
                .Select(x => x.Map<T>())
                .ToList();
        }

        public void BeginTransaction()
        {
            IsTranactionOpen = true;
        }

        public void Commit()
        {
            NumberOfCommits++;
            if (!IsTranactionOpen)
            {
                //should throw?
            }

            if (_transactionItems.Any())
            {
                //only process 25 at a time: https://docs.aws.amazon.com/amazondynamodb/latest/APIReference/API_TransactWriteItems.html
                var batch = new List<TransactWriteItem>();
                foreach (var tranItem in _transactionItems)
                {
                    batch.Add(tranItem);

                    if (batch.Count == 25)
                    {
                        BatchDynamo(batch);
                        batch.Clear();
                    }
                }

                // Process last batch
                if (batch.Any())
                {
                    BatchDynamo(batch);
                }

                Flush();
            }
        }

        private void BatchDynamo(List<TransactWriteItem> items)
        {
            var trans = new TransactWriteItemsRequest()
            {
                TransactItems = items,
                ReturnConsumedCapacity = ReturnConsumedCapacity.TOTAL
            };

            var response = base.GetDynamo().TransactWriteItemsAsync(trans).Result;
        }

        public void Rollback()
        {
            Flush();
        }

        private void Flush()
        {
            _transactionItems = new List<TransactWriteItem>();
        }

        public void Dispose()
        {
            if (_transactionItems != null && !_transactionItems.Any())
            {
                Commit();
            }
        }
    }
}
