## Hein.Framework.DependancyInjection
Inspiration: Working with some AWS Queues for a ASP.NET CORE 2.0 application, I LOVED the automatic feature of putting interfaces in the controller and Core auto setting them.  What I had an issue with was using this dependancy injection for a SQS queue listener process.  So I did some research on how I can possibly implement something for unit tests and background processes.  Enjoy.

### Samples
```csharp
//this custom ServiceLocator for this project will generate a ServiceCollection if its null
ServiceLocator.Register<IFakeServiceOne>(new FakeServiceOne());

//this can now be fetched at any time in the application that needs IFakeServiceOne
var service = ServiceLocator.Get<IFakeServiceOne>();
```
