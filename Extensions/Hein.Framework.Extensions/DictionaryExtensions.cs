using System.Collections.Generic;

namespace Hein.Framework.Extensions
{
    public static class DictionaryExtensions
    {
        public static bool IsNullOrEmpty<TKey, TValue>(this KeyValuePair<TKey, TValue> keyValuePair)
        {
            return keyValuePair.Equals(default(KeyValuePair<TKey, TValue>));
        }
    }
}
