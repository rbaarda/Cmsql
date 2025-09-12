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

            condition.Identifier.Should().BeEquivalentTo("foo");
            condition.Operator.Should().Be(EqualityOperator.Equals);
            condition.Value.Should().BeEquivalentTo("bar");
        }

        [Fact]
        public void Test_can_parse_not_equals_condition()
        {
            CmsqlParser cmsqlParser = CmsqlParserFactory.CreateParserForQuery("foo != 'bar'");
            CmsqlParser.ConditionContext parseTree = cmsqlParser.condition();

            ConditionVisitor visitor = new ConditionVisitor();
            CmsqlQueryCondition condition = visitor.VisitCondition(parseTree);

            condition.Identifier.Should().BeEquivalentTo("foo");
            condition.Operator.Should().Be(EqualityOperator.NotEquals);
            condition.Value.Should().Be("bar");
        }

        [Fact]
        public void Test_can_parse_larger_than_condition()
        {
            CmsqlParser cmsqlParser = CmsqlParserFactory.CreateParserForQuery("foo > 'bar'");
            CmsqlParser.ConditionContext parseTree = cmsqlParser.condition();

            ConditionVisitor visitor = new ConditionVisitor();
            CmsqlQueryCondition condition = visitor.VisitCondition(parseTree);

            condition.Identifier.Should().BeEquivalentTo("foo");
            condition.Operator.Should().Be(EqualityOperator.GreaterThan);
            condition.Value.Should().BeEquivalentTo("bar");
        }

        [Fact]
        public void Test_can_parse_less_than_condition()
        {
            CmsqlParser cmsqlParser = CmsqlParserFactory.CreateParserForQuery("foo < 'bar'");
            CmsqlParser.ConditionContext parseTree = cmsqlParser.condition();

            ConditionVisitor visitor = new ConditionVisitor();
            CmsqlQueryCondition condition = visitor.VisitCondition(parseTree);

            condition.Identifier.Should().BeEquivalentTo("foo");
            condition.Operator.Should().Be(EqualityOperator.LessThan);
            condition.Value.Should().BeEquivalentTo("bar");
        }

        [Fact]
        public void Test_can_parse_larger_than_or_equals_condition()
        {
            CmsqlParser cmsqlParser = CmsqlParserFactory.CreateParserForQuery("foo >= 'bar'");
            CmsqlParser.ConditionContext parseTree = cmsqlParser.condition();

            ConditionVisitor visitor = new ConditionVisitor();
            CmsqlQueryCondition condition = visitor.VisitCondition(parseTree);

            condition.Identifier.Should().BeEquivalentTo("foo");
            condition.Operator.Should().Be(EqualityOperator.GreaterThanOrEquals);
            condition.Value.Should().BeEquivalentTo("bar");
        }

        [Fact]
        public void Test_can_parse_less_than_or_equals_condition()
        {
            CmsqlParser cmsqlParser = CmsqlParserFactory.CreateParserForQuery("foo <= 'bar'");
            CmsqlParser.ConditionContext parseTree = cmsqlParser.condition();

            ConditionVisitor visitor = new ConditionVisitor();
            CmsqlQueryCondition condition = visitor.VisitCondition(parseTree);

            condition.Identifier.Should().BeEquivalentTo("foo");
            condition.Operator.Should().Be(EqualityOperator.LessThanOrEquals);
            condition.Value.Should().BeEquivalentTo("bar");
        }
    }
}
