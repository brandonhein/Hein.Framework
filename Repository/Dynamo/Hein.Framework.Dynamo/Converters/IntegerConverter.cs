using Amazon.DynamoDBv2.Model;

namespace Hein.Framework.Dynamo.Converters
{
    internal class IntegerConverter : DynamoAttributeValueConverter<int>
    {
        public override int Read(AttributeValue value)
        {
            if (value.NULL)
                return default(int);

            if (int.TryParse(value.N, out var intValue))
                return intValue;

            return default(int);
        }

        public override AttributeValue Write(int item) => new AttributeValue { N = $"{item}" };
    }

    internal class NullableIntegerConverter : DynamoAttributeValueConverter<int?>
    {
        public override int? Read(AttributeValue value)
        {
            if (value.NULL)
                return default(int?);

            if (int.TryParse(value.N, out var intValue))
                return intValue;

            return default(int?);
        }

        public override AttributeValue Write(int? item)
        {
            if (item.HasValue)
                return new AttributeValue { N = $"{item}" };

            return new AttributeValue { NULL = true };
        }
    }
}
