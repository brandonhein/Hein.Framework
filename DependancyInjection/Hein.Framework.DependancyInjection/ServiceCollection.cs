using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Linq;

namespace Hein.Framework.DependancyInjection
{
    public class ServiceCollection : IServiceCollection
    {
        private static ConcurrentDictionary<Type, object> _dictionary = new ConcurrentDictionary<Type, object>();

        public void Register<T>(T implementation)
        {
            var result = _dictionary.TryAdd(typeof(T), implementation);
        }

        public void Register<T>(Type serviceType, T implementation)
        {
            var result = _dictionary.TryAdd(serviceType, implementation);
        }

        public T GetService<T>()
        {
            var type = typeof(T);
            return (T)_dictionary.FirstOrDefault(x => x.Key == type).Value;
        }
    }

    public interface IServiceCollection
    {
        void Register<T>(T implementation);
        void Register<T>(Type serviceType, T implementation);
        T GetService<T>();
    }

}
