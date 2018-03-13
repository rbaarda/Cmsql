using Cmsql.Grammar;
using Cmsql.Grammar.Parsing.Internal;
using Cmsql.Query;
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
            CmsqlParser cmsqlParser = CmsqlParserFactory.CreateParserForQuery(query);
            CmsqlParser.QueryContext parseTree = cmsqlParser.query();

            QueryVisitor visitor = new QueryVisitor();
            CmsqlQuery cmsqlQuery = visitor.VisitQuery(parseTree);

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
            CmsqlParser cmsqlParser = CmsqlParserFactory.CreateParserForQuery(query);
            CmsqlParser.QueryContext parseTree = cmsqlParser.query();

            QueryVisitor visitor = new QueryVisitor();
            CmsqlQuery cmsqlQuery = visitor.VisitQuery(parseTree);

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
            CmsqlParser cmsqlParser = CmsqlParserFactory.CreateParserForQuery(query);
            CmsqlParser.QueryContext parseTree = cmsqlParser.query();

            QueryVisitor visitor = new QueryVisitor();
            CmsqlQuery cmsqlQuery = visitor.VisitQuery(parseTree);

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
            CmsqlParser cmsqlParser = CmsqlParserFactory.CreateParserForQuery(query);
            CmsqlParser.QueryContext parseTree = cmsqlParser.query();

            QueryVisitor visitor = new QueryVisitor();
            CmsqlQuery cmsqlQuery = visitor.VisitQuery(parseTree);

            cmsqlQuery.Should().NotBeNull();
            cmsqlQuery.ContentType.Should().NotBeNullOrEmpty();
            cmsqlQuery.StartNode.Should().NotBeNull();
            cmsqlQuery.Criteria.Should().NotBeNull();
        }
    }
}
