## Hein.Framework.Processing
My favorite design pattern is the Builder Pattern.  I think it looks cleaner, readable, reusable, and allows small changes to be added to a bigger picture item.  If you need to run steps and apply different logic to a object or multiple, this would be the perfect package to use.  Perfect for those application processes that are driven by logic, or are constantly getting updated with 'steps'.


### Sample - ProcessBuilder
```csharp
var context = new SampleContext(); //<-- implements the IProcessContext interface

var process = new ProcessBuilder<ISampleContext>(context)
	.AddStep(new AddRepositoryItemStep()) //<-- adds a step thats related to the context and will keep 
                                             //      the order when you call AddStep
	.AddStep(new CallThisServiceStep())
	.AddStep(new LogActivityStep());

process.Execute(); //<-- runs thru all the ordered steps, and applys step logic to the context

var isSuccessful = process.SuccessfullyRan; //<-- boolean value if all steps were executed
var stoppedAt = process.ProcessStoppedAt; //<-- string value of the step name that the process stopped at
```

### Sample - ProcessStep
```csharp
public class MyProcessStep : ProcessStep<IMyProcessContext>
{
   public MyProcessStep() : base("MyProcessStep") //<-- base class takes the step name in constructor
   { }
   
   /// <summary>
   /// Runs validation on IMyProcessContext to determine if this step should run.
   /// the process builder/executor will check this result before calling the Execute on this step
   /// </summary>
   public override bool ShouldExecute(IMyProcessContext context)
   {
      //run logic here
      return true;
   }
   
   /// <summary>
   /// This is where your step logic will live and apply the logic to your context.
   /// The boolean result it returns tells the process executor to continue to the next step or 
   /// stop the entire process 
   /// </summary>
   public override bool Execute(IMyProcessContext context)
   {
      //run logic here
      return true;
   }
}
```