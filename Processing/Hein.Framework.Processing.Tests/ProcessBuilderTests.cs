using Xunit;

namespace Hein.Framework.Processing.Tests
{
    public class ProcessBuilderTests
    {
        [Fact]
        public void Should_build_and_execute_a_process_builder_successfully()
        {
            var context = new MyProcessContext();

            var process = new ProcessBuilder<IMyProcessContext>(context)
                .AddStep(new MyProcessStepOne())
                .AddStep(new MyProcessStepTwo());

            Assert.False(process.SuccessfullyRan);

            process.Execute();

            Assert.True(process.SuccessfullyRan);
            Assert.Equal("I applied my change in step 2", context.SomeStringTwo);

            //stringone didnt get updated as it failed the check
            Assert.Null(context.SomeStringOne);
        }

        [Fact]
        public void Should_build_and_execute_process_but_stop_at_step_three()
        {
            var context = new MyProcessContext();

            var process = new ProcessBuilder<IMyProcessContext>(context)
                .AddStep(new MyProcessStepOne())
                .AddStep(new MyProcessStepTwo())
                .AddStep(new MyProcessStepThree());

            Assert.False(process.SuccessfullyRan);

            process.Execute();

            Assert.False(process.SuccessfullyRan);
            Assert.Equal("MyProcessStepThree", process.ProcessStoppedAt);
        }
    }







    public interface IMyProcessContext : IProcessContext
    {
        string SomeStringOne { get; set; }
        string SomeStringTwo { get; set; }
    }

    public class MyProcessContext : IMyProcessContext
    {
        public string SomeStringOne { get; set; }
        public string SomeStringTwo { get; set; }
    }

    public class MyProcessStepOne : ProcessStep<IMyProcessContext>
    {
        public MyProcessStepOne() : base("MyProcessStepOne")
        { }

        public override bool Execute(IMyProcessContext context)
        {
            context.SomeStringOne = "string one should not get applied because it fails the should execute";
            return true;
        }

        public override bool ShouldExecute(IMyProcessContext context)
        {
            return !string.IsNullOrEmpty(context.SomeStringOne);
        }
    }

    public class MyProcessStepTwo : ProcessStep<IMyProcessContext>
    {
        public MyProcessStepTwo() : base("MyProcessStepTwo")
        { }

        public override bool Execute(IMyProcessContext context)
        {
            context.SomeStringTwo = "I applied my change in step 2";
            return true;
        }

        public override bool ShouldExecute(IMyProcessContext context)
        {
            return true;
        }
    }

    public class MyProcessStepThree : ProcessStep<IMyProcessContext>
    {
        public MyProcessStepThree() : base("MyProcessStepThree")
        { }

        public override bool Execute(IMyProcessContext context)
        {
            return false;
        }

        public override bool ShouldExecute(IMyProcessContext context)
        {
            return true;
        }
    }
}
