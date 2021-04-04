using Amazon.DynamoDBv2.Model;

namespace Hein.Framework.Dynamo.Converters.Value
{
    internal class UnsignedLongConverter : DynamoAttributeValueConverter<ulong>
    {
        public override ulong Read(AttributeValue value)
        {
            if (value.NULL)
                return default(ulong);

            if (ulong.TryParse(value.N, out var unsignedLongValue))
                return unsignedLongValue;

            return default(ulong);
        }

        public override AttributeValue Write(ulong item) => new AttributeValue { N = $"{item}" };
    }

    internal class NullableUnsignedLongConverter : DynamoAttributeValueConverter<ulong?>
    {
        public override ulong? Read(AttributeValue value)
        {
            if (value.NULL)
                return default(ulong?);

            if (ulong.TryParse(value.N, out var unsignedLongValue))
                return unsignedLongValue;

            return default(ulong?);
        }

        public override AttributeValue Write(ulong? item)
        {
            if (item.HasValue)
                return new AttributeValue { N = $"{item}" };

            return new AttributeValue { NULL = true };
        }
    }
}
