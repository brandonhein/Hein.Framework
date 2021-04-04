﻿using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;
using System.Linq;

namespace Hein.Framework.Dynamo.Converters.Collection
{
    internal class UnsignedIntegerEnumerableConverter : DynamoAttributeValueConverter<IEnumerable<uint>>
    {
        public override IEnumerable<uint> Read(AttributeValue value)
        {
            if (value.NULL)
                return new List<uint>();

            return value.NS.Select(x => uint.Parse(x));
        }

        public override AttributeValue Write(IEnumerable<uint> item)
        {
            if (item != null && item.Any())
                return new AttributeValue { NS = item.Select(x => x.ToString()).ToList() };

            return new AttributeValue { NULL = true };
        }
    }

    internal class UnsignedIntegerArrayConverter : DynamoAttributeValueConverter<uint[]>
    {
        private readonly UnsignedIntegerEnumerableConverter _converter;
        public UnsignedIntegerArrayConverter()
        {
            _converter = new UnsignedIntegerEnumerableConverter();
        }

        public override uint[] Read(AttributeValue value) => _converter.Read(value).ToArray();
        public override AttributeValue Write(uint[] item) => _converter.Write(item);
    }

    internal class UnsignedIntegerListConverter : DynamoAttributeValueConverter<List<uint>>
    {
        private readonly UnsignedIntegerEnumerableConverter _converter;
        public UnsignedIntegerListConverter()
        {
            _converter = new UnsignedIntegerEnumerableConverter();
        }

        public override List<uint> Read(AttributeValue value) => _converter.Read(value).ToList();
        public override AttributeValue Write(List<uint> item) => _converter.Write(item);
    }
}
