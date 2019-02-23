using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Hein.Framework.Repository
{
    public interface IRepositoryContext : IDisposable
    {
        SqlConnection Connection { get; }
        bool IsTranactionOpen { get; }
        int NumberOfCommits { get; }
        void Execute(string sql);
        IEnumerable<T> Query<T>(string sql);
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
