using Amazon.DynamoDBv2.Model;
using Hein.Framework.Dynamo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hein.Framework.Dynamo.Converters.Collection
{
    internal class ObjectEnumerableConverter : DynamoAttributeValueConverter<IEnumerable<object>>
    {
        public override bool CanConvert(Type typeToConvert) => 
            typeToConvert.Name.IsOneOf("IEnumerable`1", "ICollection`1", "IReadOnlyCollection`1") || base.CanConvert(typeToConvert);

        public override IEnumerable<object> Read(AttributeValue value)
        {
            if (value.NULL)
                return new List<object>();

            if (value.IsLSet)
            {
                var result = new List<object>();
                foreach (var item in value.L)
                {
                    result.Add(DynamoAttributeFactory.Read(item));
                }

                return result;
            }

            return new List<object>();
        }

        public override AttributeValue Write(IEnumerable<object> item)
        {
            if (item != null && item.Any())
            {
                var result = new List<AttributeValue>();
                foreach (var value in item)
                {
                    result.Add(DynamoAttributeFactory.Create(value));
                }

                return new AttributeValue { L = result };
            }

            return new AttributeValue { NULL = true };
        }
    }

    internal class ObjectListConverter : DynamoAttributeValueConverter<List<object>>
    {
        private readonly ObjectEnumerableConverter _converter;
        public ObjectListConverter()
        {
            _converter = new ObjectEnumerableConverter();
        }

        public override bool CanConvert(Type typeToConvert) => 
            typeToConvert.Name.IsOneOf("List`1", "IList`1", "IReadOnlyList`1") || base.CanConvert(typeToConvert);
        internal override List<object> Convert(object value) => ((IEnumerable<object>)value).Cast<object>().ToList();

        public override List<object> Read(AttributeValue value) => _converter.Read(value).ToList();
        public override AttributeValue Write(List<object> item) => _converter.Write(item);
    }

    internal class ObjectArrayConverter : DynamoAttributeValueConverter<Array>
    {
        private readonly ObjectEnumerableConverter _converter;
        public ObjectArrayConverter()
        {
            _converter = new ObjectEnumerableConverter();
        }

        public override bool CanConvert(Type typeToConvert) => typeToConvert.IsArray &&
            !typeToConvert.IsOneOf(typeof(decimal[]), typeof(double[]), typeof(float[]), typeof(int[]), typeof(short[]), typeof(string[]), typeof(uint[]), typeof(ulong[]), typeof(ushort[]), typeof(byte[]) );
        internal override Array Convert(object value) => ((IEnumerable<object>)value).Cast<object>().ToArray();

        public override Array Read(AttributeValue value) => _converter.Read(value).ToArray();
        public override AttributeValue Write(Array item) => _converter.Write(item.Cast<object>());
    }
}
