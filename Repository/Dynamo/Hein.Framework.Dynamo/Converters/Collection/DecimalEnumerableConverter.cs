using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;
using System.Linq;

namespace Hein.Framework.Dynamo.Converters.Collection
{
    internal class DecimalEnumerableConverter : DynamoAttributeValueConverter<IEnumerable<decimal>>
    {
        public override IEnumerable<decimal> Read(AttributeValue value)
        {
            if (value.NULL)
                return new List<decimal>();

            return value.NS.Select(x => decimal.Parse(x));
        }

        public override AttributeValue Write(IEnumerable<decimal> item)
        {
            if (item != null && item.Any())
                return new AttributeValue { NS = item.Select(x => x.ToString()).ToList() };

            return new AttributeValue { NULL = true };
        }
    }

    internal class DecimalArrayConverter : DynamoAttributeValueConverter<decimal[]>
    {
        private readonly DecimalEnumerableConverter _converter;
        public DecimalArrayConverter()
        {
            _converter = new DecimalEnumerableConverter();
        }

        internal override decimal[] Convert(object value) => ((IEnumerable<decimal>)value).Cast<decimal>().ToArray();
        public override decimal[] Read(AttributeValue value) => _converter.Read(value).ToArray();
        public override AttributeValue Write(decimal[] item) => _converter.Write(item);
    }

    internal class DecimalListConverter : DynamoAttributeValueConverter<List<decimal>>
    {
        private readonly DecimalEnumerableConverter _converter;
        public DecimalListConverter()
        {
            _converter = new DecimalEnumerableConverter();
        }

        internal override List<decimal> Convert(object value) => ((IEnumerable<decimal>)value).Cast<decimal>().ToList();
        public override List<decimal> Read(AttributeValue value) => _converter.Read(value).ToList();
        public override AttributeValue Write(List<decimal> item) => _converter.Write(item);
    }
}
