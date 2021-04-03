using Amazon.DynamoDBv2.Model;

namespace Hein.Framework.Dynamo.Converters
{
    internal class ByteConverter : DynamoAttributeValueConverter<byte>
    {
        public override byte Read(AttributeValue value)
        {
            if (value.NULL)
                return default(byte);

            if (byte.TryParse(value.N, out var byteValue))
                return byteValue;

            return default(byte);
        }

        public override AttributeValue Write(byte item)
        {
            return new AttributeValue { N = item.ToString() };
        }
    }

    internal class NullableByteConverter : DynamoAttributeValueConverter<byte?>
    {
        public override byte? Read(AttributeValue value)
        {
            if (value.NULL)
                return default(byte?);

            if (byte.TryParse(value.N, out var byteValue))
                return byteValue;

            return default(byte?);
        }

        public override AttributeValue Write(byte? item)
        {
            if (item.HasValue)
                return new AttributeValue { N = item.ToString() };

            return new AttributeValue { NULL = true };
        }
    }
}
