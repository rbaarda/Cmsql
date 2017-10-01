using FluentAssertions;
using Xunit;

namespace Cql.Test
{
    public class CqlQueryServiceTest
    {
        [Fact]
        public void Test_when_query_contains_syntax_errors_parse_result_has_errors_and_execution_result_is_empty()
        {
            CqlQueryService queryService = new CqlQueryService(new FakeCqlQueryRunner());
            CqlQueryResultSet resultSet = queryService.ExecuteQuery("selectaaaa test from start");

            resultSet.ParseResult.Errors.Should().NotBeNullOrEmpty();
            resultSet.ExecutionResult.QueryResults.Should().BeNullOrEmpty();
        }
    }
}
