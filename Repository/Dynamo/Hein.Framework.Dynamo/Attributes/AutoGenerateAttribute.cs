using Amazon.DynamoDBv2.DataModel;
using System;

namespace Hein.Framework.Dynamo.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AutoGenerateAttribute : DynamoDBAttribute
    {
        public AutoGenerateAttribute(AutoGenerateValue valueType)
        {
            Type = valueType;
        }

        public AutoGenerateValue Type { get; }
    }

    public enum AutoGenerateValue
    {
        Guid,
        Number
    }
}
