using Hein.Framework.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Hein.Framework.Configuration
{
    /// <summary>
    /// Provides access to configuration files for client applications. This class cannot be inherited.
    /// </summary>
    public static class ConfigurationManager
    {
        private static IConfiguration Root => ServiceLocator.Get<IConfiguration>();

        /// <summary>
        /// Get a specified <see cref="IConfigurationSection"/> from <see cref="IConfiguration"/> by the section name
        /// </summary>
        /// <param name="sectionName">The configuration section name</param>
        /// <returns></returns>
        public static IConfigurationSection GetSection(string sectionName)
        {
            return Root.GetSection(sectionName);
        }

        public static AppSettings AppSettings => AppSettings.Instance;
        public static ConnectionStrings ConnectionStrings => ConnectionStrings.Instance;
    }
}
