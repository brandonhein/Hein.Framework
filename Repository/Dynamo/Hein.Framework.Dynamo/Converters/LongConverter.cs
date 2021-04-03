using Amazon.DynamoDBv2.Model;

namespace Hein.Framework.Dynamo.Converters
{
    internal class LongConverter : DynamoAttributeValueConverter<long>
    {
        public override long Read(AttributeValue value)
        {
            if (value.NULL)
                return default(long);

            if (long.TryParse(value.N, out var longValue))
                return longValue;

            return default(long);
        }

        public override AttributeValue Write(long item) => new AttributeValue { N = $"{item}" };
    }

    internal class NullableLongConverter : DynamoAttributeValueConverter<long?>
    {
        public override long? Read(AttributeValue value)
        {
            if (value.NULL)
                return default(long?);

            if (long.TryParse(value.N, out var longValue))
                return longValue;

            return default(long?);
        }

        public override AttributeValue Write(long? item)
        {
            if (item.HasValue)
                return new AttributeValue { N = $"{item}" };

            return new AttributeValue { NULL = true };
        }
    }
}
