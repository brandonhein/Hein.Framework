using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;
using System.Linq;

namespace Hein.Framework.Dynamo.Converters.Collection
{
    internal class UnsignedLongEnumerableConverter : DynamoAttributeValueConverter<IEnumerable<ulong>>
    {
        public override IEnumerable<ulong> Read(AttributeValue value)
        {
            if (value.NULL)
                return new List<ulong>();

            return value.NS.Select(x => ulong.Parse(x));
        }

        public override AttributeValue Write(IEnumerable<ulong> item)
        {
            if (item != null && item.Any())
                return new AttributeValue { NS = item.Select(x => x.ToString()).ToList() };

            return new AttributeValue { NULL = true };
        }
    }

    internal class UnsignedLongArrayConverter : DynamoAttributeValueConverter<ulong[]>
    {
        private readonly UnsignedLongEnumerableConverter _converter;
        public UnsignedLongArrayConverter()
        {
            _converter = new UnsignedLongEnumerableConverter();
        }

        public override ulong[] Read(AttributeValue value) => _converter.Read(value).ToArray();
        public override AttributeValue Write(ulong[] item) => _converter.Write(item);
    }

    internal class UnsignedLongListConverter : DynamoAttributeValueConverter<List<ulong>>
    {
        private readonly UnsignedLongEnumerableConverter _converter;
        public UnsignedLongListConverter()
        {
            _converter = new UnsignedLongEnumerableConverter();
        }

        public override List<ulong> Read(AttributeValue value) => _converter.Read(value).ToList();
        public override AttributeValue Write(List<ulong> item) => _converter.Write(item);
    }
}
