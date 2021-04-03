using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;
using System.Linq;

namespace Hein.Framework.Dynamo.Converters
{
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


    internal class ShortArrayConverter : DynamoAttributeValueConverter<short[]>
    {
        public override short[] Read(AttributeValue value)
        {
            throw new System.NotImplementedException();
        }

        public override AttributeValue Write(short[] item)
        {
            throw new System.NotImplementedException();
        }
    }

    internal class ShortListConverter : DynamoAttributeValueConverter<List<short>>
    {
        public override List<short> Read(AttributeValue value)
        {
            throw new System.NotImplementedException();
        }

        public override AttributeValue Write(List<short> item)
        {
            throw new System.NotImplementedException();
        }
    }

    internal class ShortEnumerableConverter : DynamoAttributeValueConverter<IEnumerable<short>>
    {
        public override IEnumerable<short> Read(AttributeValue value)
        {
            throw new System.NotImplementedException();
        }

        public override AttributeValue Write(IEnumerable<short> item)
        {
            throw new System.NotImplementedException();
        }
    }


    internal class FloatArrayConverter : DynamoAttributeValueConverter<float[]>
    {
        public override float[] Read(AttributeValue value)
        {
            throw new System.NotImplementedException();
        }

        public override AttributeValue Write(float[] item)
        {
            throw new System.NotImplementedException();
        }
    }

    internal class FloatListConverter : DynamoAttributeValueConverter<List<float>>
    {
        public override List<float> Read(AttributeValue value)
        {
            throw new System.NotImplementedException();
        }

        public override AttributeValue Write(List<float> item)
        {
            throw new System.NotImplementedException();
        }
    }

    internal class FloatEnumerableConverter : DynamoAttributeValueConverter<IEnumerable<float>>
    {
        public override IEnumerable<float> Read(AttributeValue value)
        {
            throw new System.NotImplementedException();
        }

        public override AttributeValue Write(IEnumerable<float> item)
        {
            throw new System.NotImplementedException();
        }
    }


    internal class DecimalArrayConverter : DynamoAttributeValueConverter<decimal[]>
    {
        public override decimal[] Read(AttributeValue value)
        {
            throw new System.NotImplementedException();
        }

        public override AttributeValue Write(decimal[] item)
        {
            throw new System.NotImplementedException();
        }
    }

    internal class DecimalListConverter : DynamoAttributeValueConverter<List<decimal>>
    {
        public override List<decimal> Read(AttributeValue value)
        {
            throw new System.NotImplementedException();
        }

        public override AttributeValue Write(List<decimal> item)
        {
            throw new System.NotImplementedException();
        }
    }

    internal class DecimalEnumerableConverter : DynamoAttributeValueConverter<IEnumerable<decimal>>
    {
        public override IEnumerable<decimal> Read(AttributeValue value)
        {
            throw new System.NotImplementedException();
        }

        public override AttributeValue Write(IEnumerable<decimal> item)
        {
            throw new System.NotImplementedException();
        }
    }


    internal class DoubleArrayConverter : DynamoAttributeValueConverter<double[]>
    {
        public override double[] Read(AttributeValue value)
        {
            throw new System.NotImplementedException();
        }

        public override AttributeValue Write(double[] item)
        {
            throw new System.NotImplementedException();
        }
    }

    internal class DoubleListConverter : DynamoAttributeValueConverter<List<double>>
    {
        public override List<double> Read(AttributeValue value)
        {
            throw new System.NotImplementedException();
        }

        public override AttributeValue Write(List<double> item)
        {
            throw new System.NotImplementedException();
        }
    }

    internal class DoubleEnumerableConverter : DynamoAttributeValueConverter<IEnumerable<double>>
    {
        public override IEnumerable<double> Read(AttributeValue value)
        {
            throw new System.NotImplementedException();
        }

        public override AttributeValue Write(IEnumerable<double> item)
        {
            throw new System.NotImplementedException();
        }
    }


    internal class UnsignedShortArrayConverter : DynamoAttributeValueConverter<ushort[]>
    {
        public override ushort[] Read(AttributeValue value)
        {
            throw new System.NotImplementedException();
        }

        public override AttributeValue Write(ushort[] item)
        {
            throw new System.NotImplementedException();
        }
    }

    internal class UnsignedShortListConverter : DynamoAttributeValueConverter<List<ushort>>
    {
        public override List<ushort> Read(AttributeValue value)
        {
            throw new System.NotImplementedException();
        }

        public override AttributeValue Write(List<ushort> item)
        {
            throw new System.NotImplementedException();
        }
    }

    internal class UnsignedShortEnumerableConverter : DynamoAttributeValueConverter<IEnumerable<ushort>>
    {
        public override IEnumerable<ushort> Read(AttributeValue value)
        {
            throw new System.NotImplementedException();
        }

        public override AttributeValue Write(IEnumerable<ushort> item)
        {
            throw new System.NotImplementedException();
        }
    }

    internal class UnsignedIntegerArrayConverter : DynamoAttributeValueConverter<uint[]>
    {
        public override uint[] Read(AttributeValue value)
        {
            throw new System.NotImplementedException();
        }

        public override AttributeValue Write(uint[] item)
        {
            throw new System.NotImplementedException();
        }
    }

    internal class UnsignedIntegerListConverter : DynamoAttributeValueConverter<List<uint>>
    {
        public override List<uint> Read(AttributeValue value)
        {
            throw new System.NotImplementedException();
        }

        public override AttributeValue Write(List<uint> item)
        {
            throw new System.NotImplementedException();
        }
    }

    internal class UnsignedIntegerEnumerableConverter : DynamoAttributeValueConverter<IEnumerable<uint>>
    {
        public override IEnumerable<uint> Read(AttributeValue value)
        {
            throw new System.NotImplementedException();
        }

        public override AttributeValue Write(IEnumerable<uint> item)
        {
            throw new System.NotImplementedException();
        }
    }

    internal class UnsignedLongArrayConverter : DynamoAttributeValueConverter<ulong[]>
    {
        public override ulong[] Read(AttributeValue value)
        {
            throw new System.NotImplementedException();
        }

        public override AttributeValue Write(ulong[] item)
        {
            throw new System.NotImplementedException();
        }
    }

    internal class UnsignedLongListConverter : DynamoAttributeValueConverter<List<ulong>>
    {
        public override List<ulong> Read(AttributeValue value)
        {
            throw new System.NotImplementedException();
        }

        public override AttributeValue Write(List<ulong> item)
        {
            throw new System.NotImplementedException();
        }
    }

    internal class UnsignedLongEnumerableConverter : DynamoAttributeValueConverter<IEnumerable<ulong>>
    {
        public override IEnumerable<ulong> Read(AttributeValue value)
        {
            throw new System.NotImplementedException();
        }

        public override AttributeValue Write(IEnumerable<ulong> item)
        {
            throw new System.NotImplementedException();
        }
    }
}
