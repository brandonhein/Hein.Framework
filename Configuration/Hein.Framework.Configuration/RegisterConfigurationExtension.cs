using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hein.Framework.DependencyInjection;

namespace Hein.Framework.Configuration
{
    public static class RegisterConfigurationExtension
    {
        /// <summary>
        /// Add the <see cref="IConfiguration"/> you built in your startup, so that <see cref="ServiceLocator"/> can leverage fetching it
        /// </summary>
        public static IServiceCollection AddConfiguration(this IServiceCollection collection, IConfiguration config)
        {
            collection.AddSingleton<IConfiguration>(s => config);
            return collection;
        }
    }
}
