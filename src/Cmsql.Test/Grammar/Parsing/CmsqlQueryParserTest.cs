using System.Linq;
using Cmsql.Grammar.Parsing;
using Cmsql.Query;
using FluentAssertions;
using Xunit;

namespace Cmsql.Test.Grammar.Parsing
{
    public class CmsqlQueryParserTest
    {
        [Fact]
        public void Test_can_parse_single_query_without_terminator()
        {
            CmsqlQueryParser parser = new CmsqlQueryParser();
            CmsqlQueryParseResult parseResult = parser.Parse("select pages from start");

            parseResult.Queries.Should().NotBeNullOrEmpty();
            parseResult.Queries.Should().HaveCount(1);
            parseResult.Errors.Should().BeNullOrEmpty();

            CmsqlQuery query = parseResult.Queries.First();
            query.ContentType.Should().BeEquivalentTo("pages");
            query.StartNode.StartNodeId.Should().BeNullOrEmpty();
            query.StartNode.StartNodeType.Should().Be(CmsqlQueryStartNodeType.Start);
            query.Criteria.Should().BeNull();
        }

        [Fact]
        public void Test_can_parse_single_query_with_terminator()
        {
            CmsqlQueryParser parser = new CmsqlQueryParser();
            CmsqlQueryParseResult parseResult = parser.Parse("select test from start;");

            parseResult.Queries.Should().NotBeNullOrEmpty();
            parseResult.Queries.Should().HaveCount(1);
            parseResult.Errors.Should().BeNullOrEmpty();

            CmsqlQuery query = parseResult.Queries.First();
            query.ContentType.Should().BeEquivalentTo("test");
            query.StartNode.StartNodeId.Should().BeNullOrEmpty();
            query.StartNode.StartNodeType.Should().Be(CmsqlQueryStartNodeType.Start);
            query.Criteria.Should().BeNull();
        }

        [Fact]
        public void Test_can_parse_multiple_queries()
        {
            CmsqlQueryParser parser = new CmsqlQueryParser();
            CmsqlQueryParseResult parseResult = parser.Parse(
                "select test from start;select test from root where foo = 'bar';select barf from 123 where (foo = 'bar' and bar = 'foo') or (bla = 'bli' and bli = 'bla')");
            
            parseResult.Queries.Should().HaveCount(3);
            parseResult.Errors.Should().BeNullOrEmpty();

            CmsqlQuery firstQuery = parseResult.Queries.First();
            firstQuery.ContentType.Should().BeEquivalentTo("test");
            firstQuery.StartNode.StartNodeId.Should().BeNullOrEmpty();
            firstQuery.StartNode.StartNodeType.Should().Be(CmsqlQueryStartNodeType.Start);
            firstQuery.Criteria.Should().BeNull();

            CmsqlQuery secondQuery = parseResult.Queries.ElementAt(1);
            secondQuery.ContentType.Should().BeEquivalentTo("test");
            secondQuery.StartNode.StartNodeId.Should().BeNullOrEmpty();
            secondQuery.StartNode.StartNodeType.Should().Be(CmsqlQueryStartNodeType.Root);
            secondQuery.Criteria.Should().BeOfType<CmsqlQueryCondition>();
            CmsqlQueryCondition condition = secondQuery.Criteria as CmsqlQueryCondition;
            condition.Identifier.Should().BeEquivalentTo("foo");
            condition.Operator.Should().Be(EqualityOperator.Equals);
            condition.Value.Should().Be("bar");

            CmsqlQuery thirdQuery = parseResult.Queries.ElementAt(2);
            thirdQuery.ContentType.Should().BeEquivalentTo("barf");
            thirdQuery.StartNode.StartNodeId.Should().BeEquivalentTo("123");
            thirdQuery.StartNode.StartNodeType.Should().Be(CmsqlQueryStartNodeType.Id);
            thirdQuery.Criteria.Should().BeOfType<CmsqlQueryBinaryExpression>();
        }

        [Fact]
        public void Test_cannot_parse_invalid_query()
        {
            CmsqlQueryParser parser = new CmsqlQueryParser();
            CmsqlQueryParseResult parseResult = parser.Parse("select test-test from start");

            parseResult.Errors.Should().HaveCount(1);
        }
    }
}
