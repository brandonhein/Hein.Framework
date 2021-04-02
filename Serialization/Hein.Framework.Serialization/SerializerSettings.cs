using Hein.Framework.Serialization.Converters;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hein.Framework.Serialization
{
    public static class SerializerSettings
    {
        internal static ConcurrentBag<JsonConverter> Converters;

        public static JsonSerializerOptions DefaultOptions
        {
            get
            {
                var options = new JsonSerializerOptions();
                options.AllowTrailingCommas = true;
                options.PropertyNameCaseInsensitive = true;
                options.ReadCommentHandling = JsonCommentHandling.Skip;
                options.NumberHandling = JsonNumberHandling.AllowReadingFromString;

                if (Converters != null)
                {
                    foreach (var converter in Converters)
                    {
                        if (!options.Converters.Contains(converter))
                            options.Converters.Add(converter);
                    }
                }

                var jsonOrderConverter = new JsonPropertyOrderConverter();
                var jsonVersionConverter = new JsonVersionConverter();
                var memoryStreamConverter = new MemoryStreamConverter();
                var boolConverter = new BooleanConverter();
                var nullBoolConverter = new NullableBooleanConverter();
                var byteArrayConverter = new ByteArrayConverter();

                if (!options.Converters.Contains(jsonOrderConverter))
                    options.Converters.Add(jsonOrderConverter);
                if (!options.Converters.Contains(jsonVersionConverter))
                    options.Converters.Add(jsonVersionConverter);
                if (!options.Converters.Contains(memoryStreamConverter))
                    options.Converters.Add(memoryStreamConverter);
                if (!options.Converters.Contains(boolConverter))
                    options.Converters.Add(boolConverter);
                if (!options.Converters.Contains(nullBoolConverter))
                    options.Converters.Add(nullBoolConverter);
                if (!options.Converters.Contains(byteArrayConverter))
                    options.Converters.Add(byteArrayConverter);

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
