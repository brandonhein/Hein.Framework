using Newtonsoft.Json.Serialization;
using Xunit;

namespace Hein.Framework.Serialization.Tests
{
    public class SerializeTests
    {
        private readonly Farm _myFarm;
        public SerializeTests()
        {
            _myFarm = TestObject.FarmA;
        }

        [Fact]
        public void Should_serialize_my_farm_object_to_json_with_default_json_settings()
        {
            var json = _myFarm.ToJson();
            Assert.Equal(ExpectedOutcome.FarmA_Json1, json);
        }

        [Fact]
        public void Should_serialize_my_farm_object_to_json_with_camel_case_json_settings()
        {
            var json = _myFarm.ToJson(new CamelCasePropertyNamesContractResolver());
            Assert.Equal(ExpectedOutcome.FarmA_Json2, json);
        }

        [Fact]
        public void Should_serialize_my_farm_object_to_xml()
        {
            var xml = _myFarm.ToXml();
            Assert.Equal(ExpectedOutcome.FarmA_Xml, xml);
        }

        [Fact]
        public void Should_serialize_my_farm_object_to_soap()
        {
            var soap = _myFarm.ToSoapXml();
            Assert.Equal(ExpectedOutcome.FarmA_Soap, soap);
        }
    }
}
