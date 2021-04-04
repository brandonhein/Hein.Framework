using Amazon.DynamoDBv2.Model;

namespace Hein.Framework.Dynamo.Converters.Value
{
    internal class UnsignedIntegerConverter : DynamoAttributeValueConverter<uint>
    {
        public override uint Read(AttributeValue value)
        {
            if (value.NULL)
                return default(uint);

            if (uint.TryParse(value.N, out var unsignedIntegerValue))
                return unsignedIntegerValue;

            return default(uint);
        }

        public override AttributeValue Write(uint item) => new AttributeValue { N = $"{item}" };
    }

    internal class NullableUnsignedIntegerConverter : DynamoAttributeValueConverter<uint?>
    {
        public override uint? Read(AttributeValue value)
        {
            if (value.NULL)
                return default(uint?);

            if (uint.TryParse(value.N, out var unsignedIntegerValue))
                return unsignedIntegerValue;

            return default(uint?);
        }

        public override AttributeValue Write(uint? item)
        {
            if (item.HasValue)
                return new AttributeValue { N = $"{item}" };

            return new AttributeValue { NULL = true };
        }
    }
}
