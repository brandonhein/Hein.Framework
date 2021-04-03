using Amazon.DynamoDBv2.Model;

namespace Hein.Framework.Dynamo.Converters
{
    internal class StringConverter : DynamoAttributeValueConverter<string>
    {
        public override string Read(AttributeValue value) => value.S;
        public override AttributeValue Write(string item) => new AttributeValue { S = item };
    }
}
