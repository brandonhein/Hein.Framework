namespace Hein.Framework.Dynamo
{
    public interface IRepositoryEvents
    {
        void ExecuteAfterGet();
        void ExecuteBeforeSave();
        void ExecuteAfterSave();
    }
}
