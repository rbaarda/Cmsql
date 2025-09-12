using Cmsql.Grammar.Parsing.Internal;
using FluentAssertions;
using Xunit;

namespace Cmsql.Test.Grammar.Parsing.Internal
{
    public class QueriesVisitorTest
    {
        [Theory]
        [InlineData("select test from start")]
        [InlineData("select test from root")]
        [InlineData("select test from 123")]
        public void Test_can_parse_single_valid_query_without_where_clause(string query)
        {
            var cmsqlParser = CmsqlParserFactory.CreateParserForQuery(query);
            var parseTree = cmsqlParser.queries();

            var visitor = new QueriesVisitor();
            var queries = visitor.VisitQueries(parseTree);

            queries.Should().NotBeNullOrEmpty();
            queries.Should().HaveCount(1);
        }

        [Theory]
        [InlineData("select test from start;")]
        [InlineData("select test from root;")]
        [InlineData("select test from 123;")]
        public void Test_can_parse_single_valid_query_without_where_clause_with_terminator(string query)
        {
            var cmsqlParser = CmsqlParserFactory.CreateParserForQuery(query);
            var parseTree = cmsqlParser.queries();

            var visitor = new QueriesVisitor();
            var queries = visitor.VisitQueries(parseTree);

            queries.Should().NotBeNullOrEmpty();
            queries.Should().HaveCount(1);
        }

        [Theory]
        [InlineData("select test from start;select test from start;select test from start")]
        [InlineData("select test from root;select test from root;select test from root;")]
        [InlineData("select test from 123;select test from 123;select test from 123")]
        public void Test_can_parse_multiple_valid_queries_without_where_clause(string query)
        {
            var cmsqlParser = CmsqlParserFactory.CreateParserForQuery(query);
            var parseTree = cmsqlParser.queries();

            var visitor = new QueriesVisitor();
            var queries = visitor.VisitQueries(parseTree);

            queries.Should().NotBeNullOrEmpty();
            queries.Should().HaveCount(3);
        }
    }
}
