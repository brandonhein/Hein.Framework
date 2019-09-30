using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace Hein.Framework.Hosting
{
    public static class Host
    {
        private static bool _calledRun;
        public static IHostBuilder CreateDefaultBuilder()
        {
            return new HostBuilder();
        }

        public static IHostBuilder CreateDefaultBuilder(string[] args)
        {
            return new HostBuilder();
        }

        public static IHostBuilder UseStartup<T>(this IHostBuilder host) where T : class, IWorkerServiceStartup
        {
            IWorkerServiceStartup startup = null;

            host.ConfigureAppConfiguration((hostContext, config) =>
            {
                var constructors = typeof(T).GetConstructors();
                foreach (var info in constructors)
                {
                    var parameters = info.GetParameters();
                    var pleaseBreak = false;
                    foreach (var param in parameters)
                    {
                        if (param.ParameterType == typeof(IHostingEnvironment))
                        {
                            startup = (IWorkerServiceStartup)Activator.CreateInstance(typeof(T), hostContext.HostingEnvironment);
                            pleaseBreak = true;
                            break;
                        }
                        else if (param.ParameterType == typeof(IConfiguration))
                        {
                            startup = (IWorkerServiceStartup)Activator.CreateInstance(typeof(T), hostContext.Configuration);
                            pleaseBreak = true;
                            break;
                        }
                    }

                    if (pleaseBreak)
                    {
                        break;
                    }
                }

                if (startup == null)
                {
                    startup = (IWorkerServiceStartup)Activator.CreateInstance(typeof(T));
                }
            });

            host.ConfigureServices((hostContext, services) =>
            {
                startup.ConfigureServices(services);
                if (!_calledRun)
                {
                    _calledRun = true;
                    Task.Run(() =>
                    {
                        startup.Run();
                    });
                }
            });

            return host;
        }
    }
}
