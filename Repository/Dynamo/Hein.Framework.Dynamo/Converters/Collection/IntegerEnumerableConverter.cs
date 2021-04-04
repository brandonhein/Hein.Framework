using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;
using System.Linq;

namespace Hein.Framework.Dynamo.Converters.Collection
{
    internal class IntegerEnumerableConverter : DynamoAttributeValueConverter<IEnumerable<int>>
    {
        public override IEnumerable<int> Read(AttributeValue value)
        {
            if (value.NULL)
                return new List<int>();

            return value.NS.Select(x => int.Parse(x));
        }

        public override AttributeValue Write(IEnumerable<int> item)
        {
            if (item != null && item.Any())
                return new AttributeValue { NS = item.Select(x => x.ToString()).ToList() };

            return new AttributeValue { NULL = true };
        }
    }

    internal class IntegerArrayConverter : DynamoAttributeValueConverter<int[]>
    {
        private readonly IntegerEnumerableConverter _converter;
        public IntegerArrayConverter()
        {
            _converter = new IntegerEnumerableConverter();
        }

        public override int[] Read(AttributeValue value) => _converter.Read(value).ToArray();
        public override AttributeValue Write(int[] item) => _converter.Write(item);
    }

    internal class IntegerListConverter : DynamoAttributeValueConverter<List<int>>
    {
        private readonly IntegerEnumerableConverter _converter;
        public IntegerListConverter()
        {
            _converter = new IntegerEnumerableConverter();
        }

        public override List<int> Read(AttributeValue value) => _converter.Read(value).ToList();
        public override AttributeValue Write(List<int> item) => _converter.Write(item);
    }
}
