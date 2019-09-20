using Hein.Framework.Repository.Criterion;
using System.Collections.Generic;

namespace Hein.Framework.Repository
{
    public interface IRepository<T>
    {
        IRepositoryContext Context { get; }

        T Save(T entity);
        void Delete(T entity);

        IEnumerable<T> GetAll();
        IEnumerable<T> Find(IQueryCriteria<T> query);

        void Commit();
        void Rollback();
    }
}
