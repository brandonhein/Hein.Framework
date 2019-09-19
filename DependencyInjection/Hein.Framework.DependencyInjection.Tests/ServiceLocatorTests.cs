using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Hein.Framework.DependencyInjection.Tests
{
    public class ServiceLocatorTests
    {
        [Fact]
        //due to the nature of static classes... they are a pain to unit test
        //normally i wouldn't do this, but i need these unit tests run in a specific order to not bomb
        public void All_Tests()
        {
            Should_throw_error_if_build_service_locator_is_not_called();
            Should_get_implimentation_from_service_locator();
        }

        private void Should_get_implimentation_from_service_locator()
        {
            var services = new ServiceCollection();
            services.AddTransient<ITestService>(s => new TestService());
            services.BuildServiceLocator();

            var testService = ServiceLocator.Get<ITestService>();
            Assert.IsType<TestService>(testService);
        }

        private void Should_throw_error_if_build_service_locator_is_not_called()
        {
            Assert.Throws<ServiceLocatorNotBuiltException>(() =>
            {
                var services = new ServiceCollection();
                services.AddTransient<ITestService>(s => new TestService());
                //services.BuildServiceLocator(); //commented out to show that the method is not called

                var testService = ServiceLocator.Get<ITestService>();
                Assert.IsType<TestService>(testService);
            });
        }
    }

    public interface ITestService
    {
        void Execute();
    }

    public class TestService : ITestService
    {
        public void Execute()
        {
            //yay i did it!
        }
    }
}
