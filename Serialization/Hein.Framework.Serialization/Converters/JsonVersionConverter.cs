using Hein.Framework.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hein.Framework.Serialization.Converters
{
    public class JsonVersionConverter : JsonConverter<object>
    {
        public override bool CanConvert(Type typeToConvert) => typeToConvert.GetCustomAttribute<JsonVersionAttribute>(true) != null;
        
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
            var attr = value.GetType().GetCustomAttribute<JsonVersionAttribute>(true);
            if (attr != null)
            {
                var sb = new StringBuilder();
                sb.Append("{");

                if (attr.IsNumber)
                    sb.Append($"\"{options.PropertyNamingPolicy?.ConvertName(attr.Name) ?? attr.Name}\":{attr.Version}");
                else
                    sb.Append($"\"{options.PropertyNamingPolicy?.ConvertName(attr.Name) ?? attr.Name}\":\"{attr.Version}\"");

                sb.Append("}");

                var work = JsonSerializer.Deserialize<IDictionary<string, object>>(sb.ToString());
                foreach (var property in value.GetType().GetProperties())
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
            else
            {
                var newOptions = new JsonSerializerOptions(options);
                if (newOptions.Converters.Contains(this))
                {
                    newOptions.Converters.Remove(this);
                }

                JsonSerializer.Serialize(writer, value, newOptions);
            }
        }
    }
}
