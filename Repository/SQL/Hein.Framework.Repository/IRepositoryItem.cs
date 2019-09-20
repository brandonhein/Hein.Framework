namespace Hein.Framework.Repository
{
    public interface IRepositoryItem : IRepositoryEvents
    {
        long GetId();
        void SetId(long id);
    }
}
