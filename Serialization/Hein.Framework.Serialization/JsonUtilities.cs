using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Hein.Framework.Serialization
{
    public static class JsonUtilities
    {
        public static string FormatJson(string json)
        {
            var options = SerializerSettings.DefaultOptions;
            options.WriteIndented = true;

            var jsonElement = JsonSerializer.Deserialize<JsonElement>(json);
            return JsonSerializer.Serialize(jsonElement, options);
        }

        public static string ToIndentedJson(this string json)
        {
            return FormatJson(json);
        }

        public static string CompressJson(this string value)
        {
            return !string.IsNullOrEmpty(value) ? Regex.Replace(value, "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1") : string.Empty;
        }

        public static string Merge(string json1, string json2)
        {
            using (var stream = new MemoryStream())
            {
                var jDoc = Merge(JsonDocument.Parse(json1), JsonDocument.Parse(json2));
                var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true });
                jDoc.WriteTo(writer);
                writer.Flush();
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        public static JsonDocument Merge(JsonDocument json1, JsonDocument json2)
        {
            using (var stream = new MemoryStream())
            {

                using (json1)
                using (json2)
                using (var jsonWriter = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true }))
                {
                    var root1 = json1.RootElement;
                    var root2 = json2.RootElement;

                    jsonWriter.WriteStartObject();

                    foreach (var property in root1.EnumerateObject())
                    {
                        if (root2.TryGetProperty(property.Name, out _))
                        {
                            property.WriteTo(jsonWriter);
                        }
                    }

                    foreach (var property in root2.EnumerateObject())
                    {
                        property.WriteTo(jsonWriter);
                    }

                    jsonWriter.WriteEndObject();
                }

                var json = Encoding.UTF8.GetString(stream.ToArray());
                return JsonDocument.Parse(json);
            }
        }
    }
}
