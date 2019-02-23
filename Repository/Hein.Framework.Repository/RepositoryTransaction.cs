using Hein.Framework.Repository.SQL;

namespace Hein.Framework.Repository
{
    public static class RepositoryTransaction
    {
        public static SqlTransactionQuery<T> Of<T>() where T : IRepositoryItem
        {
            return new SqlTransactionQuery<T>();
        }
    }
}
