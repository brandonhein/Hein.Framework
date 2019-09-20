using System;
using System.Collections.Generic;
using System.Text;

namespace Hein.Framework.Repository
{
    public interface IRepositoryEvents
    {
        void ExecuteAfterGet();
        void ExecuteBeforeSave();
        void ExecuteAfterSave();
    }
}
