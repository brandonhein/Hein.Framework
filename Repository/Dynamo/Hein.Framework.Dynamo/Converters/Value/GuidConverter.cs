using Amazon.DynamoDBv2.Model;
using System;

namespace Hein.Framework.Dynamo.Converters.Value
{
    internal class GuidConverter : DynamoAttributeValueConverter<Guid>
    {
        public override Guid Read(AttributeValue value)
        {
            if (value.NULL)
                return default(Guid);

            if (Guid.TryParse(value.S, out var guidValue))
                return guidValue;

            return default(Guid);
        }

        public override AttributeValue Write(Guid item) => new AttributeValue { S = $"{item}" };
    }

    internal class NullableGuidConverter : DynamoAttributeValueConverter<Guid?>
    {
        public override Guid? Read(AttributeValue value)
        {
            if (value.NULL)
                return default(Guid?);

            if (Guid.TryParse(value.S, out var guidValue))
                return guidValue;

            return default(Guid?);
        }

        public override AttributeValue Write(Guid? item)
        {
            if (item.HasValue)
                return new AttributeValue { S = $"{item}" };

            return new AttributeValue { NULL = true };
        }
    }
}
