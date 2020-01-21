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
    { "queryKey", "queryValue"
  }
  Body = "your json/data here"
};


var service = new ApiService();

var response = service.Execute(request);
var response = await service.ExecuteAsync(request);
