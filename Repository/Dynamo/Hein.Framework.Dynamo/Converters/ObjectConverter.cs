using Amazon.DynamoDBv2.Model;
using Hein.Framework.Dynamo.Helpers;
using System.Collections.Generic;

namespace Hein.Framework.Dynamo.Converters
{
    internal class ObjectConverter : DynamoAttributeValueConverter<object>
    {
        public override object Read(AttributeValue value)
        {
            if (value.NULL)
                return null;

            if (value.IsMSet)
            {
                var result = new Dictionary<string, object>();
                foreach (var attributeValue in value.M)
                {
                    result.Add(attributeValue.Key, DynamoAttributeFactory.Read(attributeValue.Value));
                }

                return result.ToObject();
            }

            return default;
        }

        public override AttributeValue Write(object item)
        {
            if (item == null)
                return new AttributeValue { NULL = true };

            var result = new Dictionary<string, AttributeValue>();
            var properties = item.GetType().GetProperties();
            foreach (var property in properties)
            {
                //todo what if i want a differet name?
                if (!result.ContainsKey(property.Name))
                {
                    var converter = DynamoAttributeFactory.GetConverter(property.PropertyType);
                    var attributeValue = DynamoAttributeFactory.Create(property.GetValue(item, null), converter);

                    result.Add(property.Name, attributeValue);
                }
            }

            return new AttributeValue { M = result };
        }
    }
}
