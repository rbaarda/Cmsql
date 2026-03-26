using System;
using Cmsql.Grammar.Parsing;
using Cmsql.Query;
using Cmsql.Query.Execution;
using FluentAssertions;
using Xunit;

namespace Cmsql.Test
{
    public class CmsqlQueryResultSetTest
    {
        [Fact]
        public void Test_has_parse_errors_when_parser_reports_errors()
        {
            var parseResult = CreateParseResult(new CmsqlQueryParseError(1, 5, "unexpected token"));
            var resultSet = CmsqlQueryResultSet.CreateParseFailure(parseResult);

            resultSet.HasParseErrors.Should().BeTrue();
            resultSet.HasErrors.Should().BeTrue();
        }

        [Fact]
        public void Test_has_execution_errors_when_runner_reports_errors()
        {
            var parseResult = CreateParseResult();
            var executionResult = new CmsqlQueryExecutionResult(
                Array.Empty<ICmsqlQueryResult>(),
                new[] { new CmsqlQueryExecutionError("boom") });

            var resultSet = CmsqlQueryResultSet.CreateSuccess(parseResult, executionResult);

            resultSet.HasExecutionErrors.Should().BeTrue();
            resultSet.HasErrors.Should().BeTrue();
        }

        [Fact]
        public void Test_errors_collection_contains_parse_and_execution_messages()
        {
            var parseResult = CreateParseResult(new CmsqlQueryParseError(2, 1, "parse failed"));
            var executionResult = new CmsqlQueryExecutionResult(
                Array.Empty<ICmsqlQueryResult>(),
                new[] { new CmsqlQueryExecutionError("execution failed") });

            var resultSet = new CmsqlQueryResultSet(parseResult, executionResult);

            resultSet.Errors.Should().Contain("Parse Error: parse failed");
            resultSet.Errors.Should().Contain("Execution Error: execution failed");
        }

        [Fact]
        public void Test_get_results_returns_execution_results_when_successful()
        {
            var parseResult = CreateParseResult();
            var expectedResult = new DummyCmsqlQueryResult();
            var executionResult = new CmsqlQueryExecutionResult(
                new[] { expectedResult },
                Array.Empty<CmsqlQueryExecutionError>());

            var resultSet = CmsqlQueryResultSet.CreateSuccess(parseResult, executionResult);

            resultSet.GetResults().Should().ContainSingle().Which.Should().Be(expectedResult);
        }

        [Fact]
        public void Test_get_results_returns_empty_when_result_set_has_errors()
        {
            var parseResult = CreateParseResult(new CmsqlQueryParseError(1, 0, "parse failed"));
            var executionResult = new CmsqlQueryExecutionResult(
                new[] { new DummyCmsqlQueryResult() },
                Array.Empty<CmsqlQueryExecutionError>());

            var resultSet = new CmsqlQueryResultSet(parseResult, executionResult);

            resultSet.GetResults().Should().BeEmpty();
        }

        private static CmsqlQueryParseResult CreateParseResult(params CmsqlQueryParseError[] errors)
        {
            return new CmsqlQueryParseResult
            {
                Errors = errors,
                Queries = Array.Empty<CmsqlQuery>()
            };
        }

        private class DummyCmsqlQueryResult : ICmsqlQueryResult
        {
        }
    }
}
