using System.Collections.Generic;
using Cql.Grammar.Parsing.Internal;
using Cql.Query;
using FluentAssertions;
using Xunit;

namespace Cql.Grammar.Parsing.Test.Internal
{
    public class QueriesVisitorTest
    {
        [Theory]
        [InlineData("select test from start")]
        [InlineData("select test from root")]
        [InlineData("select test from 123")]
        public void Test_can_parse_single_valid_query_without_where_clause(string query)
        {
            CqlParser cqlParser = CqlParserFactory.CreateParserForQuery(query);
            CqlParser.QueriesContext parseTree = cqlParser.queries();

            QueriesVisitor visitor = new QueriesVisitor();
            IEnumerable<CqlQuery> cqlQueries = visitor.VisitQueries(parseTree);

            cqlQueries.Should().NotBeNullOrEmpty();
            cqlQueries.Should().HaveCount(1);
        }

        [Theory]
        [InlineData("select test from start;")]
        [InlineData("select test from root;")]
        [InlineData("select test from 123;")]
        public void Test_can_parse_single_valid_query_without_where_clause_with_terminator(string query)
        {
            CqlParser cqlParser = CqlParserFactory.CreateParserForQuery(query);
            CqlParser.QueriesContext parseTree = cqlParser.queries();

            QueriesVisitor visitor = new QueriesVisitor();
            IEnumerable<CqlQuery> cqlQueries = visitor.VisitQueries(parseTree);

            cqlQueries.Should().NotBeNullOrEmpty();
            cqlQueries.Should().HaveCount(1);
        }

        [Theory]
        [InlineData("select test from start;select test from start;select test from start")]
        [InlineData("select test from root;select test from root;select test from root;")]
        [InlineData("select test from 123;select test from 123;select test from 123")]
        public void Test_can_parse_multiple_valid_queries_without_where_clause(string query)
        {
            CqlParser cqlParser = CqlParserFactory.CreateParserForQuery(query);
            CqlParser.QueriesContext parseTree = cqlParser.queries();

            QueriesVisitor visitor = new QueriesVisitor();
            IEnumerable<CqlQuery> cqlQueries = visitor.VisitQueries(parseTree);

            cqlQueries.Should().NotBeNullOrEmpty();
            cqlQueries.Should().HaveCount(3);
        }
    }
}
