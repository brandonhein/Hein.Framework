using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hein.Framework.Serialization.Converters
{
    public class MemoryStreamConverter : JsonConverter<MemoryStream>
    {
        public override MemoryStream Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String && reader.TryGetBytesFromBase64(out var bytes))
            {
                return new MemoryStream(bytes);
            }

            return default(MemoryStream);
        }

        public override void Write(Utf8JsonWriter writer, MemoryStream value, JsonSerializerOptions options)
        {
            writer.WriteBase64StringValue(value.ToArray());
        }
    }
}
