namespace Hein.Framework.Repository
{
    public interface IRepositoryEvents
    {
        void ExecuteAfterGet();
        void ExecuteBeforeSave();
        void ExecuteAfterSave();
    }
}
