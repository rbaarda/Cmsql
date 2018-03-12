using System.Collections.Generic;
using Cmsql.Grammar;
using Cmsql.Grammar.Parsing.Internal;
using Cmsql.Query;
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
            CmsqlParser cmsqlParser = CmsqlParserFactory.CreateParserForQuery(query);
            CmsqlParser.QueriesContext parseTree = cmsqlParser.queries();

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
            CmsqlParser cmsqlParser = CmsqlParserFactory.CreateParserForQuery(query);
            CmsqlParser.QueriesContext parseTree = cmsqlParser.queries();

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
            CmsqlParser cmsqlParser = CmsqlParserFactory.CreateParserForQuery(query);
            CmsqlParser.QueriesContext parseTree = cmsqlParser.queries();

            QueriesVisitor visitor = new QueriesVisitor();
            IEnumerable<CqlQuery> cqlQueries = visitor.VisitQueries(parseTree);

            cqlQueries.Should().NotBeNullOrEmpty();
            cqlQueries.Should().HaveCount(3);
        }
    }
}
