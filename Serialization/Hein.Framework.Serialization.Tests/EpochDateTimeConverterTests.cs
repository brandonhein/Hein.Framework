using Hein.Framework.Serialization.Converters;
using System;
using System.Text.Json.Serialization;
using Xunit;

namespace Hein.Framework.Serialization.Tests
{
    public class EpochDateTimeConverterTests
    {
        private DateTime _marchFirstTwentyTwentyOne = new DateTime(2021, 3, 1, 0, 0, 0, DateTimeKind.Utc);
        private long _epochTime = 1614556800;
        private string _json = "{\"Value\":1614556800}";

        [Fact]
        public void Should_serailize_date_time_to_epoch_second_value()
        {
            var test = new SampleClassWithDateTime()
            {
                Value = _marchFirstTwentyTwentyOne
            };

            var json = test.ToJson();
            Assert.Equal(_json, json);
        }

        [Theory]
        [InlineData("{\"Value\":1614556800}")]
        [InlineData("{\"Value\":1614556800.00}")]
        [InlineData("{\"Value\":\"1614556800\"}")]
        [InlineData("{\"Value\":\"1614556800.00\"}")]
        public void Should_deserialize_epoch_number_value_to_date_time(string json)
        {
            var result = json.FromJson<SampleClassWithDateTime>();
        
            Assert.Equal(result.Value.Year, _marchFirstTwentyTwentyOne.Year);
            Assert.Equal(result.Value.Month, _marchFirstTwentyTwentyOne.Month);
            Assert.Equal(result.Value.Day, _marchFirstTwentyTwentyOne.Day);
        }
    }

    public class SampleClassWithDateTime
    {
        [JsonConverter(typeof(EpochDateTimeConverter))]
        public DateTime Value { get; set; }
    }
}
