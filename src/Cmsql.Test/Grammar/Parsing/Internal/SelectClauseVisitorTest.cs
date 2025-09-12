using Cmsql.Grammar;
using Cmsql.Grammar.Parsing.Internal;
using FluentAssertions;
using Xunit;

namespace Cmsql.Test.Grammar.Parsing.Internal
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
            CmsqlParser cmsqlParser = CmsqlParserFactory.CreateParserForQuery($"select {queryIdentifier}");
            CmsqlParser.SelectClauseContext parseTree = cmsqlParser.selectClause();

            SelectClauseVisitor visitor = new SelectClauseVisitor();
            string identifier = visitor.VisitSelectClause(parseTree);
            
            identifier.Should().BeEquivalentTo(queryIdentifier);
        }
    }
}
