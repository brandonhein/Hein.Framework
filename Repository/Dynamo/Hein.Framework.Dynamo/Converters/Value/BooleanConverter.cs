using Amazon.DynamoDBv2.Model;

namespace Hein.Framework.Dynamo.Converters.Value
{
    internal class BooleanConverter : DynamoAttributeValueConverter<bool>
    {
        public override bool Read(AttributeValue value)
        {
            if (value.IsBOOLSet)
                return value.BOOL;

            if (int.TryParse(value.N, out var intValue))
                return intValue == 1;

            return false;
        }

        public override AttributeValue Write(bool item) => new AttributeValue { BOOL = item };
    }

    internal class NullableBooleanConverter : DynamoAttributeValueConverter<bool?>
    {
        public override bool? Read(AttributeValue value)
        {
            if (value.NULL)
                return default(bool?);

            if (value.IsBOOLSet)
                return value.BOOL;

            if (int.TryParse(value.N, out var intValue))
                return intValue == 1;

            return default(bool?);
        }

        public override AttributeValue Write(bool? item)
        {
            if (item.HasValue)
                return new AttributeValue { BOOL = item.Value };

            return new AttributeValue { NULL = true };
        }
    }
}
