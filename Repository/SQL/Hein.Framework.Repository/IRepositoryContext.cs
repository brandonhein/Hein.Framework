using System;
using System.Collections.Generic;
using System.Data;

namespace Hein.Framework.Repository
{
    public interface IRepositoryContext : IDisposable
    {
        IDbConnection Connection { get; }
        bool IsTranactionOpen { get; }
        int NumberOfCommits { get; }
        void Execute(string sql);
        IEnumerable<T> Query<T>(string sql);
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
