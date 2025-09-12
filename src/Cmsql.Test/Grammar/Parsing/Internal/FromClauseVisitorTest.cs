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
            var cmsqlParser = CmsqlParserFactory.CreateParserForQuery("from start");
            var parseTree = cmsqlParser.fromClause();

            var visitor = new FromClauseVisitor();
            var startNode = visitor.VisitFromClause(parseTree);

            startNode.StartNodeId.Should().BeNullOrEmpty();
            startNode.StartNodeType.Should().Be(CmsqlQueryStartNodeType.Start);
        }

        [Fact]
        public void Test_can_parse_root_as_start_node()
        {
            var cmsqlParser = CmsqlParserFactory.CreateParserForQuery("from root");
            var parseTree = cmsqlParser.fromClause();

            var visitor = new FromClauseVisitor();
            var startNode = visitor.VisitFromClause(parseTree);

            startNode.StartNodeId.Should().BeNullOrEmpty();
            startNode.StartNodeType.Should().Be(CmsqlQueryStartNodeType.Root);
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
            var cmsqlParser = CmsqlParserFactory.CreateParserForQuery($"from {id}");
            var parseTree = cmsqlParser.fromClause();

            var visitor = new FromClauseVisitor();
            var startNode = visitor.VisitFromClause(parseTree);

            startNode.StartNodeId.Should().BeEquivalentTo(id);
            startNode.StartNodeType.Should().Be(CmsqlQueryStartNodeType.Id);
        }
    }
}
