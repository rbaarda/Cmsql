using Cql.Grammar.Parser.Internal;
using Cql.Query;
using FluentAssertions;
using Xunit;

namespace Cql.Grammar.Parser.Test.Internal
{
    public class FromClauseVisitorTest
    {
        [Fact]
        public void Test_can_parse_start_as_start_node()
        {
            CqlParser cqlParser = CqlParserFactory.CreateParserForQuery("from start");
            CqlParser.FromClauseContext parseTree = cqlParser.fromClause();

            FromClauseVisitor visitor = new FromClauseVisitor();
            CqlQueryStartNode startNode = visitor.VisitFromClause(parseTree);

            startNode.StartNodeId.Should().BeNullOrEmpty();
            startNode.StartNodeType.ShouldBeEquivalentTo(CqlQueryStartNodeType.Start);
        }

        [Fact]
        public void Test_can_parse_root_as_start_node()
        {
            CqlParser cqlParser = CqlParserFactory.CreateParserForQuery("from root");
            CqlParser.FromClauseContext parseTree = cqlParser.fromClause();

            FromClauseVisitor visitor = new FromClauseVisitor();
            CqlQueryStartNode startNode = visitor.VisitFromClause(parseTree);

            startNode.StartNodeId.Should().BeNullOrEmpty();
            startNode.StartNodeType.ShouldBeEquivalentTo(CqlQueryStartNodeType.Root);
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
            CqlParser cqlParser = CqlParserFactory.CreateParserForQuery($"from {id}");
            CqlParser.FromClauseContext parseTree = cqlParser.fromClause();

            FromClauseVisitor visitor = new FromClauseVisitor();
            CqlQueryStartNode startNode = visitor.VisitFromClause(parseTree);

            startNode.StartNodeId.ShouldBeEquivalentTo(id);
            startNode.StartNodeType.ShouldBeEquivalentTo(CqlQueryStartNodeType.Id);
        }
    }
}
