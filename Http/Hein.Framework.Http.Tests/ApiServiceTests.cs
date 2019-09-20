using Xunit;

namespace Hein.Framework.Http.Tests
{
    public class ApiServiceTests
    {
        [Fact]
        public void Should_clone_the_same_implementation_when_calling_new()
        {
            var service = new ApiService();
            var result = service.New(new ApiRequest("url"));

            Assert.IsType<ApiService>(result);
        }
    }
}
