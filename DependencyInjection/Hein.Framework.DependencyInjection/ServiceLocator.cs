using Microsoft.Extensions.DependencyInjection;

namespace Hein.Framework.DependencyInjection
{
    /// <summary>
    /// Leverages Microsofts Dependency Injection - Service Collection.  
    /// Be sure to call BuildServiceLocator in your configureservices method at startup
    /// </summary>
    public static class ServiceLocator
    {
        private static IServiceCollection _collection;
        private static ServiceProvider _provider;

        /// <summary>
        /// Call this method at the end of your ConfigureServices method to leverage ServiceLocator
        /// </summary>
        public static void BuildServiceLocator(this IServiceCollection collection)
        {
            _collection = collection;
            _provider = _collection.BuildServiceProvider();
        }

        /// <summary>
        /// Get the implementation of the service you called for 
        /// </summary>
        public static T Get<T>()
        {
            if (_provider != null)
            {
                return _provider.GetService<T>();
            }

            throw new ServiceLocatorNotBuiltException();
        }
    }
}
