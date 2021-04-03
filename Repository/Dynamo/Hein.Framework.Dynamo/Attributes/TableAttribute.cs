using Amazon.DynamoDBv2.DataModel;
using System;

namespace Hein.Framework.Dynamo.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TableAttribute : DynamoDBAttribute
    {
        public TableAttribute(string tableName)
        {
            TableName = tableName;
        }

        public string TableName { get; }
    }
}
