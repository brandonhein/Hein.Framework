using Hein.Framework.Serialization.Converters;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;
using Xunit;

namespace Hein.Framework.Serialization.Tests
{
    public class MemoryStreamConverterTests
    {
        private const string _encodedValue = "U29uaWMgdGhlIEhlZGdlaG9nIGlzIGJldHRlciB0aGFuIEZsYXNo";
        private const string _sampleData = "Sonic the Hedgehog is better than Flash";

        [Fact]
        public void Should_serialize_object_with_memory_stream_to_encoded_string()
        {
            var sample = new SampleObject()
            {
                Stream = new MemoryStream(Encoding.UTF8.GetBytes(_sampleData))
            };

            var json = sample.ToJson();
            var expectedJson = string.Concat("{\"Stream\":\"", _encodedValue, "\"}");
            Assert.Equal(expectedJson, json);
        }

        [Fact]
        public void Should_deserialize_object_memeory_stream_from_encoded_string()
        {
            var json = string.Concat("{\"Stream\":\"", _encodedValue, "\"}");

            var sample = json.FromJson<SampleObject>();
            var result = Encoding.UTF8.GetString(sample.Stream.ToArray());

            Assert.Equal(_sampleData, result);
        }
    }

    public class SampleObject
    {
        [JsonConverter(typeof(MemoryStreamConverter))]
        public MemoryStream Stream { get; set; }
    }
}
