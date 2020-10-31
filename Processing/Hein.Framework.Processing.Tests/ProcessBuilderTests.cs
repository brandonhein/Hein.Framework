using System.Threading.Tasks;
using Xunit;

namespace Hein.Framework.Processing.Tests
{
    public class ProcessBuilderTests
    {
        [Fact]
        public async Task Should_build_and_execute_a_process_builder_successfully()
        {
            var context = new MyProcessContext();

            var process = new ProcessBuilder<IMyProcessContext>(context)
                .AddStep(new MyProcessStepOne())
                .AddStep(new MyProcessStepTwo());

            Assert.False(process.SuccessfullyRan);

            await process.ExecuteAsync();

            Assert.True(process.SuccessfullyRan);
            Assert.Equal("I applied my change in step 2", context.SomeStringTwo);

            //stringone didnt get updated as it failed the check
            Assert.Null(context.SomeStringOne);
        }

        [Fact]
        public async Task Should_build_and_execute_process_but_stop_at_step_three()
        {
            var context = new MyProcessContext();

            var process = new ProcessBuilder<IMyProcessContext>(context)
                .AddStep(new MyProcessStepOne())
                .AddStep(new MyProcessStepTwo())
                .AddStep(new MyProcessStepThree());

            Assert.False(process.SuccessfullyRan);

            await process.ExecuteAsync();

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

        public override async Task<bool> ExecuteAsync(IMyProcessContext context)
        {
            context.SomeStringOne = "string one should not get applied because it fails the should execute";
            return true;
        }

        public override async Task<bool> ShouldExecuteAsync(IMyProcessContext context)
        {
            return !string.IsNullOrEmpty(context.SomeStringOne);
        }
    }

    public class MyProcessStepTwo : ProcessStep<IMyProcessContext>
    {
        public MyProcessStepTwo() : base("MyProcessStepTwo")
        { }

        public override async Task<bool> ExecuteAsync(IMyProcessContext context)
        {
            context.SomeStringTwo = "I applied my change in step 2";
            return true;
        }

        public override async Task<bool> ShouldExecuteAsync(IMyProcessContext context)
        {
            return true;
        }
    }

    public class MyProcessStepThree : ProcessStep<IMyProcessContext>
    {
        public MyProcessStepThree() : base("MyProcessStepThree")
        { }

        public override async Task<bool> ExecuteAsync(IMyProcessContext context)
        {
            return false;
        }

        public override async Task<bool> ShouldExecuteAsync(IMyProcessContext context)
        {
            return true;
        }
    }
}
