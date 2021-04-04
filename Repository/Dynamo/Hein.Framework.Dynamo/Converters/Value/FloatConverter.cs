using Amazon.DynamoDBv2.Model;

namespace Hein.Framework.Dynamo.Converters.Value
{
    internal class FloatConverter : DynamoAttributeValueConverter<float>
    {
        public override float Read(AttributeValue value)
        {
            if (value.NULL)
                return default(float);

            if (float.TryParse(value.N, out var floatValue))
                return floatValue;

            return default(float);
        }

        public override AttributeValue Write(float item) => new AttributeValue { N = $"{item}" };
    }

    internal class NullableFloatConverter : DynamoAttributeValueConverter<float?>
    {
        public override float? Read(AttributeValue value)
        {
            if (value.NULL)
                return default(float?);

            if (float.TryParse(value.N, out var floatValue))
                return floatValue;

            return default(float?);
        }

        public override AttributeValue Write(float? item)
        {
            if (item.HasValue)
                return new AttributeValue { N = $"{item}" };

            return new AttributeValue { NULL = true };
        }
    }
}
