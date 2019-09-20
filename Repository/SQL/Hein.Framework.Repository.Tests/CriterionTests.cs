using Hein.Framework.Repository.Criterion;
using Hein.Framework.Repository.Tests.Helpers;
using Xunit;

namespace Hein.Framework.Repository.Tests
{
    public class CriterionTests
    {
        [Fact]
        public void Should_build_select_top_5_statement_from_a_query_over()
        {
            var query = QueryOver.Of<PersonEntity>()
                .Where(x => x.IsAwesome == true)
                .Top(5);

            var exepctedSql = "SELECT TOP 5 PersonId,FirstName,LastName,IsAwesome,IsDeleted FROM Person (NOLOCK) WHERE (IsAwesome = 1)";

            Assert.Equal(exepctedSql, query.SQL);
        }

        [Fact]
        public void Should_build_select_distinct_statement_from_a_query_over()
        {
            var query = QueryOver.Of<PersonEntity>()
                .Where(x => x.FirstName == "Brandon")
                .Distinct();

            var expectedSql = "SELECT DISTINCT PersonId,FirstName,LastName,IsAwesome,IsDeleted FROM Person (NOLOCK) WHERE (FirstName = 'Brandon')";

            Assert.Equal(expectedSql, query.SQL);
        }

        [Fact]
        public void Should_build_select_statement_with_specific_column_names()
        {
            var query = QueryOver.Of<PersonEntity>()
                .Select<PersonName>()
                .Where(x => x.IsAwesome == true && x.FirstName == "Brandon");

            var expectedSql = "SELECT FirstName,LastName FROM Person (NOLOCK) WHERE ((IsAwesome = 1) AND (FirstName = 'Brandon'))";

            Assert.Equal(expectedSql, query.SQL);
        }

        [Fact]
        public void Should_build_select_statement_with_an_order_by_desc()
        {
            var query = QueryOver.Of<PersonEntity>()
                .Where(x => x.PersonId == 16 || x.PersonId == 17)
                .OrderByDesending(o => o.PersonId);

            var expectedSql = "SELECT PersonId,FirstName,LastName,IsAwesome,IsDeleted FROM Person (NOLOCK) WHERE ((PersonId = 16) OR (PersonId = 17)) ORDER BY PersonId Desc";

            Assert.Equal(expectedSql, query.SQL);
        }
    }
}
