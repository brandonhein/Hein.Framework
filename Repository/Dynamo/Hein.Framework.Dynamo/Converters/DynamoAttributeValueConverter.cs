using Amazon.DynamoDBv2.Model;
using System;

namespace Hein.Framework.Dynamo.Converters
{
    public interface IDynamoAttributeConverter<T>
    {
        bool CanConvert(Type typeToConvert);
        T Read(AttributeValue value);
        AttributeValue Write(T item);
    }

    public abstract class DynamoAttributeValueConverter<T> : IDynamoAttributeConverter<T>
    {
        public virtual bool CanConvert(Type typeToConvert) => typeof(T) == typeToConvert;
        internal T Convert(object value) => (T)value;
        
        public abstract T Read(AttributeValue value);
        public abstract AttributeValue Write(T item);
    }
}
