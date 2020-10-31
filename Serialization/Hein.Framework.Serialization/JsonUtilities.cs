using System.Text.Json;
using System.Text.RegularExpressions;

namespace Hein.Framework.Serialization
{
    public static class JsonUtilities
    {
        public static string FormatJson(string json)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

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
    }
}
