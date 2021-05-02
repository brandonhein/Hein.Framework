using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Hein.Framework.Dynamo.Attributes;
using Hein.Framework.Dynamo.Converters;
using Hein.Framework.Dynamo.Converters.Collection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Hein.Framework.Dynamo.Tests")]
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

        public static Dictionary<string, AttributeValue> MapToDynamo(this object entity)
        {
            return DynamoAttributeFactory.Create(entity).M;
        }

        public static T MapFromDynamo<T>(this Dictionary<string, AttributeValue> dynamoItems)
        {
            return (T)MapFromDynmao(dynamoItems, typeof(T));
        }

        internal static object MapFromDynmao(this Dictionary<string, AttributeValue> objectItems, Type itemType)
        {
            var entity = Activator.CreateInstance(itemType);

            foreach (var item in objectItems)
            {
                var property = entity.GetType().GetProperty(item.Key);
                if (!item.Value.IsMSet && !item.Value.IsLSet)
                {
                    property.SetValue(entity, DynamoAttributeFactory.Read(item.Value, property.PropertyType));
                }
                else if (item.Value.IsMSet)
                {
                    property.SetValue(entity, MapFromDynmao(item.Value.M, property.PropertyType));
                }
                else if (item.Value.IsLSet)
                {
                    if (property.PropertyType.IsGenericType && 
                        property.PropertyType.Name.IsOneOf("IList`1", "List`1", "IEnumerable`1", "ICollection`1", "IReadOnlyList`1", "IReadOnlyCollection`1"))
                    {
                        var genericType = property.PropertyType.GetGenericArguments()[0];
                        var result = new List<object>();
                        foreach (var lItem in item.Value.L)
                        {
                            if (lItem.IsMSet)
                            {
                                result.Add(MapFromDynmao(lItem.M, genericType));
                            }
                            else
                            {
                                result.Add(DynamoAttributeFactory.Read(lItem, genericType));
                            }
                        }

                        property.SetValue(entity, result.CastToList(genericType));
                    }
                    else if (property.PropertyType.IsArray)
                    {
                        var elementType = property.PropertyType.GetElementType();
                        var results = new List<object>();
                        foreach (var lItem in item.Value.L)
                        {
                            if (lItem.IsMSet)
                            {
                                results.Add(MapFromDynmao(lItem.M, elementType));
                            }
                            else
                            {
                                results.Add(DynamoAttributeFactory.Read(lItem, elementType));
                            }
                        }

                        var array = results.CastToArray(elementType);

                        property.SetValue(entity, array);
                    }
                }
            }

            return entity;
        }
    }
}
