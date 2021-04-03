using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;
using System.Linq;

namespace Hein.Framework.Dynamo.Converters
{
    internal class StringArrayConverter : DynamoAttributeValueConverter<string[]>
    {
        private readonly StringEnumerableConverter _converter;
        public StringArrayConverter()
        {
            _converter = new StringEnumerableConverter();
        }

        public override string[] Read(AttributeValue value) => _converter.Read(value).ToArray();
        public override AttributeValue Write(string[] item) => _converter.Write(item);
    }

    internal class StringListConverter : DynamoAttributeValueConverter<List<string>>
    {
        private readonly StringEnumerableConverter _converter;
        public StringListConverter()
        {
            _converter = new StringEnumerableConverter();
        }

        public override List<string> Read(AttributeValue value) => _converter.Read(value).ToList();
        public override AttributeValue Write(List<string> item) => _converter.Write(item);
    }

    internal class StringEnumerableConverter : DynamoAttributeValueConverter<IEnumerable<string>>
    {
        public override IEnumerable<string> Read(AttributeValue value)
        {
            if (value.NULL)
                return new List<string>();

            return value.SS;
        }

        public override AttributeValue Write(IEnumerable<string> item)
        {
            if (item != null && item.Any())
                return new AttributeValue { SS = item.ToList() };

            return new AttributeValue { NULL = true };
        }
    }
}
