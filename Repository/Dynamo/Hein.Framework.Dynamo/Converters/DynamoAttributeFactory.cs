using Amazon.DynamoDBv2.Model;
using Hein.Framework.Dynamo.Converters.Collection;
using Hein.Framework.Dynamo.Converters.Value;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Hein.Framework.Dynamo.Tests")]
namespace Hein.Framework.Dynamo.Converters
{
    internal static class DynamoAttributeFactory
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

            new ObjectListConverter(),
            new ObjectEnumerableConverter(),
            new ObjectArrayConverter(),

            new ObjectConverter(),
        };

        public static bool HasConverter(Type itemType)
        {
            var converter = _converters.Reverse().FirstOrDefault(x => x.CanConvert(itemType));
            return converter != null;
        }

        public static object GetConverter(Type itemType)
        {
            var converter = _converters.Reverse().FirstOrDefault(x => x.CanConvert(itemType));
            if (converter == null)
                return new ObjectConverter();

            return converter;
        }

        public static object Read(AttributeValue value)
        {
            foreach (var converter in _converters.Reverse())
            {
                try
                {
                    dynamic result = converter.Read(value);
                    if (result is IEnumerable)
                    { }
                    else if (result != null)
                        return result;
                }
                catch
                { }
            }

            return default;
        }

        public static object Read(AttributeValue value, Type itemType)
        {
            dynamic converter = GetConverter(itemType);
            if (converter != null)
                return converter.Read(value);

            return default;
        }

        public static AttributeValue Create(object item)
        {
            var converter = GetConverter(item.GetType());
            return Create(item, converter);
        }

        public static AttributeValue Create(object item, dynamic converter)
        {
            return converter.Write(converter.Convert(item));
        }
    }
}
