using Amazon.DynamoDBv2.Model;
using Hein.Framework.Dynamo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hein.Framework.Dynamo.Converters.Collection
{
    internal class UnsignedShortEnumerableConverter : DynamoAttributeValueConverter<IEnumerable<ushort>>
    {
        public override bool CanConvert(Type typeToConvert) => base.CanConvert(typeToConvert) || typeToConvert.IsOneOf(typeof(ICollection<ushort>), typeof(IReadOnlyCollection<ushort>));
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

        internal override ushort[] Convert(object value) => ((IEnumerable<ushort>)value).Cast<ushort>().ToArray();
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

        public override bool CanConvert(Type typeToConvert) => base.CanConvert(typeToConvert) || typeToConvert.IsOneOf(typeof(IList<ulong>), typeof(IReadOnlyList<ulong>));
        internal override List<ushort> Convert(object value) => ((IEnumerable<ushort>)value).Cast<ushort>().ToList();
        public override List<ushort> Read(AttributeValue value) => _converter.Read(value).ToList();
        public override AttributeValue Write(List<ushort> item) => _converter.Write(item);
    }
}
