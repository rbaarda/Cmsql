using Cmsql.Grammar;
using Cmsql.Grammar.Parsing.Internal;
using Cmsql.Query;
using FluentAssertions;
using Xunit;

namespace Cmsql.Test.Grammar.Parsing.Internal
{
    public class FromClauseVisitorTest
    {
        [Fact]
        public void Test_can_parse_start_as_start_node()
        {
            CmsqlParser cmsqlParser = CmsqlParserFactory.CreateParserForQuery("from start");
            CmsqlParser.FromClauseContext parseTree = cmsqlParser.fromClause();

            FromClauseVisitor visitor = new FromClauseVisitor();
            CmsqlQueryStartNode startNode = visitor.VisitFromClause(parseTree);

            startNode.StartNodeId.Should().BeNullOrEmpty();
            startNode.StartNodeType.ShouldBeEquivalentTo(CmsqlQueryStartNodeType.Start);
        }

        [Fact]
        public void Test_can_parse_root_as_start_node()
        {
            CmsqlParser cmsqlParser = CmsqlParserFactory.CreateParserForQuery("from root");
            CmsqlParser.FromClauseContext parseTree = cmsqlParser.fromClause();

            FromClauseVisitor visitor = new FromClauseVisitor();
            CmsqlQueryStartNode startNode = visitor.VisitFromClause(parseTree);

            startNode.StartNodeId.Should().BeNullOrEmpty();
            startNode.StartNodeType.ShouldBeEquivalentTo(CmsqlQueryStartNodeType.Root);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("12")]
        [InlineData("123")]
        [InlineData("1234")]
        [InlineData("12345")]
        [InlineData("123456")]
        public void Test_can_parse_arbitrary_id_as_start_node(string id)
        {
            CmsqlParser cmsqlParser = CmsqlParserFactory.CreateParserForQuery($"from {id}");
            CmsqlParser.FromClauseContext parseTree = cmsqlParser.fromClause();

            FromClauseVisitor visitor = new FromClauseVisitor();
            CmsqlQueryStartNode startNode = visitor.VisitFromClause(parseTree);

            startNode.StartNodeId.ShouldBeEquivalentTo(id);
            startNode.StartNodeType.ShouldBeEquivalentTo(CmsqlQueryStartNodeType.Id);
        }
    }
}
