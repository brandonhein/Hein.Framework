namespace Hein.Framework.Repository.Entity
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
