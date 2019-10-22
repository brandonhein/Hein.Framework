using System;

namespace Hein.Framework.Dynamo.Entity
{
    public interface IEntity : IRepositoryItem
    {
        Guid GetId();
        void SetId(Guid id);
    }
}
