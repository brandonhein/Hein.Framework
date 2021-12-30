## Hein.Framework.DependencyInjection
I use Microsofts Dependency Injection in my ASP.NET Core apps.  I love the auto injection feature for controllers. This is a struggle when it came to Lambda Functions and windows processes/queue listeners... so I wanted to build and share my easy solution how I still was able to leverage Microsofts `ServiceProvider` from anywhere in my app.

### Samples
```csharp
//call this method after you've spun up all your service mappings in ConfigureServices()
services.BuildServiceLocator();

//this can now be fetched at any time in the application that needs IFakeServiceOne
var service = ServiceLocator.Get<IFakeServiceOne>();
```

```csharp
//call this method to register all services that implement a specific service
services.AddAll<IGenerateView>();

//call this method to register all generic services that implement a generic service
//this is useful if you have different implementations but all share the same generic interface
services.AddAll(typeof(IReport<>));
```