## Hein.Framework.Configuration
One thing I struggled with was .net core's lack of `ConfigurationManager`. I always loved putting a parameter or feature flag in my `app.config`/`web.config` to leverage.  So I made a little custom framework to bring back `ConfigurationManger` for .net core apps to use

### Setup for Implementation
```csharp
//call the AddConfiguration extension in your ConfigureServices at startup
services.AddConfiguration(config);
//dont forget to call BuildServiceLocator
services.BuildServiceLocator();
```

### Sample appsettings.json file
```json
{
  "ConnectionStrings": {
    "DbConn": "Data Source=localhost;Initial Catalog=MyDatabase;Persist Security Info=True"
  },
  "AppSettings": {
    "ApplicationId": 747474,
    "Environment": "Dev"
  },
  "MyCustomSection": {
    "HowAboutThem": "Apples"
  }
}
```

### Usage
```csharp
//AppSettings
var appId = ConfigurationManager.AppSettings["ApplicationId"];

//ConnectionStrings
var connString = ConfigurationManager.ConnectionStrings["DbConn"];

//Custom Sections
var section = ConfigurationManager.GetSection("MyCustomSection");
```


## Prerequisites
* [Hein.Framework.DependencyInjection](https://github.com/brandonhein/Hein.Framework/tree/master/DependencyInjection)
* [Microsoft.Extensions.Configuration](https://www.nuget.org/packages/Microsoft.Extensions.Configuration)
* [Microsoft.Extensions.Configuration.Json](https://www.nuget.org/packages/Microsoft.Extensions.Configuration.Json)
