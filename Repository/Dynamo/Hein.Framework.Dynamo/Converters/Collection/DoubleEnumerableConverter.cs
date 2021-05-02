using Amazon.DynamoDBv2.Model;
using Hein.Framework.Dynamo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hein.Framework.Dynamo.Converters.Collection
{
    internal class DoubleEnumerableConverter : DynamoAttributeValueConverter<IEnumerable<double>>
    {
        public override bool CanConvert(Type typeToConvert) => base.CanConvert(typeToConvert) || typeToConvert.IsOneOf(typeof(ICollection<double>), typeof(IReadOnlyCollection<double>));
        public override IEnumerable<double> Read(AttributeValue value)
        {
            if (value.NULL)
                return new List<double>();

            return value.NS.Select(x => double.Parse(x));
        }

        public override AttributeValue Write(IEnumerable<double> item)
        {
            if (item != null && item.Any())
                return new AttributeValue { NS = item.Select(x => x.ToString()).ToList() };

            return new AttributeValue { NULL = true };
        }
    }

    internal class DoubleArrayConverter : DynamoAttributeValueConverter<double[]>
    {
        private readonly DoubleEnumerableConverter _converter;
        public DoubleArrayConverter()
        {
            _converter = new DoubleEnumerableConverter();
        }

        internal override double[] Convert(object value) => ((IEnumerable<double>)value).Cast<double>().ToArray();
        public override double[] Read(AttributeValue value) => _converter.Read(value).ToArray();
        public override AttributeValue Write(double[] item) => _converter.Write(item);
    }

    internal class DoubleListConverter : DynamoAttributeValueConverter<List<double>>
    {
        private readonly DoubleEnumerableConverter _converter;
        public DoubleListConverter()
        {
            _converter = new DoubleEnumerableConverter();
        }

        public override bool CanConvert(Type typeToConvert) => base.CanConvert(typeToConvert) || typeToConvert.IsOneOf(typeof(IList<double>), typeof(IReadOnlyList<double>));
        internal override List<double> Convert(object value) => ((IEnumerable<double>)value).Cast<double>().ToList();
        public override List<double> Read(AttributeValue value) => _converter.Read(value).ToList();
        public override AttributeValue Write(List<double> item) => _converter.Write(item);
    }
}
