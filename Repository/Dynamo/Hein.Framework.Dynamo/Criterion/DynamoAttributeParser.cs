using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hein.Framework.Dynamo.Criterion
{
    public static class DynamoAttributeParser
    {
        public static string DynamoTableName(this Type entityType)
        {
            var attributes = entityType.GetCustomAttributes(typeof(DynamoDBTableAttribute), true);

            foreach (var attribute in attributes)
            {
                if (attribute is DynamoDBTableAttribute)
                {
                    return ((DynamoDBTableAttribute)attribute).TableName;
                }
            }

            return entityType.Name;
        }

        public static Dictionary<string, string> ExpressionAttriubtes(this Type entityType)
        {
            var properties = entityType.GetProperties();

            var attributes = new Dictionary<string, string>();

            foreach (var property in properties)
            {
                bool isExcludeProp = false;
                foreach (var attr in property.GetCustomAttributes(true))
                {
                    ExcludeAttribute ex = attr as ExcludeAttribute;
                    if (ex != null)
                    {
                        isExcludeProp = true;
                        break;
                    }
                }

                if (!isExcludeProp)
                {
                    attributes.Add(GetPropertyAttribute(property.Name), property.Name);
                }
            }

            return attributes;
        }

        public static string GetPropertyAttribute(string propertyName)
        {
            return $"#{char.ToLowerInvariant(propertyName[0])}{propertyName.Substring(1)}";
        }

        public static T Map<T>(this Dictionary<string, AttributeValue> values)
        {
            var entity = (T)Activator.CreateInstance(typeof(T));

            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                if (values.ContainsKey(property.Name))
                {
                    if (DynamoValueConverter.ConvertToValue.ContainsKey(property.PropertyType))
                    {
                        property.SetValue(entity, DynamoValueConverter.ConvertToValue[property.PropertyType](values[property.Name]));
                    }
                    else if (property.PropertyType.IsClass)
                    {
                        var value = (Dictionary<string, AttributeValue>)DynamoValueConverter.ConvertToValue[typeof(object)](values[property.Name]);

                        property.SetValue(entity, DynamoValueConverter.FromDictionary(property.PropertyType, value));
                    }
                }
            }

            return entity;
        }

        public static Dictionary<string, AttributeValue> GetKey<T>(this T entity, object hashKeyValue = null, object rangeKeyValue = null)
        {
            var keys = new Dictionary<string, AttributeValue>();

            var type = typeof(T);
            var properties = type.GetProperties();
            var hashKeys = properties.Where(property => Attribute.IsDefined(property, typeof(DynamoDBHashKeyAttribute)) || Attribute.IsDefined(property, typeof(KeyAttribute)));
            var rangeKeys = properties.Where(property => Attribute.IsDefined(property, typeof(DynamoDBRangeKeyAttribute)));

            //if ((hashKeys.Count() + rangeKeys.Count()) == 0)
            //    throw new TableKeyAttributeException(type);
            //else if (hashKeys.Count() != 1)
            //    throw new TableKeyAttributeException(type, KeyEnum.Hash);
            //else if (rangeKeys.Count() > 1)
            //    throw new TableKeyAttributeException(type, KeyEnum.Range);

            if (hashKeys.Count() == 1)
            {
                var propertyInfo = hashKeys.First();
                var attributeName = propertyInfo.Name;

                if (hashKeyValue != null)
                    keys.Add(attributeName, DynamoValueConverter.ConvertToAttributeValue[propertyInfo.PropertyType](hashKeyValue));
                else
                    keys.Add(attributeName, DynamoValueConverter.ConvertToAttributeValue[propertyInfo.PropertyType](propertyInfo.GetValue(entity)));
            }

            if (rangeKeys.Count() == 1)
            {
                var propertyInfo = rangeKeys.First();
                var attributeName = propertyInfo.Name;

                if (rangeKeyValue != null)
                    keys.Add(attributeName, DynamoValueConverter.ConvertToAttributeValue[propertyInfo.PropertyType](rangeKeyValue));
                else
                    keys.Add(attributeName, DynamoValueConverter.ConvertToAttributeValue[propertyInfo.PropertyType](propertyInfo.GetValue(entity)));
            }

            return keys;
        }

        public static Dictionary<string, AttributeValue> Map(this object entity, bool includeKeys = false)
        {
            var results = new Dictionary<string, AttributeValue>();

            var type = entity.GetType();
            var properties = type.GetProperties()
                .Where(x => includeKeys || !IsKeyProperty(x));

            foreach (var property in properties)
            {
                if (DynamoValueConverter.ConvertToAttributeValue.ContainsKey(property.PropertyType))
                {
                    results.Add(property.Name, DynamoValueConverter.ConvertToAttributeValue[property.PropertyType](property.GetValue(entity)));
                }
                else if (property.PropertyType.IsClass)
                {
                    results.Add(property.Name, DynamoValueConverter.ConvertToAttributeValue[typeof(object)](property.GetValue(entity)));
                }
            }

            return results;
        }

        private static bool IsKeyProperty(PropertyInfo propertyInfo)
        {
            var result = false;
            if (Attribute.IsDefined(propertyInfo, typeof(DynamoDBHashKeyAttribute)) ||
                Attribute.IsDefined(propertyInfo, typeof(DynamoDBRangeKeyAttribute)) ||
                Attribute.IsDefined(propertyInfo, typeof(KeyAttribute)))
            {
                result = true;
            }

            return result;
        }
    }
}
