using Hein.Framework.Dynamo.Criterion;
using Hein.Framework.Dynamo.Tests.Helpers;
using Xunit;

namespace Hein.Framework.Dynamo.Tests
{
    public class RepositoryTests
    {
        [Fact]
        public void Test1()
        {
            var query = QueryOver.Of<Person>()
                .Where(x => x.FirstName == "Brandon" || x.LastName == "Smith");
        }
    }
}
