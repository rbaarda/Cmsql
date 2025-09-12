using Cmsql.Grammar.Parsing.Internal;
using FluentAssertions;
using Xunit;

namespace Cmsql.Test.Grammar.Parsing.Internal
{
    public class QueryVisitorTest
    {
        [Theory]
        [InlineData("select test from start")]
        [InlineData("select test from root")]
        [InlineData("select test from 123")]
        public void Test_can_parse_valid_query_without_where_clause(string query)
        {
            var cmsqlParser = CmsqlParserFactory.CreateParserForQuery(query);
            var parseTree = cmsqlParser.query();

            var visitor = new QueryVisitor();
            var cmsqlQuery = visitor.VisitQuery(parseTree);

            cmsqlQuery.Should().NotBeNull();
            cmsqlQuery.ContentType.Should().NotBeNullOrEmpty();
            cmsqlQuery.StartNode.Should().NotBeNull();
            cmsqlQuery.Criteria.Should().BeNull();
        }

        [Theory]
        [InlineData("select test from start;")]
        [InlineData("select test from root;")]
        [InlineData("select test from 123;")]
        public void Test_can_parse_valid_query_without_where_clause_with_terminator(string query)
        {
            var cmsqlParser = CmsqlParserFactory.CreateParserForQuery(query);
            var parseTree = cmsqlParser.query();

            var visitor = new QueryVisitor();
            var cmsqlQuery = visitor.VisitQuery(parseTree);

            cmsqlQuery.Should().NotBeNull();
            cmsqlQuery.ContentType.Should().NotBeNullOrEmpty();
            cmsqlQuery.StartNode.Should().NotBeNull();
            cmsqlQuery.Criteria.Should().BeNull();
        }

        [Theory]
        [InlineData("select test from start where foo = 'bar'")]
        [InlineData("select test from root where foo = 'bar'")]
        [InlineData("select test from 123 where foo = 'bar'")]
        public void Test_can_parse_valid_query_with_where_clause(string query)
        {
            var cmsqlParser = CmsqlParserFactory.CreateParserForQuery(query);
            var parseTree = cmsqlParser.query();

            var visitor = new QueryVisitor();
            var cmsqlQuery = visitor.VisitQuery(parseTree);

            cmsqlQuery.Should().NotBeNull();
            cmsqlQuery.ContentType.Should().NotBeNullOrEmpty();
            cmsqlQuery.StartNode.Should().NotBeNull();
            cmsqlQuery.Criteria.Should().NotBeNull();
        }

        [Theory]
        [InlineData("select test from start where foo = 'bar';")]
        [InlineData("select test from root where foo = 'bar';")]
        [InlineData("select test from 123 where foo = 'bar';")]
        public void Test_can_parse_valid_query_with_where_clause_with_terminator(string query)
        {
            var cmsqlParser = CmsqlParserFactory.CreateParserForQuery(query);
            var parseTree = cmsqlParser.query();

            var visitor = new QueryVisitor();
            var cmsqlQuery = visitor.VisitQuery(parseTree);

            cmsqlQuery.Should().NotBeNull();
            cmsqlQuery.ContentType.Should().NotBeNullOrEmpty();
            cmsqlQuery.StartNode.Should().NotBeNull();
            cmsqlQuery.Criteria.Should().NotBeNull();
        }
    }
}
