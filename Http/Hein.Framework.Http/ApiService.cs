using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hein.Framework.Http
{
    public class ApiService : ApiServiceBase, IApiService
    {
        public ApiService()
        { }

        public ApiService(string url)
        {
            _parameters = new ApiRequest(url);
        }

        public ApiService(string url, IDictionary<string, string> headers)
        {
            _parameters = new ApiRequest(url, headers);
        }

        public ApiService(string url, string accept, IDictionary<string, string> headers)
        {
            _parameters = new ApiRequest(url, HttpMethod.Get, accept, headers);
        }

        public ApiService(ApiRequest parameters)
        {
            _parameters = parameters;
        }

        /// <summary>
        /// Clones the implementation of the interface to run thru a new API call
        /// </summary>
        public IApiService New(ApiRequest request)
        {
            return new ApiService(request);
        }

        /// <summary>
        /// Will Execute an asynchronous HTTP API Call and only return the body of the response
        /// </summary>
        public Task<string> ExecuteAsync()
        {
            return Task.Run(() =>
            {
                Response();
                return _responseString;
            });
        }

        /// <summary>
        /// Will Execute an synchronous HTTP API Call and only return the body of the response
        /// </summary>
        public string Execute()
        {
            return ExecuteAsync().Result;
        }

        /// <summary>
        /// Will Execute an asynchronous HTTP API Call and only return body, status, headers, and content type
        /// </summary>
        public Task<ApiResponse> ResponseAsync()
        {
            return Task.Run(() =>
            {
                var response = new ApiResponse();

                base.ExecuteCall();

                response.ResponseCode = _statusCode;
                response.ContentType = _contentType;
                response.ResponseString = _responseString;
                response.Headers = _responseHeaders;

                return response;
            });
        }

        /// <summary>
        /// Will Execute an synchronous HTTP API Call and only return body, status, headers, and content type
        /// </summary>
        public ApiResponse Response()
        {
            return ResponseAsync().Result;
        }
    }
}
