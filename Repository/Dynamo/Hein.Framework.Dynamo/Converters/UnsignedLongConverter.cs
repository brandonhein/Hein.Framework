using Amazon.DynamoDBv2.Model;
using System;

namespace Hein.Framework.Dynamo.Converters
{
    internal class UnsignedLongConverter : DynamoAttributeValueConverter<ulong>
    {
        public override ulong Read(AttributeValue value)
        {
            throw new NotImplementedException();
        }

        public override AttributeValue Write(ulong item)
        {
            throw new NotImplementedException();
        }
    }

    internal class NullableUnsignedLongConverter : DynamoAttributeValueConverter<ulong?>
    {
        public override ulong? Read(AttributeValue value)
        {
            throw new NotImplementedException();
        }

        public override AttributeValue Write(ulong? item)
        {
            throw new NotImplementedException();
        }
    }
}
