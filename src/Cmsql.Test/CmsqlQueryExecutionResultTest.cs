using Cmsql.Query;
using Cmsql.Query.Execution;
using FluentAssertions;
using Xunit;

namespace Cmsql.Test
{
    public class CmsqlQueryExecutionResultTest
    {
        [Fact]
        public void Test_default_constructor_initializes_empty_collections()
        {
            var executionResult = new CmsqlQueryExecutionResult();

            executionResult.QueryResults.Should().NotBeNull().And.BeEmpty();
            executionResult.Errors.Should().NotBeNull().And.BeEmpty();
        }

        [Fact]
        public void Test_constructor_preserves_results_and_errors()
        {
            var queryResults = new ICmsqlQueryResult[] { new DummyCmsqlQueryResult() };
            var errors = new[] { new CmsqlQueryExecutionError("failed") };

            var executionResult = new CmsqlQueryExecutionResult(queryResults, errors);

            executionResult.QueryResults.Should().BeEquivalentTo(queryResults);
            executionResult.Errors.Should().BeEquivalentTo(errors);
        }

        private class DummyCmsqlQueryResult : ICmsqlQueryResult
        {
        }
    }
}
