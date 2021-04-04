using Amazon.DynamoDBv2.Model;

namespace Hein.Framework.Dynamo.Converters.Value
{
    internal class ShortConverter : DynamoAttributeValueConverter<short>
    {
        public override short Read(AttributeValue value)
        {
            if (value.NULL)
                return default(short);

            if (short.TryParse(value.N, out var shortValue))
                return shortValue;

            return default(short);
        }

        public override AttributeValue Write(short item) => new AttributeValue { N = $"{item}" };
    }

    internal class NullableShortConverter : DynamoAttributeValueConverter<short?>
    {
        public override short? Read(AttributeValue value)
        {
            if (value.NULL)
                return default(short?);

            if (short.TryParse(value.N, out var shortValue))
                return shortValue;

            return default(short?);
        }

        public override AttributeValue Write(short? item)
        {
            if (item.HasValue)
                return new AttributeValue { N = $"{item}" };

            return new AttributeValue { NULL = true };
        }
    }
}
