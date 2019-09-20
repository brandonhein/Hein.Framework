using System;
using System.Collections.Generic;
using System.Text;

namespace Hein.Framework.Repository
{
    public class KeyAttribute : Attribute
    { }

    public class ExcludeAttribute : Attribute
    { }

    public class TableNameAttribute : Attribute
    {
        public TableNameAttribute(string tableName)
        {
            Value = tableName;
        }

        public string Value { get; }
    }
}
