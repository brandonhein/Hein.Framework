using Amazon.DynamoDBv2.Model;
using System;

namespace Hein.Framework.Dynamo.Converters.Value
{
    internal class DateTimeOffsetConverter : DynamoAttributeValueConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(AttributeValue value)
        {
            if (value.NULL)
                return default(DateTimeOffset);

            if (DateTimeOffset.TryParse(value.S, out var dateTimeValue))
                return dateTimeValue;

            return default(DateTimeOffset);
        }

        public override AttributeValue Write(DateTimeOffset item)
        {
            var dateTime = item.DateTime;
            return new AttributeValue { S = dateTime.ToUniversalTime().ToString("o") };
        }
    }

    internal class NullableDateTimeOffsetConverter : DynamoAttributeValueConverter<DateTimeOffset?>
    {
        public override DateTimeOffset? Read(AttributeValue value)
        {
            if (value.NULL)
                return default(DateTimeOffset?);

            if (DateTimeOffset.TryParse(value.S, out var dateTimeValue))
                return dateTimeValue;

            return default(DateTimeOffset?);
        }

        public override AttributeValue Write(DateTimeOffset? item)
        {
            if (item.HasValue)
            {
                var dateTime = item.Value.DateTime;
                return new AttributeValue { S = dateTime.ToUniversalTime().ToString("o") };
            }

            return new AttributeValue { NULL = true };
        }
    }
}
