using Amazon.DynamoDBv2.Model;

namespace Hein.Framework.Dynamo.Converters
{
    internal class CharacterConverter : DynamoAttributeValueConverter<char>
    {
        public override char Read(AttributeValue value)
        {
            if (value.NULL)
                return default(char);

            if (int.TryParse(value.N, out var intValue))
                return (char)intValue;

            return default(char);
        }

        public override AttributeValue Write(char item)
        {
            if (item == default(char))
                return new AttributeValue { NULL = true };

            return new AttributeValue { N = $"{System.Convert.ToInt32(item)}" };
        }
    }

    internal class NullableCharacterConverter : DynamoAttributeValueConverter<char?>
    {
        public override char? Read(AttributeValue value)
        {
            if (value.NULL)
                return default(char?);

            if (int.TryParse(value.N, out var intValue))
                return (char)intValue;

            return default(char?);
        }

        public override AttributeValue Write(char? item)
        {
            if (item.HasValue)
                return new AttributeValue { N = $"{System.Convert.ToInt32(item)}" };

            return new AttributeValue { NULL = true };
        }
    }
}
