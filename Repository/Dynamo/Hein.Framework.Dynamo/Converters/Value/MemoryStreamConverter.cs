using Amazon.DynamoDBv2.Model;
using System.IO;

namespace Hein.Framework.Dynamo.Converters.Value
{
    internal class MemoryStreamConverter : DynamoAttributeValueConverter<MemoryStream>
    {
        public override MemoryStream Read(AttributeValue value)
        {
            if (value.NULL)
                return default(MemoryStream);

            return value.B;
        }

        public override AttributeValue Write(MemoryStream item)
        {
            return new AttributeValue { B = item };
        }
    }
}
