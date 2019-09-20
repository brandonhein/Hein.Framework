using Amazon.DynamoDBv2.Model;
using Hein.Framework.Dynamo.Criterion;
using Hein.Framework.Dynamo.Tests.Helpers;
using System.Linq;
using Xunit;

namespace Hein.Framework.Dynamo.Tests
{
    public class CriterionTests
    {
        [Fact]
        public void Should_build_a_top_5_query_request_from_a_query_over()
        {
            var query = QueryOver.Of<Person>()
                .Where(x => x.IsAwesome == true)
                .Top(5);

            var queryRequest = query.DynamoCommand;
            Assert.IsType<QueryRequest>(queryRequest);


            var request = (QueryRequest)queryRequest;

            Assert.True(request.TableName == "HelloPerson"); //will use the DynamoDBTableAttribute for this
            Assert.True(request.Limit == 5); //told it to max return 5 items;

            Assert.True(request.ProjectionExpression == "#personId, #firstName, #lastName, #isAwesome, #isDeleted"); //should exculde IsFirstNameBrandon as it has that attribute

            Assert.True(request.FilterExpression == "#isAwesome = :val0");
            Assert.True(request.ExpressionAttributeValues.FirstOrDefault().Key == ":val0");
            Assert.True(request.ExpressionAttributeValues.FirstOrDefault().Value.BOOL == true);
        }
    }
}
