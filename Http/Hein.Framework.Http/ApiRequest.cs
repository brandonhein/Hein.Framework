using System.Collections.Generic;

namespace Hein.Framework.Http
{
    public class ApiRequest
    {
        public string BaseUrl { get; set; }
        public string Path { get; set; }
        public IDictionary<string, string> QueryParameters { get; set; }
        public IDictionary<string, string> Headers { get; set; }
        public HttpMethod Method { get; set; }
        public string Accept { get; set; }
        public string ContentType { get; set; }
        public string Body { get; set; }
        public int Timeout { get; set; }
    }
}
