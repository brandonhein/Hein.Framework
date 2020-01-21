using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Hein.Framework.Http
{
    public class ApiServiceBase
    {
        protected ApiServiceBase()
        { }

        protected ApiResponse GetResult(WebResponse response)
        {
            string responseString;
            string contentType;
            HttpStatusCode statusCode;
            IDictionary<string, string> responseHeaders = null;

            using (var httpResponse = (HttpWebResponse)response)
            using (var reader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var headerKeys = httpResponse.Headers.AllKeys;
                if (headerKeys != null && headerKeys.Any())
                {
                    responseHeaders = new Dictionary<string, string>();
                    foreach (var key in headerKeys)
                    {
                        var value = response.Headers[key];
                        responseHeaders.Add(key, value);
                    }
                }

                responseString = reader.ReadToEnd();
                contentType = httpResponse.ContentType;
                statusCode = httpResponse.StatusCode;
            }

            return new ApiResponse(responseString, contentType, responseHeaders, statusCode);
        }

        protected async Task<ApiResponse> GetResultAsync(WebResponse response)
        {
            string responseString;
            string contentType;
            HttpStatusCode statusCode;
            IDictionary<string, string> responseHeaders = null;

            using (var httpResponse = (HttpWebResponse)response)
            using (var reader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var headerKeys = httpResponse.Headers.AllKeys;
                if (headerKeys != null && headerKeys.Any())
                {
                    responseHeaders = new Dictionary<string, string>();
                    foreach (var key in headerKeys)
                    {
                        var value = response.Headers[key];
                        responseHeaders.Add(key, value);
                    }
                }

                responseString = await reader.ReadToEndAsync();
                contentType = httpResponse.ContentType;
                statusCode = httpResponse.StatusCode;
            }

            return new ApiResponse(responseString, contentType, responseHeaders, statusCode);
        }

        protected bool IsWriteableMethod(HttpMethod method)
        {
            return method == HttpMethod.Post || method == HttpMethod.Put ||
                   method == HttpMethod.Patch || method == HttpMethod.Delete ||
                   method == HttpMethod.Options || method == HttpMethod.Link ||
                   method == HttpMethod.Unlink || method == HttpMethod.Lock ||
                   method == HttpMethod.Propfind || method == HttpMethod.View;
        }

        protected HttpWebRequest GenerateWebRequest(ApiRequest request)
        {
            var url = Url.Combine(request.BaseUrl, request.Path);

            if (request.QueryParameters != null && request.QueryParameters.Any())
            {
                url = string.Concat(url, "?", request.QueryParameters.ToUrlEncode());
            }

            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = request.Method.ToString().ToUpper();
            webRequest.ContentType = request.ContentType;
            webRequest.Accept = request.Accept;
            if (request.Headers != null && request.Headers.Any())
            {
                foreach (var header in request.Headers)
                {
                    webRequest.Headers.Add(header.Key, header.Value);
                }
            }

            if (request.Timeout <= 0)
            {
                webRequest.Timeout = 30000;
            }
            else
            {
                webRequest.Timeout = request.Timeout;
            }

            return webRequest;
        }
    }
}
