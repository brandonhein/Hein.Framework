using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;

namespace Hein.Framework.Dynamo.Converters
{
    internal class DictionaryConverter : DynamoAttributeValueConverter<Dictionary<string, object>>
    {
        public override Dictionary<string, object> Read(AttributeValue value)
        {
            throw new NotImplementedException();
        }

        public override AttributeValue Write(Dictionary<string, object> item)
        {
            throw new NotImplementedException();
        }
    }

    internal class IDictionaryConverter : DynamoAttributeValueConverter<IDictionary<string, object>>
    {
        public override IDictionary<string, object> Read(AttributeValue value)
        {
            throw new NotImplementedException();
        }

        public override AttributeValue Write(IDictionary<string, object> item)
        {
            throw new NotImplementedException();
        }
    }
}
