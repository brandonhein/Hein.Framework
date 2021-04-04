using Amazon.DynamoDBv2.Model;
using System;

namespace Hein.Framework.Dynamo.Converters
{
    public interface IDynamoAttributeValueConverter<T>
    {
        bool CanConvert(Type typeToConvert);
        T Read(AttributeValue value);
        AttributeValue Write(T item);
    }

    public abstract class DynamoAttributeValueConverter<T> : IDynamoAttributeValueConverter<T>
    {
        public virtual bool CanConvert(Type typeToConvert) => typeof(T) == typeToConvert;
        internal virtual T Convert(object value) => (T)value;

        public abstract T Read(AttributeValue value);
        public abstract AttributeValue Write(T item);
    }
}
