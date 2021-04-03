using Amazon.DynamoDBv2.Model;

namespace Hein.Framework.Dynamo.Converters
{
    internal class ByteArrayConverter : DynamoAttributeValueConverter<byte[]>
    {
        public override byte[] Read(AttributeValue value)
        {
            if (!value.NULL)
                return value.B.ToArray();

            return default(byte[]);
        }

        public override AttributeValue Write(byte[] item) => new AttributeValue { B = new System.IO.MemoryStream(item) };
    }
}
