## Hein.Framework.Http
Just like Serialization packages, every framework has an Http/Api library to use.  Api calls can be tricky and difficult to unit test.  This library sets you up to work with input and outputs as objects.

### Samples
```csharp
/* POST Example */
var request = new ApiRequest()
{
  BaseUrl = "https://my.baseurl.com",
  Path = "prod/v2/example"
  Method = HttpMethod.Post, //<-- enum included in this library with all methods
  Accept = HttpContentType.Json //<-- constant class included in this library with regularly used mime types
  ContentType = HttpContentType.Json
  Headers = new Dictionary<string, string>()
  {
    { "customHeader", "customValueToSend" }
  },
  QueryParameters = new Dictionary<string, string>()
  {
    { "queryKey", "queryValue" }
  }
  Body = "your json/data here"
};

/* curl sample of the request above
curl -X POST "https://my.baseurl.com/prod/v2/example?queryKey=queryValue" 
     -H "accept: application/json" 
     -H "Content-Type: application/json"
     -H "customHeader: customValueToSend"
     -d "your json/data here"
*/

var service = new ApiService();

var response = service.Execute(request);
var response = await service.ExecuteAsync(request);
```

### Dependency Injection
The `ApiService` leverages the interface `IApiService`.  So if your functionality, provider, or service is dependant on an API call, pass the `IApiService` thru by wiring it up in your service collection/ioc.

```csharp
// This method gets called by the runtime. Use this method to add services to the container.
public void ConfigureServices(IServiceCollection services)
{
   services.AddSingleton<IApiService>(s => new ApiService());
   services.AddTransient<IExternalIntegrationProvider>(s => new ExternalIntegrationProvider(new ApiService()));
}
```
