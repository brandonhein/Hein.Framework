using Hein.Framework.Serialization.Attributes;
using System;
using Xunit;

namespace Hein.Framework.Serialization.Tests.Converters
{
    public class JsonVersionConverterTests
    {
        [Fact]
        public void Should_serialize_with_attribute_version()
        {
            var json = new VersionSample().ToJson();

            var expectedJson = "{\"Version\":2,\"Name\":\"Look!!!\",\"WithoutSample\":{\"Result\":true},\"AnotherSample\":{\"EventVersion\":\"1.3\",\"Christmas\":\"2010-12-25T00:00:00Z\"}}";
            Assert.Equal(expectedJson, json);
        }

        [Fact]
        public void Should_deserailize_just_fine_even_tho_version_properties_are_associated_in_json()
        {
            var json = "{\"Version\":2,\"Name\":\"Sonic\",\"WithoutSample\":{\"Result\":false},\"AnotherSample\":{\"EventVersion\":\"1.3\",\"Christmas\":\"2012-12-25T00:00:00Z\"}}";

            var sample = json.FromJson<VersionSample>();
            Assert.Equal("Sonic", sample.Name);
            Assert.False(sample.WithoutSample.Result);
            Assert.Equal(2012, sample.AnotherSample.Christmas.Year);
        }
    }

    [Version(2)]
    public class VersionSample
    {
        public VersionSample()
        {
            Name = "Look!!!";
            AnotherSample = new AnotherVersionSample();
            WithoutSample = new SampleWithoutVersion();
        }

        public string Name { get; set; }
        public SampleWithoutVersion WithoutSample { get; set; }
        public AnotherVersionSample AnotherSample { get; set; }
    }

    [EventVersion("1.3")]
    public class AnotherVersionSample
    {
        public AnotherVersionSample()
        {
            Christmas = new DateTime(2010, 12, 25, 0, 0, 0, DateTimeKind.Utc);
        }

        public DateTime Christmas { get; set; }
    }

    public class SampleWithoutVersion
    {
        public SampleWithoutVersion()
        {
            Result = true;
        }

        public bool Result { get; set; }
    }
}
