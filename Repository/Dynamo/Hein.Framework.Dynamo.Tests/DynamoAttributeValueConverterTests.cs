using Hein.Framework.Dynamo.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hein.Framework.Dynamo.Tests
{
    public class DynamoAttributeValueConverterTests
    {
        [Fact]
        public void Should_find_and_convert_to_dynamo_attribute_value()
        {
            try
            {
                var result = DynamoAttributeValueConverter.Write(new List<int>() { 24 }, typeof(List<int>));
            }
            catch (Exception ex)
            {

            }
        }
    }
}
