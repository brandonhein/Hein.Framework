using Amazon.DynamoDBv2.Model;
using Hein.Framework.Dynamo.Helpers;
using System.Collections.Generic;

namespace Hein.Framework.Dynamo.Converters
{
    internal class ObjectConverter : DynamoAttributeValueConverter<object>
    {
        public override object Read(AttributeValue value)
        {
            if (value.IsMSet)
            {
                var dictionary = new Dictionary<string, object>();
                foreach (var item in value.M)
                {
                    dictionary.Add(item.Key, DynamoAttributeFactory.Read(item.Value));
                }

                return dictionary.ToObject();
            }

            return null;
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
