using System;
using System.Collections.Generic;

namespace Hein.Framework.Configuration
{
    public sealed class AppSettings
    {
        private AppSettings() { }

        internal static AppSettings Instance { get; } = new AppSettings();

        /// <summary>
        /// Gets the entry in your "AppSettings" section of you configuration file
        /// </summary>
        /// <param name="key">The key name found in the "AppSettings" section of your configuration file</param>
        /// <exception cref="KeyNotFoundException">This error will occur if the key you're looking for is not in the "AppSettings" section</exception>
        /// <returns></returns>
        public string this[string key] => ConfigurationManager.GetSection("AppSettings")[key] ?? throw GetKeyNotFoundExeption(key);

        private static Exception GetKeyNotFoundExeption(string key) =>
            new KeyNotFoundException($"Unable to locate {nameof(ConfigurationManager.AppSettings)} key '{key}'");
    }
}
