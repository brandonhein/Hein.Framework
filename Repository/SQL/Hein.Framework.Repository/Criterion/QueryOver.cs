namespace Hein.Framework.Repository.Criterion
{
    public static class QueryOver
    {
        public static IQueryCriteria<T> Of<T>()
        {
            return new QueryCriteria<T>();
        }
    }
}
