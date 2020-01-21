using System;
using System.Collections.Generic;
using System.Net;

namespace Hein.Framework.Http
{
    public class ApiResponse
    {
        public ApiResponse(string responseString, string contentType, IDictionary<string, string> headers, HttpStatusCode code)
        {
            ResponseString = responseString;
            ContentType = contentType;
            Headers = headers;
            StatusCode = code;
        }

        public ApiResponse(Exception ex)
        {
            Exception = ex;
        }

        public string ResponseString { get; }
        public string ContentType { get; }
        public IDictionary<string, string> Headers { get; }
        public HttpStatusCode StatusCode { get; }
        public Exception Exception { get; }
    }
}
