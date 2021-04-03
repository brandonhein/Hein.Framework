using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Hein.Framework.Dynamo.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hein.Framework.Dynamo
{
    public interface IRepositoryContext : IDisposable
    {
        IRepositoryContext BeginTransaction();
        bool IsTransactionOpen { get; }
        int NumberOfCommits { get; }
        bool Execute(AmazonDynamoDBRequest request);
        IEnumerable<T> Execute<T>(AmazonDynamoDBRequest request);
        Task<bool> ExecuteAsync(AmazonDynamoDBRequest request);
        Task<IEnumerable<T>> ExecuteAsync<T>(AmazonDynamoDBRequest request);
        bool Commit();
        Task<bool> CommitAsync();
        bool Rollback();
        Task<bool> RollbackAsync();
    }

    public class RepositoryContext : IRepositoryContext
    {
        private readonly IAmazonDynamoDB _dynamo;
        private List<TransactWriteItem> _transactionItems;

        public RepositoryContext(IAmazonDynamoDB dynamo = null)
        {
            _dynamo = dynamo ?? new AmazonDynamoDBClient();
        }

        public bool IsTransactionOpen { get; private set; }
        public int NumberOfCommits { get; private set; }

        public IRepositoryContext BeginTransaction()
        {
            _transactionItems = new List<TransactWriteItem>();
            IsTransactionOpen = true;
            return this;
        }

        private void Flush()
        {
            _transactionItems = new List<TransactWriteItem>();
        }

        public bool Commit()
        {
            try
            {
                return CommitAsync().Result;
            }
            catch (AggregateException ex)
            {
                throw ex.GetBaseException();
            }
        }

        public async Task<bool> CommitAsync()
        {
            if (!IsTransactionOpen)
                return true;

            try
            {
                NumberOfCommits++;
                if (_transactionItems.Any())
                {
                    //only process 25 at a time: https://docs.aws.amazon.com/amazondynamodb/latest/APIReference/API_TransactWriteItems.html
                    var batch = new List<TransactWriteItem>();
                    foreach (var tranItem in _transactionItems)
                    {
                        batch.Add(tranItem);

                        if (batch.Count == 25)
                        {
                            await ExecuteBatchAsync(batch);
                            batch = new List<TransactWriteItem>();
                        }
                    }

                    // Process last batch
                    if (batch.Any())
                    {
                        await ExecuteBatchAsync(batch);
                    }

                    Flush();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private async Task ExecuteBatchAsync(IEnumerable<TransactWriteItem> items)
        {
            var request = new TransactWriteItemsRequest()
            {
                TransactItems = items.ToList(),
                ReturnConsumedCapacity = ReturnConsumedCapacity.TOTAL
            };

            await _dynamo.TransactWriteItemsAsync(request);
        }

        public void Dispose()
        {
            if (_transactionItems != null && !_transactionItems.Any())
            {
                Commit();
            }
        }

        public bool Rollback()
        {
            Flush();
            return true;
        }

        public Task<bool> RollbackAsync() => Task.FromResult(Rollback());

        public bool Execute(AmazonDynamoDBRequest request)
        {
            try
            {
                return ExecuteAsync(request).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.GetBaseException();
            }
        }

        public async Task<bool> ExecuteAsync(AmazonDynamoDBRequest request)
        {
            var item = new TransactWriteItem();
            if (request is UpdateItemRequest)
            {
                item.Update = ((UpdateItemRequest)request).Convert();
            }
            if (request is PutItemRequest)
            {
                item.Put = ((PutItemRequest)request).Convert();
            }
            if (request is DeleteItemRequest)
            {
                item.Delete = ((DeleteItemRequest)request).Convert();
            }

            if (IsTransactionOpen)
            {
                if (_transactionItems == null)
                    _transactionItems = new List<TransactWriteItem>();

                _transactionItems.Add(item);
            }
            else
            {
                var itemList = new List<TransactWriteItem>() { item };
                await ExecuteBatchAsync(itemList);
            }

            return true;
        }

        public IEnumerable<T> Execute<T>(AmazonDynamoDBRequest request)
        {
            try
            {
                return ExecuteAsync<T>(request).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.GetBaseException();
            }
        }

        public async Task<IEnumerable<T>> ExecuteAsync<T>(AmazonDynamoDBRequest request)
        {
            var results = new List<T>();

            if (request is GetItemRequest)
            {
                var response = await _dynamo.GetItemAsync((GetItemRequest)request);
            }
            if (request is ScanRequest)
            {
                var scanRequest = (ScanRequest)request;
                var gettingAll = true;
                while (gettingAll)
                {
                    var response = await _dynamo.ScanAsync(scanRequest);
                    //results.AddRange();

                    if (response.LastEvaluatedKey == default(Dictionary<string, AttributeValue>))
                        gettingAll = false;
                    else
                        scanRequest.ExclusiveStartKey = response.LastEvaluatedKey;
                }
            }
            if (request is QueryRequest)
            {
                var queryRequest = (QueryRequest)request;
                var gettingAll = true;
                while (gettingAll)
                {
                    var response = await _dynamo.QueryAsync(queryRequest);
                    //results.AddRange();

                    if (response.LastEvaluatedKey == default(Dictionary<string, AttributeValue>))
                        gettingAll = false;
                    else
                        queryRequest.ExclusiveStartKey = response.LastEvaluatedKey;
                }
            }

            return new List<T>();
        }
    }
}
