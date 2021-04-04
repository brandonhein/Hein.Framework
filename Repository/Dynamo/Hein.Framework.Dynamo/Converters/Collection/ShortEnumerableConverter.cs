using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;
using System.Linq;

namespace Hein.Framework.Dynamo.Converters.Collection
{
    internal class ShortEnumerableConverter : DynamoAttributeValueConverter<IEnumerable<short>>
    {
        public override IEnumerable<short> Read(AttributeValue value)
        {
            if (value.NULL)
                return new List<short>();

            return value.NS.Select(x => short.Parse(x));
        }

        public override AttributeValue Write(IEnumerable<short> item)
        {
            if (item != null && item.Any())
                return new AttributeValue { NS = item.Select(x => x.ToString()).ToList() };

            return new AttributeValue { NULL = true };
        }
    }

    internal class ShortArrayConverter : DynamoAttributeValueConverter<short[]>
    {
        private readonly ShortEnumerableConverter _converter;
        public ShortArrayConverter()
        {
            _converter = new ShortEnumerableConverter();
        }

        public override short[] Read(AttributeValue value) => _converter.Read(value).ToArray();
        public override AttributeValue Write(short[] item) => _converter.Write(item);
    }

    internal class ShortListConverter : DynamoAttributeValueConverter<List<short>>
    {
        private readonly ShortEnumerableConverter _converter;
        public ShortListConverter()
        {
            _converter = new ShortEnumerableConverter();
        }

        public override List<short> Read(AttributeValue value) => _converter.Read(value).ToList();
        public override AttributeValue Write(List<short> item) => _converter.Write(item);
    }
}
