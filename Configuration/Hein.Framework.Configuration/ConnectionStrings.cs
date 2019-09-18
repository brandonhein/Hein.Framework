using System;
using System.Collections.Generic;
using System.Text;

namespace Hein.Framework.Configuration
{
    public sealed class ConnectionStrings
    {
        private ConnectionStrings() { }

        internal static ConnectionStrings Instance { get; } = new ConnectionStrings();

        /// <summary>
        /// Gets the entry in your "ConnectionStrings" section of you configuration file
        /// </summary>
        /// <param name="key">The key name found in the "ConnectionStrings" section of your configuration file</param>
        /// <exception cref="KeyNotFoundException">This error will occur if the key you're looking for is not in the "ConnectionStrings" section</exception>
        /// <returns></returns>
        public string this[string key] => ConfigurationManager.GetSection("ConnectionStrings")[key] ?? throw GetKeyNotFoundExeption(key);

        private static Exception GetKeyNotFoundExeption(string key) =>
            new KeyNotFoundException($"Unable to locate {nameof(ConfigurationManager.ConnectionStrings)} key '{key}'");
    }
}
