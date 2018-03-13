using Cmsql.Grammar;
using Cmsql.Grammar.Parsing.Internal;
using Cmsql.Query;
using FluentAssertions;
using Xunit;

namespace Cmsql.Test.Grammar.Parsing.Internal
{
    public class ConditionVisitorTest
    {
        [Fact]
        public void Test_can_parse_equals_condition()
        {
            CmsqlParser cmsqlParser = CmsqlParserFactory.CreateParserForQuery("foo = 'bar'");
            CmsqlParser.ConditionContext parseTree = cmsqlParser.condition();
            
            ConditionVisitor visitor = new ConditionVisitor();
            CmsqlQueryCondition condition = visitor.VisitCondition(parseTree);

            condition.Identifier.ShouldBeEquivalentTo("foo");
            condition.Operator.ShouldBeEquivalentTo(EqualityOperator.Equals);
            condition.Value.ShouldBeEquivalentTo("bar");
        }

        [Fact]
        public void Test_can_parse_not_equals_condition()
        {
            CmsqlParser cmsqlParser = CmsqlParserFactory.CreateParserForQuery("foo != 'bar'");
            CmsqlParser.ConditionContext parseTree = cmsqlParser.condition();

            ConditionVisitor visitor = new ConditionVisitor();
            CmsqlQueryCondition condition = visitor.VisitCondition(parseTree);

            condition.Identifier.ShouldBeEquivalentTo("foo");
            condition.Operator.ShouldBeEquivalentTo(EqualityOperator.NotEquals);
            condition.Value.ShouldBeEquivalentTo("bar");
        }

        [Fact]
        public void Test_can_parse_larger_than_condition()
        {
            CmsqlParser cmsqlParser = CmsqlParserFactory.CreateParserForQuery("foo > 'bar'");
            CmsqlParser.ConditionContext parseTree = cmsqlParser.condition();

            ConditionVisitor visitor = new ConditionVisitor();
            CmsqlQueryCondition condition = visitor.VisitCondition(parseTree);

            condition.Identifier.ShouldBeEquivalentTo("foo");
            condition.Operator.ShouldBeEquivalentTo(EqualityOperator.GreaterThan);
            condition.Value.ShouldBeEquivalentTo("bar");
        }

        [Fact]
        public void Test_can_parse_less_than_condition()
        {
            CmsqlParser cmsqlParser = CmsqlParserFactory.CreateParserForQuery("foo < 'bar'");
            CmsqlParser.ConditionContext parseTree = cmsqlParser.condition();

            ConditionVisitor visitor = new ConditionVisitor();
            CmsqlQueryCondition condition = visitor.VisitCondition(parseTree);

            condition.Identifier.ShouldBeEquivalentTo("foo");
            condition.Operator.ShouldBeEquivalentTo(EqualityOperator.LessThan);
            condition.Value.ShouldBeEquivalentTo("bar");
        }

        [Fact]
        public void Test_can_parse_larger_than_or_equals_condition()
        {
            CmsqlParser cmsqlParser = CmsqlParserFactory.CreateParserForQuery("foo >= 'bar'");
            CmsqlParser.ConditionContext parseTree = cmsqlParser.condition();

            ConditionVisitor visitor = new ConditionVisitor();
            CmsqlQueryCondition condition = visitor.VisitCondition(parseTree);

            condition.Identifier.ShouldBeEquivalentTo("foo");
            condition.Operator.ShouldBeEquivalentTo(EqualityOperator.GreaterThanOrEquals);
            condition.Value.ShouldBeEquivalentTo("bar");
        }

        [Fact]
        public void Test_can_parse_less_than_or_equals_condition()
        {
            CmsqlParser cmsqlParser = CmsqlParserFactory.CreateParserForQuery("foo <= 'bar'");
            CmsqlParser.ConditionContext parseTree = cmsqlParser.condition();

            ConditionVisitor visitor = new ConditionVisitor();
            CmsqlQueryCondition condition = visitor.VisitCondition(parseTree);

            condition.Identifier.ShouldBeEquivalentTo("foo");
            condition.Operator.ShouldBeEquivalentTo(EqualityOperator.LessThanOrEquals);
            condition.Value.ShouldBeEquivalentTo("bar");
        }
    }
}
