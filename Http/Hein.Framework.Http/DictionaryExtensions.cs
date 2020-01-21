using System.Collections.Generic;

namespace Hein.Framework.Http
{
    public static class DictionaryExtensions
    {
        public static string ToUrlEncode(this IDictionary<string, string> dictionary)
        {
            var result = string.Empty;
            foreach (var item in dictionary)
            {
                result = string.Concat(result, item.Key, "=", item.Value, "&");
            }

            result = result.TrimEnd('&');
            result = result.Trim();
            return result;
        }
    }
}
