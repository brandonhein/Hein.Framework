using System.Collections.Generic;
using System.Net;
using Xunit;

namespace Hein.Framework.Http.Tests
{
    public class Implementation
    {
        [Fact]
        public void Show_api_service_in_action()
        {
            var service = new ApiService();
            var request = new ApiRequest("https://1njp7ql9lb.execute-api.us-east-2.amazonaws.com/lookup/zipcode?codes=90210")
            {
                Method = HttpMethod.Get,
                Accept = HttpContentType.Json,
                Headers = new Dictionary<string, string>()
                {
                    { "x-api-key", "my-access-key" }
                },
                d
            };

            var result = service.New(request).Response();
            var contains90210 = result.ResponseString.Contains("90210");

            Assert.Equal(HttpContentType.Json, result.ContentType);
            Assert.Equal(8, result.Headers.Count);
            Assert.Equal(HttpStatusCode.OK, result.ResponseCode);
            Assert.True(contains90210);
        }
    }
}
