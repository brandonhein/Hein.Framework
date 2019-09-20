using Xunit;

namespace Hein.Framework.Serialization.Tests
{
    public class XmlUtilitiesTests
    {
        [Fact]
        public void Should_format_xml_with_indents_then_compess_it_back()
        {
            var prettyXml = XmlUtilities.FormatXml(ExpectedOutcome.FarmA_Xml);
            var containsIndents = prettyXml.Contains("\r\n");
            Assert.True(containsIndents);

            var compressedXml = prettyXml.CompressXml();
            containsIndents = compressedXml.Contains("\r\n");
            Assert.False(containsIndents);
            Assert.Equal(ExpectedOutcome.FarmA_Xml, compressedXml);
        }

        [Fact]
        public void Should_format_json_with_indents_then_compess_it_back()
        {
            var prettyJson = JsonUtilities.FormatJson(ExpectedOutcome.FarmA_Json1);
            var containsIndents = prettyJson.Contains("\r\n");
            Assert.True(containsIndents);

            var compressedJson = prettyJson.CompressJson();
            containsIndents = compressedJson.Contains("\r\n");
            Assert.False(containsIndents);
            Assert.Equal(ExpectedOutcome.FarmA_Json1, compressedJson);
        }
    }
}
