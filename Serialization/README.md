## Hein.Framework.Serialization
Serialization librarys and packages are all the same.  They all contain some extension to turn an object to JSON or XML.  This one is no different.  Tried some Soap serialization to help out if your app integrates and needs soap xml

## Usage

### Json Samples
```csharp
/****** Serialize ******/
//Serialize object with the default settings
var json = someObject.ToJson();
//serialize object with any custom `JsonConverter`'s
var json = someObject.ToJson(new MyCustomConverter(), new MyOtherCustomConverter());

/****** Deserialize ******/
//deserialize json into an object *System.Text.Json does the magic... this is just a wrapper*
var result = json.FromJson<MyClass>();

/****** Formating ******/
//for display purposes you might want to have json indented for easier readability... just call this extension
var prettyJson = compressedJson.ToIndentedJson();

//for logs or other cases where you want your json to be compressed... call this extension
var compressedJson = indentedJson.CompressJson();
```

### Xml Samples
```csharp
/****** Serialize ******/
var xml = someObject.ToXml();

/****** Deserialize ******/
//deserialize xml string to object
var result = xml.FromXml<MyClass>();

/****** Formating ******/
//for display purposes you might want to have json indented for easier readability... just call this extension
var prettyXml = compressedXml.ToIndentedXml();

//for logs or other cases where you want your json to be compressed... call this extension
var compressedXml = indentedXml.CompressXml();
```

### Soap Samples
```csharp
/****** Serialize ******/
var soapXml = someObject.ToSoapXml();

/****** Deserialize ******/
//deserialize soap xml string to object
var result = Deserialize.SoapToObject<MyClass>(soapXml);
```

## JSON Coolness
`System.Text.Json` seralization (as of 4.1.2021) is still a work in progress.  A lot of functionality found in `Newtonsoft`/`JSON.Net` that C# developers took for granted isn't available yet. So this library/package is an attempt to create some exteneded functionality for `System.Text.Json`

### JsonPropertyOrder
Property order is not important in the world of json... however, when it comes to viewing json in logs or displaying on admin screens... some techy folks appreciate an order of properites when viewing.

`Newtonsoft` had this as an attribute... and allowed us to order properites in our json with ease... `System.Text.Json` doesn't have this yet... so I made it
```csharp
public class SampleClass
{
   [JsonPropertyOrder(3)]
   public string Last { get; set; }
   [JsonPropertyOrder(1)]
   public string First { get; set; }
   [JsonPropertyOrder(2)]
   public string Middle { get; set; }
}

var json = new SampleClass { Last = "last", First = "first", Middle = "middle" }.ToJson();
```
Result:
```json
{
   "First": "first",
   "Middle": "middle",
   "Last": "last"
}
```

### Event Versioning (as an attribute!)
In the grand scheme of event messages to and from systems... usually a property for event versioning is used.  Typcially this can be done with an immutable(read-only) property to tack along in the json.  Instead of that... you can use this package's `JsonVersion` attribute to tack on a Version property in the json for you
```csharp
[EventVersion("1.3")]
public class EventMessage
{
   public string Name { get; set; }
}

var json = new EventMessage { Name = "sample" }.ToJson();
```
Result:
```json
{ 
   "EventVersion": "1.3",
   "Name": "sample"
}
```