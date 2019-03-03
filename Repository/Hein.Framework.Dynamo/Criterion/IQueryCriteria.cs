using Hein.Framework.Dynamo.Command;
using System;
using System.Linq.Expressions;

namespace Hein.Framework.Dynamo.Criterion
{
    public interface IQueryCriteria<T> : IDynamoCommand
    {
        IQueryCriteria<T> Select(params string[] propertyNames);
        IQueryCriteria<T> Select<M>();
        IQueryCriteria<T> Distinct();
        IQueryCriteria<T> Top(int max);
        IQueryCriteria<T> Where(Expression<Func<T, bool>> filter);
        IQueryCriteria<T> OrderBy(Expression<Func<T, object>> orderBy);
        IQueryCriteria<T> OrderByDesending(Expression<Func<T, object>> orderBy);
    }
}
