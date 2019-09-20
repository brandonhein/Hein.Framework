using Hein.Framework.Repository.SQL;
using System;
using System.Linq.Expressions;

namespace Hein.Framework.Repository.Criterion
{
    public interface IQueryCriteria<T> : ISqlCommand
    {
        IQueryCriteria<T> Select<M>();
        IQueryCriteria<T> Distinct();
        IQueryCriteria<T> Top(int max);
        IQueryCriteria<T> Where(Expression<Func<T, bool>> filter);
        IQueryCriteria<T> OrderBy(Expression<Func<T, object>> orderBy);
        IQueryCriteria<T> OrderByDesending(Expression<Func<T, object>> orderBy);
    }
}
