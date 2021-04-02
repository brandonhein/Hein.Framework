using Hein.Framework.Serialization.Attributes;
using System.Text.Json.Serialization;
using Xunit;

namespace Hein.Framework.Serialization.Tests.Converters
{
    public class JsonPropertyOrderConverterTests
    {
        [Fact]
        public void Should_order_json_properties_correctly_due_to_order_attribute()
        {
            var sample = new SampleOrderClass() { First = "first", Middle = "middle", Last = "last" };

            var json = sample.ToJson();
            var expectedJson = "{\"FIRST\":\"first\",\"Middle\":\"middle\",\"last_value\":\"last\"}";
            Assert.Equal(expectedJson, json);
        }

        [Fact]
        public void Should_deserialize_json_correctly_order_doesnt_matter_here_but_do_name_check()
        {
            var json = "{\"FiRsT\":\"lightning\",\"miDDle\":\"thunder\",\"last_VALUE\":\"rain\"}";

            var sample = json.FromJson<SampleOrderClass>();

            Assert.Equal("lightning", sample.First);
            Assert.Equal("thunder", sample.Middle);
            Assert.Equal("rain", sample.Last);
        }
    }

    public class SampleOrderClass : SampleOrderClassBase
    {
        [JsonPropertyOrder(3)]
        [JsonPropertyName("last_value")]
        public string Last { get; set; }
        [JsonPropertyOrder(2)]
        public string Middle { get; set; }
    }

    public class SampleOrderClassBase
    {
        [JsonPropertyOrder(1)]
        [JsonPropertyName("FIRST")]
        public string First { get; set; }
    }
}
