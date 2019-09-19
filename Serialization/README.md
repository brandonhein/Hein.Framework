## Hein.Framework.Serialization
Serialization librarys and packages are all the same.  They all contain some extension to turn an object to JSON or XML.  This one is no different.  Tried some Soap serialization to help out if your app integrates and needs soap xml

### Json Samples
```csharp
/****** Serialize ******/
//Serialize object with the default property name resovler
var json = someObject.ToJson();
//serialize object with any resovler that implements IContractResolver
var json = someObject.ToJson(new CamelCasePropertyNamesContractResolver());

/****** Deserialize ******/
//deserialize json into an object *Newtonsoft does the magic... this is just a wrapper*
var result = Deserialize.JsonToObject<MyClass>(json);

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
var result = Deserialize.XmlToObject<MyClass>(xml);

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