using Amazon.DynamoDBv2.DataModel;
using Hein.Framework.Dynamo.Converters;
using System;

namespace Hein.Framework.Dynamo.Attributes
{
    public class DynamoConverterAttribute : DynamoDBAttribute
    {
        /// <summary>
        /// Custom Dynamo <see cref="AttributeValue"/> converter.
        /// <para>must inherit <see cref="DynamoAttributeValueConverter{T}"/> as a base class</para>
        /// </summary>
        /// <param name="converterType"></param>
        public DynamoConverterAttribute(Type converterType)
        {
            if (converterType == typeof(IDynamoAttributeValueConverter<>))
            {
                ConverterType = converterType;
            }
            else
            {
                throw new Exception();
            }
        }

        public Type ConverterType { get; }
    }
}
