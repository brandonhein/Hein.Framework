using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hein.Framework.Serialization.Converters
{
    public class EpochDateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
            {
                if (reader.TryGetInt64(out var unixSeconds))
                    return FromUnixTime(unixSeconds);

                if (reader.TryGetDecimal(out var unixDecimalSeconds))
                    return FromUnixTime(Convert.ToInt64(unixDecimalSeconds));
            }

            if (reader.TokenType == JsonTokenType.String)
            {
                if (reader.TryGetDateTime(out var dateTime))
                    return dateTime;

                var jsonString = reader.GetString();
                if (long.TryParse(jsonString, out var unixSeconds))
                    return FromUnixTime(unixSeconds);
                if (decimal.TryParse(jsonString, out var unixDecmialSeconds))
                    return FromUnixTime(Convert.ToInt64(unixDecmialSeconds));
            }

            return default(DateTime);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(ToUnixTime(value));
        }

        private DateTime FromUnixTime(long unixTime)
        {
            var result = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return result.AddSeconds(unixTime);
        }

        private long ToUnixTime(DateTime dateTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((dateTime - epoch).TotalSeconds);
        }
    }
}
