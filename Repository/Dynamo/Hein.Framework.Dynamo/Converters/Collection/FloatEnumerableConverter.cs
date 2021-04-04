using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;
using System.Linq;

namespace Hein.Framework.Dynamo.Converters.Collection
{
    internal class FloatEnumerableConverter : DynamoAttributeValueConverter<IEnumerable<float>>
    {
        public override IEnumerable<float> Read(AttributeValue value)
        {
            if (value.NULL)
                return new List<float>();

            return value.NS.Select(x => float.Parse(x));
        }

        public override AttributeValue Write(IEnumerable<float> item)
        {
            if (item != null && item.Any())
                return new AttributeValue { NS = item.Select(x => x.ToString()).ToList() };

            return new AttributeValue { NULL = true };
        }
    }

    internal class FloatArrayConverter : DynamoAttributeValueConverter<float[]>
    {
        private readonly FloatEnumerableConverter _converter;
        public FloatArrayConverter()
        {
            _converter = new FloatEnumerableConverter();
        }

        public override float[] Read(AttributeValue value) => _converter.Read(value).ToArray();
        public override AttributeValue Write(float[] item) => _converter.Write(item);
    }

    internal class FloatListConverter : DynamoAttributeValueConverter<List<float>>
    {
        private readonly FloatEnumerableConverter _converter;
        public FloatListConverter()
        {
            _converter = new FloatEnumerableConverter();
        }

        public override List<float> Read(AttributeValue value) => _converter.Read(value).ToList();
        public override AttributeValue Write(List<float> item) => _converter.Write(item);
    }
}
