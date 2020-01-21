using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Hein.Framework.Http
{
    public class ApiService : ApiServiceBase, IApiService
    {
        public async Task<ApiResponse> ExecuteAsync(ApiRequest request)
        {
            try
            {
                var webRequest = GenerateWebRequest(request);

                if (IsWriteableMethod(request.Method))
                {
                    using (var stream = new StreamWriter(await webRequest.GetRequestStreamAsync()))
                    {
                        await stream.WriteAsync(request.Body);
                        await stream.FlushAsync();
                        stream.Close();
                    }
                }

                return await GetResultAsync(await webRequest.GetResponseAsync());
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    return await GetResultAsync(ex.Response);
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex);
            }

            return new ApiResponse(new Exception("no exception caught"));
        }

        public ApiResponse Execute(ApiRequest request)
        {
            try
            {
                var webRequest = GenerateWebRequest(request);

                if (IsWriteableMethod(request.Method))
                {
                    using (var stream = new StreamWriter(webRequest.GetRequestStream()))
                    {
                        stream.Write(request.Body);
                        stream.Flush();
                        stream.Close();
                    }
                }

                return GetResult(webRequest.GetResponse());
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    return GetResult(ex.Response);
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex);
            }

            return new ApiResponse(new Exception("no exception caught"));
        }
    }
}
