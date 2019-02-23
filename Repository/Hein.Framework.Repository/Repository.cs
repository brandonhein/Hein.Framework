using Hein.Framework.Repository.Criterion;
using System;
using System.Collections.Generic;

namespace Hein.Framework.Repository
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

        public void Commit()
        {
            Context.Commit();
        }

        public void Rollback()
        {
            Context.Rollback();
        }
    }
}
