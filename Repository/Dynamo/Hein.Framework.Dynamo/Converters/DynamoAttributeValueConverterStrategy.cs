using Amazon.DynamoDBv2.Model;
using Hein.Framework.Dynamo.Converters.Collection;
using Hein.Framework.Dynamo.Converters.Value;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Hein.Framework.Dynamo.Tests")]
namespace Hein.Framework.Dynamo.Converters
{
    internal static class DynamoAttributeValueConverter
    {
        private static ConcurrentBag<dynamic> _converters = new ConcurrentBag<dynamic>()
        {
            //Value
            new StringConverter(),
            new BooleanConverter(),
            new NullableBooleanConverter(),
            new ByteArrayConverter(),
            new ByteConverter(),
            new NullableByteConverter(),
            new CharacterConverter(),
            new NullableCharacterConverter(),
            new DateTimeConverter(),
            new NullableDateTimeConverter(),
            new DateTimeOffsetConverter(),
            new NullableDateTimeOffsetConverter(),
            new DecimalConverter(),
            new NullableDecimalConverter(),
            new FloatConverter(),
            new NullableFloatConverter(),
            new GuidConverter(),
            new NullableGuidConverter(),
            new IntegerConverter(),
            new NullableIntegerConverter(),
            new LongConverter(),
            new NullableLongConverter(),
            new MemoryStreamConverter(),
            new ShortConverter(),
            new NullableShortConverter(),
            new UnsignedIntegerConverter(),
            new NullableUnsignedIntegerConverter(),
            new UnsignedLongConverter(),
            new NullableUnsignedLongConverter(),
            new UnsignedShortConverter(),
            new NullableUnsignedShortConverter(),

            //Collection
            new DecimalEnumerableConverter(),
            new DecimalArrayConverter(),
            new DecimalListConverter(),
            new DoubleEnumerableConverter(),
            new DoubleArrayConverter(),
            new DoubleListConverter(),
            new FloatEnumerableConverter(),
            new FloatArrayConverter(),
            new FloatListConverter(),
            new IntegerEnumerableConverter(),
            new IntegerArrayConverter(),
            new IntegerListConverter(),
            new ShortEnumerableConverter(),
            new ShortArrayConverter(),
            new ShortListConverter(),
            new StringEnumerableConverter(),
            new StringArrayConverter(),
            new StringListConverter(),
            new UnsignedIntegerEnumerableConverter(),
            new UnsignedIntegerArrayConverter(),
            new UnsignedIntegerListConverter(),
            new UnsignedLongEnumerableConverter(),
            new UnsignedLongArrayConverter(),
            new UnsignedLongListConverter(),
            new UnsignedShortEnumerableConverter(),
            new UnsignedShortArrayConverter(),
            new UnsignedShortListConverter(),
        };

        public static object Read(AttributeValue value, Type itemType)
        {
            //add custom converter attr check

            var converter = _converters.FirstOrDefault(x => x.CanConvert(itemType));
            if (converter != null)
            {
                return converter.Read(value);
            }

            throw new Exception();
        }

        public static AttributeValue Write(object item, Type itemType)
        {
            //add custom converter attr check
            var converter = _converters.FirstOrDefault(x => x.CanConvert(itemType));
            if (converter != null)
            {
                return converter.Write(converter.Convert(item));
            }

            throw new Exception("I thru this!");
        }
    }
}
