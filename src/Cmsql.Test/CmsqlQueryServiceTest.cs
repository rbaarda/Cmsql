using System;
using FluentAssertions;
using Xunit;

namespace Cmsql.Test
{
    public class CmsqlQueryServiceTest
    {
        [Fact]
        public void Test_when_query_contains_syntax_errors_parse_result_has_errors_and_execution_result_is_empty()
        {
            CmsqlQueryService queryService = new CmsqlQueryService(new FakeCmsqlQueryRunner());
            CmsqlQueryResultSet resultSet = queryService.ExecuteQuery("selectaaaa test from start");

            resultSet.ParseResult.Errors.Should().NotBeNullOrEmpty();
            resultSet.ExecutionResult.QueryResults.Should().BeNullOrEmpty();
        }

        [Fact]
        public void Test_when_query_is_null_should_throw()
        {
            CmsqlQueryService queryService = new CmsqlQueryService(new FakeCmsqlQueryRunner());
            queryService.Invoking(x => x.ExecuteQuery(null)).ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void Test_when_query_is_empty_should_throw()
        {
            CmsqlQueryService queryService = new CmsqlQueryService(new FakeCmsqlQueryRunner());
            queryService.Invoking(x => x.ExecuteQuery(string.Empty)).ShouldThrow<ArgumentException>();
        }
    }
}
