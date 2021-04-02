using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hein.Framework.Serialization.Converters
{
    public class ByteArrayConverter : JsonConverter<byte[]>
    {
        public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(byte[]) || typeToConvert == typeof(IEnumerable<byte>);

        public override byte[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String && reader.TryGetBytesFromBase64(out var bytes))
            {
                return bytes;
            }

            return default(byte[]);
        }

        public override void Write(Utf8JsonWriter writer, byte[] value, JsonSerializerOptions options)
        {
            writer.WriteBase64StringValue(value);
        }
    }
}
