using Cql.Grammar.Parser.Internal;
using Cql.Query;
using FluentAssertions;
using Xunit;

namespace Cql.Grammar.Parser.Test.Internal
{
    public class QueryVisitorTest
    {
        [Theory]
        [InlineData("select test from start")]
        [InlineData("select test from root")]
        [InlineData("select test from 123")]
        public void Test_can_parse_valid_query_without_where_clause(string query)
        {
            CqlParser cqlParser = CqlParserFactory.CreateParserForQuery(query);
            CqlParser.QueryContext parseTree = cqlParser.query();

            QueryVisitor visitor = new QueryVisitor();
            CqlQuery cqlQuery = visitor.VisitQuery(parseTree);

            cqlQuery.Should().NotBeNull();
            cqlQuery.ContentType.Should().NotBeNullOrEmpty();
            cqlQuery.StartNode.Should().NotBeNull();
            cqlQuery.Criteria.Should().BeNull();
        }

        [Theory]
        [InlineData("select test from start;")]
        [InlineData("select test from root;")]
        [InlineData("select test from 123;")]
        public void Test_can_parse_valid_query_without_where_clause_with_terminator(string query)
        {
            CqlParser cqlParser = CqlParserFactory.CreateParserForQuery(query);
            CqlParser.QueryContext parseTree = cqlParser.query();

            QueryVisitor visitor = new QueryVisitor();
            CqlQuery cqlQuery = visitor.VisitQuery(parseTree);

            cqlQuery.Should().NotBeNull();
            cqlQuery.ContentType.Should().NotBeNullOrEmpty();
            cqlQuery.StartNode.Should().NotBeNull();
            cqlQuery.Criteria.Should().BeNull();
        }

        [Theory]
        [InlineData("select test from start where foo = 'bar'")]
        [InlineData("select test from root where foo = 'bar'")]
        [InlineData("select test from 123 where foo = 'bar'")]
        public void Test_can_parse_valid_query_with_where_clause(string query)
        {
            CqlParser cqlParser = CqlParserFactory.CreateParserForQuery(query);
            CqlParser.QueryContext parseTree = cqlParser.query();

            QueryVisitor visitor = new QueryVisitor();
            CqlQuery cqlQuery = visitor.VisitQuery(parseTree);

            cqlQuery.Should().NotBeNull();
            cqlQuery.ContentType.Should().NotBeNullOrEmpty();
            cqlQuery.StartNode.Should().NotBeNull();
            cqlQuery.Criteria.Should().NotBeNull();
        }

        [Theory]
        [InlineData("select test from start where foo = 'bar';")]
        [InlineData("select test from root where foo = 'bar';")]
        [InlineData("select test from 123 where foo = 'bar';")]
        public void Test_can_parse_valid_query_with_where_clause_with_terminator(string query)
        {
            CqlParser cqlParser = CqlParserFactory.CreateParserForQuery(query);
            CqlParser.QueryContext parseTree = cqlParser.query();

            QueryVisitor visitor = new QueryVisitor();
            CqlQuery cqlQuery = visitor.VisitQuery(parseTree);

            cqlQuery.Should().NotBeNull();
            cqlQuery.ContentType.Should().NotBeNullOrEmpty();
            cqlQuery.StartNode.Should().NotBeNull();
            cqlQuery.Criteria.Should().NotBeNull();
        }
    }
}
