using Cql.Grammar;
using Cql.Grammar.Parsing.Internal;
using Cql.Query;
using FluentAssertions;
using Xunit;

namespace Cql.Test.Grammar.Parsing.Internal
{
    public class ConditionVisitorTest
    {
        [Fact]
        public void Test_can_parse_equals_condition()
        {
            CqlParser cqlParser = CqlParserFactory.CreateParserForQuery("foo = 'bar'");
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
            CqlParser cqlParser = CqlParserFactory.CreateParserForQuery("foo != 'bar'");
            CqlParser.ConditionContext parseTree = cqlParser.condition();

            ConditionVisitor visitor = new ConditionVisitor();
            CqlQueryCondition condition = visitor.VisitCondition(parseTree);

            condition.Identifier.ShouldBeEquivalentTo("foo");
            condition.Operator.ShouldBeEquivalentTo(EqualityOperator.NotEquals);
            condition.Value.ShouldBeEquivalentTo("bar");
        }

        [Fact]
        public void Test_can_parse_larger_than_condition()
        {
            CqlParser cqlParser = CqlParserFactory.CreateParserForQuery("foo > 'bar'");
            CqlParser.ConditionContext parseTree = cqlParser.condition();

            ConditionVisitor visitor = new ConditionVisitor();
            CqlQueryCondition condition = visitor.VisitCondition(parseTree);

            condition.Identifier.ShouldBeEquivalentTo("foo");
            condition.Operator.ShouldBeEquivalentTo(EqualityOperator.GreaterThan);
            condition.Value.ShouldBeEquivalentTo("bar");
        }

        [Fact]
        public void Test_can_parse_less_than_condition()
        {
            CqlParser cqlParser = CqlParserFactory.CreateParserForQuery("foo < 'bar'");
            CqlParser.ConditionContext parseTree = cqlParser.condition();

            ConditionVisitor visitor = new ConditionVisitor();
            CqlQueryCondition condition = visitor.VisitCondition(parseTree);

            condition.Identifier.ShouldBeEquivalentTo("foo");
            condition.Operator.ShouldBeEquivalentTo(EqualityOperator.LessThan);
            condition.Value.ShouldBeEquivalentTo("bar");
        }

        [Fact]
        public void Test_can_parse_larger_than_or_equals_condition()
        {
            CqlParser cqlParser = CqlParserFactory.CreateParserForQuery("foo >= 'bar'");
            CqlParser.ConditionContext parseTree = cqlParser.condition();

            ConditionVisitor visitor = new ConditionVisitor();
            CqlQueryCondition condition = visitor.VisitCondition(parseTree);

            condition.Identifier.ShouldBeEquivalentTo("foo");
            condition.Operator.ShouldBeEquivalentTo(EqualityOperator.GreaterThanOrEquals);
            condition.Value.ShouldBeEquivalentTo("bar");
        }

        [Fact]
        public void Test_can_parse_less_than_or_equals_condition()
        {
            CqlParser cqlParser = CqlParserFactory.CreateParserForQuery("foo <= 'bar'");
            CqlParser.ConditionContext parseTree = cqlParser.condition();

            ConditionVisitor visitor = new ConditionVisitor();
            CqlQueryCondition condition = visitor.VisitCondition(parseTree);

            condition.Identifier.ShouldBeEquivalentTo("foo");
            condition.Operator.ShouldBeEquivalentTo(EqualityOperator.LessThanOrEquals);
            condition.Value.ShouldBeEquivalentTo("bar");
        }
    }
}
