using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Hein.Framework.Hosting.Sample
{
    public class Startup : IWorkerServiceStartup
    {
        public Startup(IHostingEnvironment env)
        {
            Console.WriteLine("HostingEnv ctor");
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("Configure Services Called");
        }

        public void Run()
        {
            Console.WriteLine("Run is called");
        }
    }
}
