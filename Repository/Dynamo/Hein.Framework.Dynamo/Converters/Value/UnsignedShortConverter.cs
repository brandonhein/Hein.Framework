using Amazon.DynamoDBv2.Model;

namespace Hein.Framework.Dynamo.Converters.Value
{
    internal class UnsignedShortConverter : DynamoAttributeValueConverter<ushort>
    {
        public override ushort Read(AttributeValue value)
        {
            if (value.NULL)
                return default(ushort);

            if (ushort.TryParse(value.N, out var unsignedShortValue))
                return unsignedShortValue;

            return default(ushort);
        }

        public override AttributeValue Write(ushort item) => new AttributeValue { N = $"{item}" };
    }

    internal class NullableUnsignedShortConverter : DynamoAttributeValueConverter<ushort?>
    {
        public override ushort? Read(AttributeValue value)
        {
            if (value.NULL)
                return default(ushort?);

            if (ushort.TryParse(value.N, out var unsignedShortValue))
                return unsignedShortValue;

            return default(ushort?);
        }

        public override AttributeValue Write(ushort? item)
        {
            if (item.HasValue)
                return new AttributeValue { N = $"{item}" };

            return new AttributeValue { NULL = true };
        }
    }
}
