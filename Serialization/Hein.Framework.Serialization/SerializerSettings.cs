using Hein.Framework.Serialization.Converters;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hein.Framework.Serialization
{
    public static class SerializerSettings
    {
        internal static ConcurrentBag<JsonConverter> Converters;

        internal static JsonSerializerOptions DefaultOptions
        {
            get
            {
                var options = new JsonSerializerOptions();
                options.PropertyNameCaseInsensitive = true;
                options.ReadCommentHandling = JsonCommentHandling.Skip;

                if (Converters != null)
                {
                    foreach (var converter in Converters)
                    {
                        options.Converters.Add(converter);
                    }
                }

                return options;
            }
        }

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
