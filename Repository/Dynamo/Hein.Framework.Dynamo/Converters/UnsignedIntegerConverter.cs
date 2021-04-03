using Amazon.DynamoDBv2.Model;
using System;

namespace Hein.Framework.Dynamo.Converters
{
    internal class UnsignedIntegerConverter : DynamoAttributeValueConverter<uint>
    {
        public override uint Read(AttributeValue value)
        {
            throw new NotImplementedException();
        }

        public override AttributeValue Write(uint item)
        {
            throw new NotImplementedException();
        }
    }

    internal class NullableUnsignedIntegerConverter : DynamoAttributeValueConverter<uint?>
    {
        public override uint? Read(AttributeValue value)
        {
            throw new NotImplementedException();
        }

        public override AttributeValue Write(uint? item)
        {
            throw new NotImplementedException();
        }
    }
}
