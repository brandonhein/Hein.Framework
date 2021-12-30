using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Hein.Framework.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add all services that implement <typeparamref name="T"/> in the assembly of where <typeparamref name="T"/> resides
        /// </summary>
        public static void AddAll<T>(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Singleton)
            => AddAll<T>(services, new[] { typeof(T).Assembly }, lifetime);

        /// <summary>
        /// Add all services that implement <typeparamref name="T"/> in specific assemblies
        /// </summary>
        public static void AddAll<T>(this IServiceCollection services, Assembly[] assemblies, ServiceLifetime lifetime = ServiceLifetime.Singleton)
            => AddAll(services, typeof(T), assemblies, lifetime);

        /// <summary>
        /// Add all services that implement the service type in the assembly where the service type resides
        /// </summary>
        public static void AddAll(this IServiceCollection services, Type serviceType, ServiceLifetime lifetime = ServiceLifetime.Singleton)
            => AddAll(services, serviceType, new[] { serviceType.Assembly }, lifetime);

        /// <summary>
        /// Add all services that implement the service type in specific assemblies
        /// </summary>
        public static void AddAll(this IServiceCollection services, Type serviceType, Assembly[] assemblies, ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            if (serviceType.IsGenericType)
            {
                foreach (var assembly in assemblies)
                {
                    var serviceTypesFromAssembly = assembly.DefinedTypes.Where(x => x.GetInterfaces().Any(b => b.FullName.StartsWith(serviceType.FullName)));
                    foreach (var service in serviceTypesFromAssembly)
                    {
                        var interfaces = service.GetInterfaces();
                        foreach (var face in interfaces)
                        {
                            services.Add(new ServiceDescriptor(face, service, lifetime));
                        }
                    }
                }
            }
            else
            {
                var typesFromAssemblies = assemblies.SelectMany(x => x.DefinedTypes.Where(b => b.GetInterfaces().Contains(serviceType)));
                foreach (var type in typesFromAssemblies)
                {
                    services.Add(new ServiceDescriptor(serviceType, type, lifetime));
                }
            }
        }
    }
}
