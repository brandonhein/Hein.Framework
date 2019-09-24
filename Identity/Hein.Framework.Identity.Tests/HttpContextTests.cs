using Moq;
using System.Security.Principal;
using Xunit;

namespace Hein.Framework.Identity.Tests
{
    public class HttpContextTests
    {
        public HttpContextTests()
        {
            var mockIdentity = new Mock<IIdentity>();
            mockIdentity.Setup(x => x.Name).Returns("Company\\Brandon");

            var mockHttpContext = new Mock<Microsoft.AspNetCore.Http.HttpContext>();
            mockHttpContext.Setup(x => x.User).Returns(new System.Security.Claims.ClaimsPrincipal(mockIdentity.Object));

            HttpContext.Set(mockHttpContext.Object);
        }

        [Fact]
        public void Should_get_the_username_from_context()
        {
            var userName = HttpContext.Current.User.Identity.Name;
            Assert.Equal("Company\\Brandon", userName);
            HttpContext.Flush();
        }
    }
}
