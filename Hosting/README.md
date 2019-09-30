## Hein.Framework.Hosting
I enjoy when applications look like they're doing the same thing in the code.  ASP.NET Core as a pretty solid base to attach how your application is hosted.  I like the `UseStartup` extension for `IWebHostBuilder` which moves the DI and Configuration stuff to that class.  For Windows Services/Background process apps with no user interface... it doesn't have that ability to call `UseStartup` soooo I made this package that can help use a `Startup` class in for your `IHostBuilder`

### Environment Variables (Important Note!)
I use a single environment variable to select a specific `appsettings.json` file.  This is usally `ASPNETCORE_ENVIRONMENT`.  This package will look for value in `ASPNETCORE_ENVIRONMENT`, `NETCORE_ENVIRONMENT`, and `ENVIRONMENT` before defaulting the value to `Dev`


### Sample
Program.cs
```csharp
public class Program
{
   public static void Main(string[] args)
   {
      CreateHostBuilder(args).Build().Run();
   }

   public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
         .UseContentRoot(Directory.GetCurrentDirectory())  //<-- I like setting my Content Root
         .UseConsoleLifetime()    //<-- need to call this to keep the App Running :)
         .UseStartup<Startup>();  //<-- Call the UseStartup<T> extension
}
```
Startup.cs
```csharp
public class Startup : IWorkerServiceStartup //<-- must implement IWorkerServiceStartup interface 
{
   //Startup constructor takes IHostingEnvironment or IConfiguration like
   public Startup(IHostingEnvironment env)
   {
      //Call your ConfigurationBuilder here and pass in your env variables
   }

   public IConfiguration Configuration { get; }

   //Configure Services for your Dependency Injection... 
   //I will suggest you leverage Hein.Framework.DependencyInejection as well
   public void ConfigureServices(IServiceCollection services)
   {
      //create your DIs
   }

   public void Run()
   {
      //here's your App to run after all that startup stuff
      //this is where you call your ConJobs/Queue Listeners/etc
   }
}
```


## Prerequisites
* [Microsoft.Extensions.Hosting](https://www.nuget.org/packages/Microsoft.Extensions.Hosting)
