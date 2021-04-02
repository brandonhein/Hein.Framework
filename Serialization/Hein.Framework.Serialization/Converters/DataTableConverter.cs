using System;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hein.Framework.Serialization.Converters
{
    public class DataTableConverter : JsonConverter<DataTable>
    {
        public override DataTable Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var newOptions = new JsonSerializerOptions(options);
            if (newOptions.Converters.Contains(this))
            {
                newOptions.Converters.Remove(this);
            }

            var results = JsonSerializer.Deserialize<IEnumerable<Dictionary<string, object>>>(ref reader, newOptions);
            var table = new DataTable();
            foreach (var item in results)
            {
                var row = table.NewRow();
                foreach (var property in item)
                {
                    row[property.Key] = property.Value;
                }
            }

            return table;
        }

        public override void Write(Utf8JsonWriter writer, DataTable value, JsonSerializerOptions options)
        {
            var results = new List<Dictionary<string, object>>();
            foreach (DataRow row in value.Rows)
            {
                var rowItem = new Dictionary<string, object>();
                foreach (DataColumn column in value.Columns)
                {
                    if (row[column] == DBNull.Value)
                    {
                        if (!options.IgnoreNullValues)
                        {
                            rowItem.Add(options.PropertyNamingPolicy?.ConvertName(column.ColumnName) ?? column.ColumnName, null);
                        }
                    }
                    else
                    {
                        rowItem.Add(options.PropertyNamingPolicy?.ConvertName(column.ColumnName) ?? column.ColumnName, row[column]);
                    }
                }

                results.Add(rowItem);
            }

            JsonSerializer.Serialize(writer, results, options);
        }
    }
}
