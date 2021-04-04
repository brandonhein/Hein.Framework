using Amazon.DynamoDBv2.Model;
using System;

namespace Hein.Framework.Dynamo.Converters.Value
{
    internal class DateTimeConverter : DynamoAttributeValueConverter<DateTime>
    {
        public override DateTime Read(AttributeValue value)
        {
            if (value.NULL)
                return default(DateTime);

            if (DateTime.TryParse(value.S, out var dateTimeValue))
                return dateTimeValue.ToUniversalTime();

            return default(DateTime);
        }

        public override AttributeValue Write(DateTime item) => new AttributeValue { S = item.ToUniversalTime().ToString("o") };
    }

    internal class NullableDateTimeConverter : DynamoAttributeValueConverter<DateTime?>
    {
        public override DateTime? Read(AttributeValue value)
        {
            if (value.NULL)
                return default(DateTime?);

            if (DateTime.TryParse(value.S, out var dateTimeValue))
                return dateTimeValue.ToUniversalTime();

            return default(DateTime?);
        }

        public override AttributeValue Write(DateTime? item)
        {
            if (item.HasValue)
                return new AttributeValue { S = item.Value.ToUniversalTime().ToString("o") };

            return new AttributeValue { NULL = true };
        }
    }
}
