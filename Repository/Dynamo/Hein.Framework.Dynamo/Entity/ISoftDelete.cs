namespace Hein.Framework.Dynamo.Entity
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
