using System.Collections.Generic;

namespace Hein.Framework.Http
{
    public class ApiRequest
    {
        /// <summary>
        /// Straight up GET Request without any headers
        /// </summary>
        /// <param name="url"></param>
        public ApiRequest(string url)
        {
            Url = url;
            Headers = null;
            Method = HttpMethod.Get;
            Accept = null;
            ContentType = null;
            RequestBody = null;
        }

        /// <summary>
        /// Generate a GET Request with headers
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        public ApiRequest(string url, IDictionary<string, string> headers)
        {
            Url = url;
            Headers = headers;
            Method = HttpMethod.Get;
            Accept = null;
            ContentType = null;
            RequestBody = null;
        }

        /// <summary>
        /// Generate an Api Request without headers.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name=""></param>
        public ApiRequest(string url, HttpMethod method)
        {
            Url = url;
            Headers = null;
            Method = method;
            Accept = null;
            ContentType = null;
            RequestBody = null;
        }

        /// <summary>
        /// Generate an Api Request with headers.  
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name=""></param>
        public ApiRequest(string url, HttpMethod method, IDictionary<string, string> headers)
        {
            Url = url;
            Headers = headers;
            Method = method;
            Accept = null;
            ContentType = null;
            RequestBody = null;
        }

        /// <summary>
        /// Generate an Api Request without headers.  And allow you to inform the api to return a specifc way
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="accept"></param>
        public ApiRequest(string url, HttpMethod method, string accept)
        {
            Url = url;
            Headers = null;
            Method = method;
            Accept = accept;
            ContentType = null;
            RequestBody = null;
        }

        /// <summary>
        /// Generate an Api Request with headers.  And allow you to inform the api to return results a specific way
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="accept"></param>
        /// <param name="headers"></param>
        public ApiRequest(string url, HttpMethod method, string accept, IDictionary<string, string> headers)
        {
            Url = url;
            Headers = headers;
            Method = method;
            Accept = accept;
            ContentType = null;
            RequestBody = null;
        }

        /// <summary>
        /// Generate an Api PUT/POST Request without headers.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="accept"></param>
        /// <param name="contentType"></param>
        /// <param name="requestBody"></param>
        public ApiRequest(string url, HttpMethod method, string accept, string contentType, string requestBody)
        {
            Url = url;
            Headers = null;
            Method = method;
            Accept = accept;
            ContentType = contentType;
            RequestBody = requestBody;
        }

        /// <summary>
        /// Generate an Api Request with all the details you want to work with
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="accept"></param>
        /// <param name="contentType"></param>
        /// <param name="requestBody"></param>
        /// <param name="headers"></param>
        public ApiRequest(string url, HttpMethod method, string accept, string contentType, string requestBody, IDictionary<string, string> headers)
        {
            Url = url;
            Headers = headers;
            Method = method;
            Accept = accept;
            ContentType = contentType;
            RequestBody = requestBody;
        }

        public string Url { get; private set; }
        public IDictionary<string, string> Headers { get; set; }
        public HttpMethod Method { get; set; }
        public string Accept { get; set; }
        public string ContentType { get; set; }
        public string RequestBody { get; set; }
    }
}
