## Hein.Framework.Http
Just like Serialization packages, every framework has an Http/Api library to use.  Api calls can be tricky and difficult to unit test.  This library sets you up to work with input and outputs as objects.

### Samples
```csharp
/* POST Example */
var request = new ApiRequest("https://someurl.here")
{
  Method = HttpMethod.Post, //<-- enum included in this library with all methods
  Accept = HttpContentType.Json //<-- constant class included in this library with regularly used mime types
  ContentType = HttpContentType.Json
  Headers = new Dictionary<string, string>()
  {
    { "customHeader", "customValueToSend" }
  },
  RequestBody = "your json/data here"
};

//once you build the request... you can pass into the service controller
var service = new ApiService(request);

//or you can pass it into the New method, as it clone the implementation.
var api = service.New(request);

//execute calls to get the response body string
var response = api.Execute();
var response = await api.ExecuteAsync();

//response calls to get more details about the response coming back
var response = api.Response();
var response = await api.ResponseAsync();
```