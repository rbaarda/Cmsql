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
            query.ContentType.ShouldBeEquivalentTo("pages");
            query.StartNode.StartNodeId.Should().BeNullOrEmpty();
            query.StartNode.StartNodeType.ShouldBeEquivalentTo(CmsqlQueryStartNodeType.Start);
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
            query.ContentType.ShouldBeEquivalentTo("test");
            query.StartNode.StartNodeId.Should().BeNullOrEmpty();
            query.StartNode.StartNodeType.ShouldBeEquivalentTo(CmsqlQueryStartNodeType.Start);
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
            firstQuery.ContentType.ShouldBeEquivalentTo("test");
            firstQuery.StartNode.StartNodeId.Should().BeNullOrEmpty();
            firstQuery.StartNode.StartNodeType.ShouldBeEquivalentTo(CmsqlQueryStartNodeType.Start);
            firstQuery.Criteria.Should().BeNull();

            CmsqlQuery secondQuery = parseResult.Queries.ElementAt(1);
            secondQuery.ContentType.ShouldBeEquivalentTo("test");
            secondQuery.StartNode.StartNodeId.Should().BeNullOrEmpty();
            secondQuery.StartNode.StartNodeType.ShouldBeEquivalentTo(CmsqlQueryStartNodeType.Root);
            secondQuery.Criteria.Should().BeOfType<CmsqlQueryCondition>();
            CmsqlQueryCondition condition = secondQuery.Criteria as CmsqlQueryCondition;
            condition.Identifier.ShouldBeEquivalentTo("foo");
            condition.Operator.ShouldBeEquivalentTo(EqualityOperator.Equals);
            condition.Value.ShouldBeEquivalentTo("bar");

            CmsqlQuery thirdQuery = parseResult.Queries.ElementAt(2);
            thirdQuery.ContentType.ShouldBeEquivalentTo("barf");
            thirdQuery.StartNode.StartNodeId.ShouldBeEquivalentTo("123");
            thirdQuery.StartNode.StartNodeType.ShouldBeEquivalentTo(CmsqlQueryStartNodeType.Id);
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
