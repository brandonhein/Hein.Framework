using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Hein.Framework.Dynamo.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hein.Framework.Dynamo.Helpers
{
    internal static class DynamoDbHelper
    {
        public static string DynamoTableName(this object entity) => entity.GetType().DynamoTableName();

        public static string DynamoTableName<T>() => DynamoTableName(typeof(T));

        public static string DynamoTableName(this Type entityType)
        {
            var attr = entityType.GetCustomAttribute<DynamoDBTableAttribute>(true);
            if (attr != null)
                return attr.TableName;

            var tableAttr = entityType.GetCustomAttribute<TableAttribute>(true);
            if (tableAttr != null)
                return tableAttr.TableName;

            return entityType.Name;
        }

        public static Dictionary<string, AttributeValue> GetKey(this object entity)
        {
            var keyProperty = entity
                .GetType()
                .GetProperties()
                .Where(x => 
                    x.GetCustomAttribute<DynamoDBHashKeyAttribute>(true) != null || 
                    x.GetCustomAttribute<PartitionKeyAttribute>(true) != null ||
                    x.GetCustomAttribute<KeyAttribute>(true) != null)
                .FirstOrDefault();

            return null;

        }
    }
}
