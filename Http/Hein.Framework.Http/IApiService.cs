using System.Threading.Tasks;

namespace Hein.Framework.Http
{
    public interface IApiService
    {
        /// <summary>
        /// Clones the implementation of the interface to run thru a new API call
        /// </summary>
        IApiService New(ApiRequest request);
        /// <summary>
        /// Will Execute an asynchronous HTTP API Call and only return the body of the response
        /// </summary>
        Task<string> ExecuteAsync();
        /// <summary>
        /// Will Execute an synchronous HTTP API Call and only return the body of the response
        /// </summary>
        string Execute();
        /// <summary>
        /// Will Execute an asynchronous HTTP API Call and only return body, status, headers, and content type
        /// </summary>
        Task<ApiResponse> ResponseAsync();
        /// <summary>
        /// Will Execute an synchronous HTTP API Call and only return body, status, headers, and content type
        /// </summary>
        ApiResponse Response();
    }
}
