using Amazon.DynamoDBv2.Model;
using System;

namespace Hein.Framework.Dynamo.Converters
{
    internal class ObjectConverter : DynamoAttributeValueConverter<object>
    {
        public override object Read(AttributeValue value)
        {
            throw new NotImplementedException();
        }

        public override AttributeValue Write(object item)
        {
            throw new NotImplementedException();
        }
    }
}
