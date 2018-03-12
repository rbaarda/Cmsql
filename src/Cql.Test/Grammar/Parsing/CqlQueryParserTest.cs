using System.Linq;
using Cmsql.Grammar.Parsing;
using Cmsql.Query;
using FluentAssertions;
using Xunit;

namespace Cmsql.Test.Grammar.Parsing
{
    public class CqlQueryParserTest
    {
        [Fact]
        public void Test_can_parse_single_query_without_terminator()
        {
            CqlQueryParser parser = new CqlQueryParser();
            CqlQueryParseResult parseResult = parser.Parse("select pages from start");

            parseResult.Queries.Should().NotBeNullOrEmpty();
            parseResult.Queries.Should().HaveCount(1);
            parseResult.Errors.Should().BeNullOrEmpty();

            CqlQuery query = parseResult.Queries.First();
            query.ContentType.ShouldBeEquivalentTo("pages");
            query.StartNode.StartNodeId.Should().BeNullOrEmpty();
            query.StartNode.StartNodeType.ShouldBeEquivalentTo(CqlQueryStartNodeType.Start);
            query.Criteria.Should().BeNull();
        }

        [Fact]
        public void Test_can_parse_single_query_with_terminator()
        {
            CqlQueryParser parser = new CqlQueryParser();
            CqlQueryParseResult parseResult = parser.Parse("select test from start;");

            parseResult.Queries.Should().NotBeNullOrEmpty();
            parseResult.Queries.Should().HaveCount(1);
            parseResult.Errors.Should().BeNullOrEmpty();

            CqlQuery query = parseResult.Queries.First();
            query.ContentType.ShouldBeEquivalentTo("test");
            query.StartNode.StartNodeId.Should().BeNullOrEmpty();
            query.StartNode.StartNodeType.ShouldBeEquivalentTo(CqlQueryStartNodeType.Start);
            query.Criteria.Should().BeNull();
        }

        [Fact]
        public void Test_can_parse_multiple_queries()
        {
            CqlQueryParser parser = new CqlQueryParser();
            CqlQueryParseResult parseResult = parser.Parse(
                "select test from start;select test from root where foo = 'bar';select barf from 123 where (foo = 'bar' and bar = 'foo') or (bla = 'bli' and bli = 'bla')");
            
            parseResult.Queries.Should().HaveCount(3);
            parseResult.Errors.Should().BeNullOrEmpty();

            CqlQuery firstQuery = parseResult.Queries.First();
            firstQuery.ContentType.ShouldBeEquivalentTo("test");
            firstQuery.StartNode.StartNodeId.Should().BeNullOrEmpty();
            firstQuery.StartNode.StartNodeType.ShouldBeEquivalentTo(CqlQueryStartNodeType.Start);
            firstQuery.Criteria.Should().BeNull();

            CqlQuery secondQuery = parseResult.Queries.ElementAt(1);
            secondQuery.ContentType.ShouldBeEquivalentTo("test");
            secondQuery.StartNode.StartNodeId.Should().BeNullOrEmpty();
            secondQuery.StartNode.StartNodeType.ShouldBeEquivalentTo(CqlQueryStartNodeType.Root);
            secondQuery.Criteria.Should().BeOfType<CqlQueryCondition>();
            CqlQueryCondition condition = secondQuery.Criteria as CqlQueryCondition;
            condition.Identifier.ShouldBeEquivalentTo("foo");
            condition.Operator.ShouldBeEquivalentTo(EqualityOperator.Equals);
            condition.Value.ShouldBeEquivalentTo("bar");

            CqlQuery thirdQuery = parseResult.Queries.ElementAt(2);
            thirdQuery.ContentType.ShouldBeEquivalentTo("barf");
            thirdQuery.StartNode.StartNodeId.ShouldBeEquivalentTo("123");
            thirdQuery.StartNode.StartNodeType.ShouldBeEquivalentTo(CqlQueryStartNodeType.Id);
            thirdQuery.Criteria.Should().BeOfType<CqlQueryBinaryExpression>();
        }

        [Fact]
        public void Test_cannot_parse_invalid_query()
        {
            CqlQueryParser parser = new CqlQueryParser();
            CqlQueryParseResult parseResult = parser.Parse("select test-test from start");

            parseResult.Errors.Should().HaveCount(1);
        }
    }
}
