using Amazon.DynamoDBv2.Model;
using System;

namespace Hein.Framework.Dynamo.Converters
{
    internal class DecimalConverter : DynamoAttributeValueConverter<decimal>
    {
        public override decimal Read(AttributeValue value)
        {
            if (value.NULL)
                return default(decimal);

            if (decimal.TryParse(value.N, out var decimalValue))
                return decimalValue;

            return default(decimal);
        }

        public override AttributeValue Write(decimal item) => new AttributeValue { N = $"{item}" };
    }

    internal class NullableDecimalConverter : DynamoAttributeValueConverter<decimal?>
    {
        public override decimal? Read(AttributeValue value)
        {
            if (value.NULL)
                return default(decimal?);

            if (decimal.TryParse(value.N, out var decimalValue))
                return decimalValue;

            return default(decimal?);
        }

        public override AttributeValue Write(decimal? item)
        {
            if (item.HasValue)
                return new AttributeValue { N = $"{item}" };

            return new AttributeValue { NULL = true };
        }
    }
}
