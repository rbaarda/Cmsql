using Cql.Grammar.Parsing.Internal;
using FluentAssertions;
using Xunit;

namespace Cql.Grammar.Parsing.Test.Internal
{
    public class SelectClauseVisitorTest
    {
        [Theory]
        [InlineData("test")]
        [InlineData("Test")]
        [InlineData("test123")]
        [InlineData("test123test")]
        [InlineData("test123Test")]
        [InlineData("Test123")]
        [InlineData("Test123test")]
        [InlineData("Test123Test")]
        public void Test_can_parse_valid_content_type_identifier(string queryIdentifier)
        {
            CqlParser cqlParser = CqlParserFactory.CreateParserForQuery($"select {queryIdentifier}");
            CqlParser.SelectClauseContext parseTree = cqlParser.selectClause();

            SelectClauseVisitor visitor = new SelectClauseVisitor();
            string identifier = visitor.VisitSelectClause(parseTree);
            
            identifier.ShouldBeEquivalentTo(queryIdentifier);
        }
    }
}
