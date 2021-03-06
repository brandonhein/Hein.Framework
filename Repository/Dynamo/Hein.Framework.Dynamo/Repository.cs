﻿using Hein.Framework.Dynamo.Criterion;
using System.Collections.Generic;

namespace Hein.Framework.Dynamo
{
    public abstract class Repository<T> : IRepository<T>
    {
        private readonly IRepositoryContext _context;
        public IRepositoryContext Context
        {
            get { return _context; }
        }

        public Repository(IRepositoryContext context)
        {
            _context = context;
        }

        public abstract IEnumerable<T> GetAll();

        public abstract IEnumerable<T> Find(IQueryCriteria<T> query);

        public abstract T Save(T entity);
        public abstract void Delete(T entity);
    }
}
