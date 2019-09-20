using Hein.Framework.Dynamo.Command;

namespace Hein.Framework.Dynamo
{
    public static class RepositoryTransaction
    {
        public static DynamoTransactionCommand<T> Of<T>() where T : IRepositoryItem
        {
            return new DynamoTransactionCommand<T>();
        }
    }
}
