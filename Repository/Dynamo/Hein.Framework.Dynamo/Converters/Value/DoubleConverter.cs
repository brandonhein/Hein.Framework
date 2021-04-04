using Amazon.DynamoDBv2.Model;
using System;

namespace Hein.Framework.Dynamo.Converters.Value
{
    internal class DoubleConverter : DynamoAttributeValueConverter<double>
    {
        public override double Read(AttributeValue value)
        {
            if (value.NULL)
                return default(double);

            if (double.TryParse(value.N, out var doubleValue))
                return doubleValue;

            return default(double);
        }

        public override AttributeValue Write(double item) => new AttributeValue { N = $"{item}" };
    }

    internal class NullableDoubleConverter : DynamoAttributeValueConverter<double?>
    {
        public override double? Read(AttributeValue value)
        {
            if (value.NULL)
                return default(double?);

            if (double.TryParse(value.N, out var doubleValue))
                return doubleValue;

            return default(double?);
        }

        public override AttributeValue Write(double? item)
        {
            if (item.HasValue)
                return new AttributeValue { N = $"{item}" };

            return new AttributeValue { NULL = true };
        }
    }
}
