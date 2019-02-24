namespace Hein.Framework.DependancyInjection.Tests
{
    public class FakeServiceOne : IFakeServiceOne
    {
        public decimal Calculate()
        {
            return 42m;
        }
    }

    public interface IFakeServiceOne
    {
        decimal Calculate();
    }

    public class FakeServiceTwo : IFakeServiceTwo
    {
        private readonly string _name;
        public FakeServiceTwo(string name)
        {
            _name = name;
        }

        public string GetName()
        {
            return _name;
        }
    }

    public interface IFakeServiceTwo
    {
        string GetName();
    }

    public class FakeServiceThree : IFakeServiceThree
    {
        public void Execute()
        {
            //https://www.youtube.com/watch?v=4zLfCnGVeL4
        }
    }

    public interface IFakeServiceThree
    {
        void Execute();
    }
}
