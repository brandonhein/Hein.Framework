using System.Collections.Concurrent;
using System.Text.Json.Serialization;

namespace Hein.Framework.Serialization
{
    public static class Serializer
    {
        internal static ConcurrentBag<JsonConverter> Converters;

        public static void GlobalConverters(params JsonConverter[] converters)
        {
            if (Converters == null)
            {
                Converters = new ConcurrentBag<JsonConverter>();
            }

            foreach (var converter in converters)
            {
                Converters.Add(converter);
            }
        }
    }
}
