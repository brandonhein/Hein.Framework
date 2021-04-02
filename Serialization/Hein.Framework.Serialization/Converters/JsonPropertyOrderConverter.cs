using Hein.Framework.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hein.Framework.Serialization.Converters
{
    public class JsonPropertyOrderConverter : JsonConverter<object>
    {
        public override bool CanConvert(Type typeToConvert) =>
            typeToConvert.GetProperties().Any(x => x.GetCustomAttribute<JsonPropertyOrderAttribute>(true) != null);

        public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var newOptions = new JsonSerializerOptions(options);
            if (newOptions.Converters.Contains(this))
            {
                newOptions.Converters.Remove(this);
            }

            return JsonSerializer.Deserialize(ref reader, typeToConvert, newOptions);
        }

        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            var orderedProperites = value.GetType().GetProperties()
                .Where(x => x.GetCustomAttribute<JsonIgnoreAttribute>(true) == null)
                .Select(x => new
                {
                    Info = x,
                    Order = x.GetCustomAttribute<JsonPropertyOrderAttribute>(true)?.Order ?? 0
                })
                .OrderBy(x => x.Order)
                .Select(x => x.Info);

            var work = new Dictionary<string, object>();
            foreach (var property in orderedProperites)
            {
                if (property.PropertyType.IsClass)
                {
                    var propValue = property.GetValue(value, null);
                    if (propValue == null && options.IgnoreNullValues)
                    {
                        //do nothing
                    }
                    else
                    {
                        var classObj = JsonSerializer.Deserialize<object>(JsonSerializer.Serialize(propValue, options));

                        var jsonPropertyName = property.GetCustomAttribute<JsonPropertyNameAttribute>(true)?.Name;
                        if (!string.IsNullOrEmpty(jsonPropertyName))
                            work[jsonPropertyName] = classObj;
                        else
                            work[options.PropertyNamingPolicy?.ConvertName(property.Name) ?? property.Name] = classObj;
                    }
                }
                else
                {
                    var propValue = property.GetValue(value, null);
                    if (propValue == null && options.IgnoreNullValues)
                    {
                        //do nothing
                    }
                    else
                    {
                        var jsonPropertyName = property.GetCustomAttribute<JsonPropertyNameAttribute>(true)?.Name;
                        if (!string.IsNullOrEmpty(jsonPropertyName))
                            work[jsonPropertyName] = propValue;
                        else
                            work[options.PropertyNamingPolicy?.ConvertName(property.Name) ?? property.Name] = propValue;
                    }
                }
            }

            var newValue = JsonSerializer.Deserialize<object>(JsonSerializer.Serialize(work));
            JsonSerializer.Serialize(writer, newValue, options);
        }
    }
}
