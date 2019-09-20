using Amazon.DynamoDBv2;
using System;
using System.Collections.Generic;

namespace Hein.Framework.Dynamo
{
    public interface IRepositoryContext : IDisposable
    {
        bool IsTranactionOpen { get; }
        int NumberOfCommits { get; }
        void Execute(AmazonDynamoDBRequest dynamoRequest);
        IEnumerable<T> Query<T>(AmazonDynamoDBRequest query);
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
