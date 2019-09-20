using System;

namespace Hein.Framework.Dynamo
{
    public interface IRepositoryItem : IRepositoryEvents
    {
        Guid GetId();
        void SetId(Guid id);
    }
}
