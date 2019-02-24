namespace Hein.Framework.DependancyInjection
{
    public static class ServiceLocator
    {
        private static IServiceCollection _collection;
        public static void Register<T>(T service)
        {
            if (_collection == null)
            {
                _collection = new ServiceCollection();
            }

            _collection.Register(service);
        }

        public static T Get<T>()
        {
            var service = _collection.GetService<T>();
            return service;
        }

        public static void Reset()
        {
            _collection = null;
        }
    }
}
