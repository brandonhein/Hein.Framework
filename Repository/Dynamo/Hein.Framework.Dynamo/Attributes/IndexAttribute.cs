using Amazon.DynamoDBv2.DataModel;
using System;

namespace Hein.Framework.Dynamo.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class IndexAttribute : DynamoDBAttribute
    {
        public IndexAttribute(string indexName)
        {
            IndexName = indexName;
        }

        public string IndexName { get; }
    }
}
