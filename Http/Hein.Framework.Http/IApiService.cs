using System.Threading.Tasks;

namespace Hein.Framework.Http
{
    public interface IApiService
    {
        ApiResponse Execute(ApiRequest request);
        Task<ApiResponse> ExecuteAsync(ApiRequest request);
    }
}
