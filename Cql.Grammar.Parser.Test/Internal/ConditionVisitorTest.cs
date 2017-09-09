using Antlr4.Runtime;
using Cql.Grammar.Parser.Internal;
using Cql.Query;
using FluentAssertions;
using Xunit;

namespace Cql.Grammar.Parser.Test.Internal
{
    public class ConditionVisitorTest
    {
        [Fact]
        public void Test_can_parse_equals_condition()
        {
            CqlParser cqlParser = CreateParserForQuery("foo = 'bar'");
            CqlParser.ConditionContext parseTree = cqlParser.condition();
            
            ConditionVisitor visitor = new ConditionVisitor();
            CqlQueryCondition condition = visitor.VisitCondition(parseTree);

            condition.Identifier.ShouldBeEquivalentTo("foo");
            condition.Operator.ShouldBeEquivalentTo(EqualityOperator.Equals);
            condition.Value.ShouldBeEquivalentTo("bar");
        }

        [Fact]
        public void Test_can_parse_not_equals_condition()
        {
            CqlParser cqlParser = CreateParserForQuery("foo != 'bar'");
            CqlParser.ConditionContext parseTree = cqlParser.condition();

            ConditionVisitor visitor = new ConditionVisitor();
            CqlQueryCondition condition = visitor.VisitCondition(parseTree);

            condition.Identifier.ShouldBeEquivalentTo("foo");
            condition.Operator.ShouldBeEquivalentTo(EqualityOperator.NotEquals);
            condition.Value.ShouldBeEquivalentTo("bar");
        }

        private CqlParser CreateParserForQuery(string query)
        {
            return new CqlParser(
                new CommonTokenStream(
                    new CqlLexer(
                        new AntlrInputStream(query))));
        }
    }
}
