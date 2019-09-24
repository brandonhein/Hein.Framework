## Hein.Framework.Identity
.NET Framework has a `Current` property on the `HttpContext` object.  This made it so you can grab the current user making a call to your server if you enabled Windows Auth in IIS.  Pretty nifty feature so you didn't need to have/manage custom logins to understand who a user was.  With .NET Core, this feature didn't come with. Windows Auth didnt't go away, and after reading this [article](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/windowsauth?view=aspnetcore-2.1&tabs=visual-studio) 

### Setup
Theres a bit of Setup you need to do to enable this.
1. Add Windows Auth to your WebHostBuilder
```csharp
/* Program.cs */
using Hein.Framework.Identity;

WebHost.CreateDefaultBuilder(args)
   .UseStartup<Startup>()
   .UseWindowsAuthentication(); //<-- it takes an optional bool to allow anonymous auth as well
```
2. Add Identity Service to ServiceCollection
```csharp
/* Startup.cs */
using Hein.Framework.Identity;

public void ConfigureServices(IServiceCollection services)
{
   services.AddMvc();
   services.AddIdentity(); //<-- register usage
}
```
3. Use Identity in your pipeline
```csharp
/* Startup.cs */
using Hein.Framework.Identity;

public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
   app.UseIdentity(); //<-- make it the top most/first call in the pipeline
   app.UseMvc(routes =>
      {
         routes.MapRoute(
         name: "default",
         template: "{controller=Home}/{action=Index}/{id?}");
      });
}
```

### Usage
Once the setup is all configured, you can now use `HttpContext` like you would in the .NET Framework applications

```csharp
using Hein.Framework.Identity;

var user = HttpContext.Current.User;
var identity = HttpContext.Current.User.Identity;
var windowsUserName = HttpContext.Current.User.Identity.Name;
```

Also be sure to add the `Authroize`/`AllowAnonymous` attributes to your Controllers where you want `HttpContext.Current` to be available to use
```csharp
[AllowAnonymous] //<-- will not set/use windows identity perfect for calls that don't need any auth
public class HealthController : Controller
{ }

[Authorize] //<-- will set/use windows identity, perfect for calls you need to know who the user is
public class MessageController : Controller
{ }
```

## Prerequisites
* [Microsoft.AspNetCore.Authentication](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication)
* [Microsoft.AspNetCore.Http](https://www.nuget.org/packages/Microsoft.AspNetCore.Http)
* [Microsoft.AspNetCore.Server.HttpSys](https://www.nuget.org/packages/Microsoft.AspNetCore.Server.HttpSys)