using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hein.Framework.Serialization.Converters
{
    /// <summary>
    /// JsonConverter that reads possible values for a bool
    /// <para>Values for true: T, True, Y, Yes, 1</para>
    /// <para>Values for false: F, False, N, No, 0</para>
    /// <para>If null or not discoverable: false</para>
    /// </summary>
    public class BooleanConverter : JsonConverter<bool>
    {
        public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(bool);

        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return false;
            if (reader.TokenType == JsonTokenType.None)
                return false;
            if (reader.TokenType == JsonTokenType.True)
                return true;
            if (reader.TokenType == JsonTokenType.False)
                return false;
            if (reader.TokenType == JsonTokenType.Number)
            {
                if (reader.TryGetInt64(out var longValue))
                    return longValue == 1;
                if (reader.TryGetDouble(out var doubleValue))
                    return doubleValue == 1;
            }
            if (reader.TokenType == JsonTokenType.String)
            {
                var value = reader.GetString();
                if (string.IsNullOrEmpty(value))
                    return false;

                value = value.ToLower().Trim();
                return value == "true" || value == "t" || value == "yes" || value == "y" || value == "1";
            }

            return false;
        }

        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        {
            writer.WriteBooleanValue(value);
        }
    }

    /// <summary>
    /// JsonConverter that reads possible values for a nullable bool
    /// <para>Values for true: T, True, Y, Yes, 1</para>
    /// <para>Values for false: F, False, N, No, 0</para>
    /// <para>If null or not discoverable: default(<see cref="bool?"/>)</para>
    /// </summary>
    public class NullableBooleanConverter : JsonConverter<bool?>
    {
        public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(bool?);

        public override bool? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return default(bool?);
            if (reader.TokenType == JsonTokenType.None)
                return default(bool?);
            if (reader.TokenType == JsonTokenType.True)
                return true;
            if (reader.TokenType == JsonTokenType.False)
                return false;
            if (reader.TokenType == JsonTokenType.Number)
            {
                if (reader.TryGetInt64(out var longValue))
                    return longValue == 1;
                if (reader.TryGetDouble(out var doubleValue))
                    return doubleValue == 1;
            }
            if (reader.TokenType == JsonTokenType.String)
            {
                var value = reader.GetString();
                if (string.IsNullOrEmpty(value))
                    return false;

                value = value.ToLower().Trim();
                var isTrue = value == "true" || value == "t" || value == "yes" || value == "y" || value == "1";
                if (isTrue)
                    return true;

                var isFalse = value == "false" || value == "f" || value == "no" || value == "n" || value == "0";
                if (isFalse)
                    return false;
            }

            return default(bool?);
        }

        public override void Write(Utf8JsonWriter writer, bool? value, JsonSerializerOptions options)
        {
            if (!value.HasValue)
                writer.WriteNullValue();
            else
                writer.WriteBooleanValue(value.Value);
        }
    }
}
