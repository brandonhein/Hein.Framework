using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.DependencyInjection;

namespace Hein.Framework.Identity
{
    public static class HttpContextSetupExtensions
    {
        public static IApplicationBuilder UseIdentity(this IApplicationBuilder app)
        {
            app.UseMiddleware<HttpContextSetterMiddleware>();
            return app;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddAuthentication(HttpSysDefaults.AuthenticationScheme);
            return services;
        }

        public static IWebHostBuilder UseWindowsAuthentication(this IWebHostBuilder builder, bool allowAnonymous = false)
        {
            builder.UseHttpSys(options =>
            {
                options.Authentication.Schemes = AuthenticationSchemes.NTLM | AuthenticationSchemes.Negotiate;
                options.Authentication.AllowAnonymous = allowAnonymous;
            });
            return builder;
        }
    }
}
