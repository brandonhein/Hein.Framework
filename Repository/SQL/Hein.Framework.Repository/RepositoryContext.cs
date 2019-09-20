using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Hein.Framework.Repository
{ 
    public class RepositoryContext : IRepositoryContext
    {
        private List<string> _sqlCommands;
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private bool _isOpen = false;
        private int _numberOfCommits = 0;

        public RepositoryContext(string connectionString)
        {
            _sqlCommands = new List<string>();
            _connection = new SqlConnection(connectionString);
        }

        public IDbConnection Connection
        {
            get { return _connection; }
        }

        public bool IsTranactionOpen
        {
            get { return _isOpen; }
        }

        public int NumberOfCommits
        {
            get { return _numberOfCommits; }
        }

        public void Execute(string sql)
        {
            if (_transaction == null)
            {
                this.BeginTransaction();
            }
            if (_sqlCommands.Count >= 100)
            {
                _sqlCommands.Clear();
            }
            _sqlCommands.Add(sql);
            _connection.Execute(sql, transaction: _transaction);
        }

        public IEnumerable<T> Query<T>(string sql)
        {
            if (_transaction == null || !_isOpen)
            {
                this.BeginTransaction();
            }
            if (_sqlCommands.Count >= 100)
            {
                _sqlCommands.Clear();
            }
            _sqlCommands.Add(sql);
            return _connection.Query<T>(sql, transaction: _transaction);
        }

        public void BeginTransaction()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            _isOpen = true;

            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            _numberOfCommits++;

            if (_transaction == null)
            {
                throw new Exception("transcation never opened");
            }

            _transaction.Commit();
        }

        public void Rollback()
        {
            if (_transaction == null && !_isOpen)
            {
                //throw new Exception("transcation never opened");
            }
            else
            {
                _transaction.Rollback();
            }
        }

        public void Dispose()
        {
            _isOpen = false;

            if (_sqlCommands.Count > 0)
            {
                try
                { _transaction.Commit(); }
                catch { }
            }

            if (_connection != null)
            {
                _connection.Dispose();
            }

            if (_transaction != null)
            {
                _transaction.Dispose();
            }
        }
    }
}
