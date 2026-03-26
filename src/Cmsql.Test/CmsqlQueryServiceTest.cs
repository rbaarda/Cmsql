using System;
using Cmsql.Query;
using Cmsql.Query.Execution;
using FluentAssertions;
using Xunit;

namespace Cmsql.Test
{
    public class CmsqlQueryServiceTest
    {
        [Theory]
        [InlineData("selectaaaa test from start")]
        [InlineData("select * from ProductPage")]
        public void Test_when_query_contains_syntax_errors_parse_result_has_errors_and_execution_result_is_empty(string query)
        {
            var queryService = new CmsqlQueryService(new FakeCmsqlQueryRunner());
            var resultSet = queryService.ExecuteQuery(query);

            resultSet.ParseResult.Errors.Should().NotBeNullOrEmpty();
            resultSet.ExecutionResult.QueryResults.Should().BeNullOrEmpty();
        }

        [Fact]
        public void Test_when_query_is_null_should_throw()
        {
            var queryService = new CmsqlQueryService(new FakeCmsqlQueryRunner());
            queryService.Invoking(x => x.ExecuteQuery(null)).Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Test_when_query_is_empty_should_throw()
        {
            var queryService = new CmsqlQueryService(new FakeCmsqlQueryRunner());
            queryService.Invoking(x => x.ExecuteQuery(string.Empty)).Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Test_when_query_is_valid_executes_runner_and_returns_results()
        {
            var runner = new FakeCmsqlQueryRunner();
            var expectedResult = new TestCmsqlQueryResult();
            runner.ExecutionResultToReturn = new CmsqlQueryExecutionResult(
                new[] { expectedResult },
                Array.Empty<CmsqlQueryExecutionError>());

            var queryService = new CmsqlQueryService(runner);
            var resultSet = queryService.ExecuteQuery("select ProductPage from start");

            runner.ExecuteCallCount.Should().Be(1);
            runner.ExecutedQueries.Should().ContainSingle(query =>
                query.ContentType == "ProductPage" &&
                query.StartNode.StartNodeType == CmsqlQueryStartNodeType.Start);

            resultSet.IsSuccess.Should().BeTrue();
            resultSet.ExecutionResult.QueryResults.Should().ContainSingle().Which.Should().Be(expectedResult);
        }

        [Fact]
        public void Test_when_runner_returns_errors_result_set_contains_execution_errors()
        {
            var runner = new FakeCmsqlQueryRunner();
            runner.ExecutionResultToReturn = new CmsqlQueryExecutionResult(
                Array.Empty<ICmsqlQueryResult>(),
                new[] { new CmsqlQueryExecutionError("boom") });

            var queryService = new CmsqlQueryService(runner);
            var resultSet = queryService.ExecuteQuery("select ProductPage from start");

            resultSet.HasExecutionErrors.Should().BeTrue();
            resultSet.ExecutionResult.Errors.Should().ContainSingle().Which.Message.Should().Be("boom");
        }

        [Fact]
        public void Test_when_query_has_parse_errors_runner_is_not_called()
        {
            var runner = new FakeCmsqlQueryRunner();
            var queryService = new CmsqlQueryService(runner);

            var resultSet = queryService.ExecuteQuery("select foo-bar from start");

            runner.ExecuteCallCount.Should().Be(0);
            resultSet.HasParseErrors.Should().BeTrue();
        }

        private class TestCmsqlQueryResult : ICmsqlQueryResult
        {
        }
    }
}
