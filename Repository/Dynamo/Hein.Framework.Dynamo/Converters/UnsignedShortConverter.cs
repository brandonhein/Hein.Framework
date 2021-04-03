using Amazon.DynamoDBv2.Model;
using System;

namespace Hein.Framework.Dynamo.Converters
{
    internal class UnsignedShortConverter : DynamoAttributeValueConverter<ushort>
    {
        public override ushort Read(AttributeValue value)
        {
            throw new NotImplementedException();
        }

        public override AttributeValue Write(ushort item)
        {
            throw new NotImplementedException();
        }
    }

    internal class NullableUnsignedShortConverter : DynamoAttributeValueConverter<ushort?>
    {
        public override ushort? Read(AttributeValue value)
        {
            throw new NotImplementedException();
        }

        public override AttributeValue Write(ushort? item)
        {
            throw new NotImplementedException();
        }
    }
}
