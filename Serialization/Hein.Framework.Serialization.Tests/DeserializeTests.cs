using System.Linq;
using Xunit;

namespace Hein.Framework.Serialization.Tests
{
    public class DeserializeTests
    {
        [Fact]
        public void Should_deserialize_a_default_json_to_object()
        {
            var result = Deserialize.FromJson<Farm>(ExpectedOutcome.FarmA_Json1);
            var farm = TestObject.FarmA;

            Assert.Equal(farm.Tractors.FirstOrDefault().Type, result.Tractors.FirstOrDefault().Type);
        }

        [Fact]
        public void Should_deserialize_a_camelcase_json_to_object()
        {
            var result = Deserialize.FromJson<Farm>(ExpectedOutcome.FarmA_Json2);
            var farm = TestObject.FarmA;

            Assert.Equal(farm.Tractors.FirstOrDefault().Type, result.Tractors.FirstOrDefault().Type);
        }

        [Fact]
        public void Should_deserialize_xml_to_object()
        {
            var result = Deserialize.FromXml<Farm>(ExpectedOutcome.FarmA_Xml);
            var farm = TestObject.FarmA;

            Assert.Equal(farm.Tractors.FirstOrDefault().Type, result.Tractors.FirstOrDefault().Type);
        }

        [Fact]
        public void Should_deserialize_soap_xml_to_object()
        {
            var result = Deserialize.FromSoapXml<Farm>(ExpectedOutcome.FarmA_Soap);
            var farm = TestObject.FarmA;

            Assert.Equal(farm.Tractors.FirstOrDefault().Type, result.Tractors.FirstOrDefault().Type);
        }
    }
}
