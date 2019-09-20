namespace Hein.Framework.Repository.SQL
{
    public abstract class SqlCommandBase : ISqlCommand
    {
        public abstract string SQL { get; }
    }
}
