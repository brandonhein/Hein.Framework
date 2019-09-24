using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Hein.Framework.Identity
{
    public class HttpContextSetterMiddleware
    {
        private readonly RequestDelegate _next;
        public HttpContextSetterMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(Microsoft.AspNetCore.Http.HttpContext context)
        {
            HttpContext.Set(context);
            await _next(context);
            HttpContext.Flush();
        }
    }
}
