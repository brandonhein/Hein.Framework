using System.Collections.Generic;
using System.Net;

namespace Hein.Framework.Http
{
    public class ApiResponse
    {
        public string ResponseString { get; set; }
        public string ContentType { get; set; }
        public IDictionary<string, string> Headers { get; set; }
        public HttpStatusCode ResponseCode { get; set; }
    }
}
