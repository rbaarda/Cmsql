using Cmsql.Grammar.Parsing.Internal;
using Cmsql.Query;
using FluentAssertions;
using Xunit;

namespace Cmsql.Test.Grammar.Parsing.Internal
{
    public class WhereClauseVisitorTest
    {
        [Fact]
        public void Test_can_parse_single_condition_as_expression()
        {
            var cmsqlParser = CmsqlParserFactory.CreateParserForQuery("where foo != 'bar'");
            var parseTree = cmsqlParser.whereClause();

            var visitor = new WhereClauseVisitor();
            var expression = visitor.VisitWhereClause(parseTree);

            expression.Should().BeOfType<CmsqlQueryCondition>();
        }

        [Fact]
        public void Test_can_parse_two_conditions_as_expression()
        {
            var cmsqlParser = CmsqlParserFactory.CreateParserForQuery("where foo != 'bar' and bar = 'foo'");
            var parseTree = cmsqlParser.whereClause();

            var visitor = new WhereClauseVisitor();
            var binaryExpression = visitor.VisitWhereClause(parseTree) as CmsqlQueryBinaryExpression;
            
            binaryExpression.Operator.Should().Be(ConditionalOperator.And);
            binaryExpression.LeftExpression.Should().BeOfType<CmsqlQueryCondition>();
            binaryExpression.RightExpression.Should().BeOfType<CmsqlQueryCondition>();
        }

        [Fact]
        public void Test_can_parse_grouped_expression()
        {
            var cmsqlParser = CmsqlParserFactory.CreateParserForQuery("where (foo != 'bar' and bar = 'foo')");
            var parseTree = cmsqlParser.whereClause();

            var visitor = new WhereClauseVisitor();
            var binaryExpression = visitor.VisitWhereClause(parseTree) as CmsqlQueryBinaryExpression;

            binaryExpression.Operator.Should().Be(ConditionalOperator.And);
            binaryExpression.LeftExpression.Should().BeOfType<CmsqlQueryCondition>();
            binaryExpression.RightExpression.Should().BeOfType<CmsqlQueryCondition>();
        }

        [Fact]
        public void Test_can_parse_condition_and_grouped_expression()
        {
            var cmsqlParser = CmsqlParserFactory.CreateParserForQuery("where foo != 'bar' or (bar = 'foo' and foo = 'bar')");
            var parseTree = cmsqlParser.whereClause();

            var visitor = new WhereClauseVisitor();
            var binaryExpression = visitor.VisitWhereClause(parseTree) as CmsqlQueryBinaryExpression;

            binaryExpression.Operator.Should().Be(ConditionalOperator.Or);
            binaryExpression.LeftExpression.Should().BeOfType<CmsqlQueryCondition>();
            binaryExpression.RightExpression.Should().BeOfType<CmsqlQueryBinaryExpression>();
        }

        [Fact]
        public void Test_can_parse_grouped_expression_and_condition()
        {
            var cmsqlParser = CmsqlParserFactory.CreateParserForQuery("where (bar = 'foo' and foo = 'bar') or foo != 'bar'");
            var parseTree = cmsqlParser.whereClause();

            var visitor = new WhereClauseVisitor();
            var binaryExpression = visitor.VisitWhereClause(parseTree) as CmsqlQueryBinaryExpression;

            binaryExpression.Operator.Should().Be(ConditionalOperator.Or);
            binaryExpression.LeftExpression.Should().BeOfType<CmsqlQueryBinaryExpression>();
            binaryExpression.RightExpression.Should().BeOfType<CmsqlQueryCondition>();
        }  
    }
}
