using Xunit;

namespace Hein.Framework.DependancyInjection.Tests
{
    public class DependancyInjectionTests
    {
        [Fact]
        public void Should_create_a_new_service_upon_new_register_and_get_specific_services_when_called()
        {
            ServiceLocator.Reset();

            ServiceLocator.Register<IFakeServiceOne>(new FakeServiceOne());
            ServiceLocator.Register<IFakeServiceTwo>(new FakeServiceTwo("Brandon"));

            var fakeServiceOne = ServiceLocator.Get<IFakeServiceOne>();

            Assert.IsType<FakeServiceOne>(fakeServiceOne);
            Assert.True(fakeServiceOne.Calculate() == 42m);

            var fakeServiceTwo = ServiceLocator.Get<IFakeServiceTwo>();

            Assert.IsType<FakeServiceTwo>(fakeServiceTwo);
            Assert.True(fakeServiceTwo.GetName() == "Brandon");


            //service will be null if not registered
            var fakeServiceThree = ServiceLocator.Get<IFakeServiceThree>();
            Assert.IsNotType<FakeServiceThree>(fakeServiceThree);
            Assert.True(fakeServiceThree == null);
        }
    }
}
