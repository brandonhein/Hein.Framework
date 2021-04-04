using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;
using System.Linq;

namespace Hein.Framework.Dynamo.Converters.Collection
{
    internal class UnsignedShortEnumerableConverter : DynamoAttributeValueConverter<IEnumerable<ushort>>
    {
        public override IEnumerable<ushort> Read(AttributeValue value)
        {
            if (value.NULL)
                return new List<ushort>();

            return value.NS.Select(x => ushort.Parse(x));
        }

        public override AttributeValue Write(IEnumerable<ushort> item)
        {
            if (item != null && item.Any())
                return new AttributeValue { NS = item.Select(x => x.ToString()).ToList() };

            return new AttributeValue { NULL = true };
        }
    }

    internal class UnsignedShortArrayConverter : DynamoAttributeValueConverter<ushort[]>
    {
        private readonly UnsignedShortEnumerableConverter _converter;
        public UnsignedShortArrayConverter()
        {
            _converter = new UnsignedShortEnumerableConverter();
        }

        public override ushort[] Read(AttributeValue value) => _converter.Read(value).ToArray();
        public override AttributeValue Write(ushort[] item) => _converter.Write(item);
    }

    internal class UnsignedShortListConverter : DynamoAttributeValueConverter<List<ushort>>
    {
        private readonly UnsignedShortEnumerableConverter _converter;
        public UnsignedShortListConverter()
        {
            _converter = new UnsignedShortEnumerableConverter();
        }

        public override List<ushort> Read(AttributeValue value) => _converter.Read(value).ToList();
        public override AttributeValue Write(List<ushort> item) => _converter.Write(item);
    }
}
