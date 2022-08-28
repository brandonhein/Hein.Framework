using Xunit;

namespace Hein.Framework.Extensions.Tests
{
    public class StringExtensionTests
    {
        [Theory]
        [InlineData("joHN dOe", "John Doe")]
        [InlineData("TORI-NADO", "Tori-Nado")]
        public void Should_captialize_the_words_in_string(string raw, string expected)
        {
            var result = raw.ToCapitalize();
            Assert.Equal(expected, result);
        }
    }
}
