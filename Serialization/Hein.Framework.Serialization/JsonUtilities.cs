using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace Hein.Framework.Serialization
{
    public static class JsonUtilities
    {
        public static string FormatJson(string json)
        {
            return JValue.Parse(json).ToString(Formatting.Indented);
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
