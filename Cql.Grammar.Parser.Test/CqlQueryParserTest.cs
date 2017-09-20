using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime.Misc;
using Cql.Query;
using FluentAssertions;
using Xunit;

namespace Cql.Grammar.Parser.Test
{
    public class CqlQueryParserTest
    {
        [Fact]
        public void Test_can_parse_single_query_without_terminator()
        {
            CqlQueryParser parser = new CqlQueryParser();
            IEnumerable<CqlQuery> queries = parser.Parse("select pages from start");

            queries.Should().NotBeNullOrEmpty();
            queries.Should().HaveCount(1);
            CqlQuery query = queries.First();
            query.ContentType.ShouldBeEquivalentTo("pages");
            query.StartNode.StartNodeId.Should().BeNullOrEmpty();
            query.StartNode.StartNodeType.ShouldBeEquivalentTo(CqlQueryStartNodeType.Start);
            query.Criteria.Should().BeNull();
        }

        [Fact]
        public void Test_can_parse_single_query_with_terminator()
        {
            CqlQueryParser parser = new CqlQueryParser();
            IEnumerable<CqlQuery> queries = parser.Parse("select test from start;");

            queries.Should().NotBeNullOrEmpty();
            queries.Should().HaveCount(1);
            CqlQuery query = queries.First();
            query.ContentType.ShouldBeEquivalentTo("test");
            query.StartNode.StartNodeId.Should().BeNullOrEmpty();
            query.StartNode.StartNodeType.ShouldBeEquivalentTo(CqlQueryStartNodeType.Start);
            query.Criteria.Should().BeNull();
        }

        [Fact]
        public void Test_can_parse_multiple_queries()
        {
            CqlQueryParser parser = new CqlQueryParser();
            IEnumerable<CqlQuery> queries = parser.Parse(
                "select test from start;select test from root where foo = 'bar';select barf from 123 where (foo = 'bar' and bar = 'foo') or (bla = 'bli' and bli = 'bla')");
            
            queries.Should().HaveCount(3);

            CqlQuery firstQuery = queries.First();
            firstQuery.ContentType.ShouldBeEquivalentTo("test");
            firstQuery.StartNode.StartNodeId.Should().BeNullOrEmpty();
            firstQuery.StartNode.StartNodeType.ShouldBeEquivalentTo(CqlQueryStartNodeType.Start);
            firstQuery.Criteria.Should().BeNull();

            CqlQuery secondQuery = queries.ElementAt(1);
            secondQuery.ContentType.ShouldBeEquivalentTo("test");
            secondQuery.StartNode.StartNodeId.Should().BeNullOrEmpty();
            secondQuery.StartNode.StartNodeType.ShouldBeEquivalentTo(CqlQueryStartNodeType.Root);
            secondQuery.Criteria.Should().BeOfType<CqlQueryCondition>();
            CqlQueryCondition condition = secondQuery.Criteria as CqlQueryCondition;
            condition.Identifier.ShouldBeEquivalentTo("foo");
            condition.Operator.ShouldBeEquivalentTo(EqualityOperator.Equals);
            condition.Value.ShouldBeEquivalentTo("bar");

            CqlQuery thirdQuery = queries.ElementAt(2);
            thirdQuery.ContentType.ShouldBeEquivalentTo("barf");
            thirdQuery.StartNode.StartNodeId.ShouldBeEquivalentTo("123");
            thirdQuery.StartNode.StartNodeType.ShouldBeEquivalentTo(CqlQueryStartNodeType.Id);
            thirdQuery.Criteria.Should().BeOfType<CqlQueryExpression>();
        }

        [Fact]
        public void Test_cannot_parse_invalid_query()
        {
            CqlQueryParser parser = new CqlQueryParser();
            parser.Invoking(x => x.Parse("select test-test from start")).ShouldThrow<ParseCanceledException>();
        }
    }
}
