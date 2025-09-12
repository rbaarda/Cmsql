using Cmsql.Grammar;
using Cmsql.Grammar.Parsing.Internal;
using Cmsql.Query;
using FluentAssertions;
using Xunit;

namespace Cmsql.Test.Grammar.Parsing.Internal
{
    public class ExpressionVisitorTest
    {
        [Fact]
        public void Test_can_parse_condition_as_expression()
        {
            var cmsqlParser = CmsqlParserFactory.CreateParserForQuery("foo != 'bar'");
            var parseTree = (CmsqlParser.ConditionExpressionContext)cmsqlParser.expression();

            var visitor = new ExpressionVisitor();
            var expression = visitor.VisitConditionExpression(parseTree);

            expression.Should().BeOfType<CmsqlQueryCondition>();
            expression.Should().NotBeNull();
        }

        [Fact]
        public void Test_can_parse_parenthesized_expression_containing_condition_expression()
        {
            var cmsqlParser = CmsqlParserFactory.CreateParserForQuery("(foo != 'bar')");
            var parseTree = (CmsqlParser.ParenthesizedExpressionContext)cmsqlParser.expression();

            var visitor = new ExpressionVisitor();
            var expression = visitor.VisitParenthesizedExpression(parseTree);

            expression.Should().BeOfType<CmsqlQueryCondition>();
            expression.Should().NotBeNull();
        }

        [Fact]
        public void Test_can_binary_expression_containing_two_condition_expressions()
        {
            var cmsqlParser = CmsqlParserFactory.CreateParserForQuery("foo != 'bar' and bar = 'foo'");
            var parseTree = (CmsqlParser.BinaryExpressionContext)cmsqlParser.expression();

            var visitor = new ExpressionVisitor();
            var binaryExpression = (CmsqlQueryBinaryExpression)visitor.VisitBinaryExpression(parseTree);

            binaryExpression.LeftExpression.Should().BeOfType<CmsqlQueryCondition>();
            binaryExpression.LeftExpression.Should().NotBeNull();
            binaryExpression.RightExpression.Should().BeOfType<CmsqlQueryCondition>();
            binaryExpression.RightExpression.Should().NotBeNull();
        }

        [Theory]
        [InlineData("(foo != 'bar') and bar = 'foo'")]
        [InlineData("foo != 'bar' and (bar = 'foo')")]
        public void Test_can_parse_binary_expression_containing_parenthesized_expression_and_condition_expression(string queryExpression)
        {
            var cmsqlParser = CmsqlParserFactory.CreateParserForQuery(queryExpression);
            var parseTree = (CmsqlParser.BinaryExpressionContext)cmsqlParser.expression();

            var visitor = new ExpressionVisitor();
            var binaryExpression = (CmsqlQueryBinaryExpression)visitor.VisitBinaryExpression(parseTree);

            binaryExpression.LeftExpression.Should().BeOfType<CmsqlQueryCondition>();
            binaryExpression.LeftExpression.Should().NotBeNull();
            binaryExpression.RightExpression.Should().BeOfType<CmsqlQueryCondition>();
            binaryExpression.RightExpression.Should().NotBeNull();
        }

        [Fact]
        public void Test_can_parse_parenthesized_expression_for_binary_expression()
        {
            var cmsqlParser = CmsqlParserFactory.CreateParserForQuery("(foo != 'bar' and bar = 'foo')");
            var parseTree = (CmsqlParser.ParenthesizedExpressionContext)cmsqlParser.expression();

            var visitor = new ExpressionVisitor();
            var binaryExpression = (CmsqlQueryBinaryExpression)visitor.VisitParenthesizedExpression(parseTree);

            binaryExpression.LeftExpression.Should().BeOfType<CmsqlQueryCondition>();
            binaryExpression.LeftExpression.Should().NotBeNull();
            binaryExpression.RightExpression.Should().BeOfType<CmsqlQueryCondition>();
            binaryExpression.RightExpression.Should().NotBeNull();
        }

        [Fact]
        public void Test_can_parse_binary_expression_for_two_parenthesized_expressions()
        {
            var cmsqlParser = CmsqlParserFactory.CreateParserForQuery(
                "(jon != 'stark' and john = 'snow') or (arya = 'stark' and sansa = 'stark')");
            var parseTree = (CmsqlParser.BinaryExpressionContext)cmsqlParser.expression();

            var visitor = new ExpressionVisitor();
            var binaryExpression = (CmsqlQueryBinaryExpression)visitor.VisitBinaryExpression(parseTree);

            binaryExpression.LeftExpression.Should().BeOfType<CmsqlQueryBinaryExpression>();
            binaryExpression.LeftExpression.Should().NotBeNull();
            binaryExpression.RightExpression.Should().BeOfType<CmsqlQueryBinaryExpression>();
            binaryExpression.RightExpression.Should().NotBeNull();

            var leftParenthesizedBinaryExpression = (CmsqlQueryBinaryExpression) binaryExpression.LeftExpression;
            leftParenthesizedBinaryExpression.LeftExpression.Should().BeOfType<CmsqlQueryCondition>();
            leftParenthesizedBinaryExpression.LeftExpression.Should().NotBeNull();
            leftParenthesizedBinaryExpression.RightExpression.Should().BeOfType<CmsqlQueryCondition>();
            leftParenthesizedBinaryExpression.RightExpression.Should().NotBeNull();

            var rightParenthesizedBinaryExpression = (CmsqlQueryBinaryExpression)binaryExpression.RightExpression;
            rightParenthesizedBinaryExpression.LeftExpression.Should().BeOfType<CmsqlQueryCondition>();
            rightParenthesizedBinaryExpression.LeftExpression.Should().NotBeNull();
            rightParenthesizedBinaryExpression.RightExpression.Should().BeOfType<CmsqlQueryCondition>();
            rightParenthesizedBinaryExpression.RightExpression.Should().NotBeNull();
        }

        [Fact]
        public void Test_can_parse_parenthesized_expression_for_binary_expression_for_two_parenthesized_expressions()
        {
            var cmsqlParser = CmsqlParserFactory.CreateParserForQuery(
                "((jon != 'stark' and john = 'snow') or (arya = 'stark' and sansa = 'stark'))");
            var parseTree = (CmsqlParser.ParenthesizedExpressionContext)cmsqlParser.expression();

            var visitor = new ExpressionVisitor();
            var binaryExpression = (CmsqlQueryBinaryExpression)visitor.VisitParenthesizedExpression(parseTree);

            binaryExpression.LeftExpression.Should().BeOfType<CmsqlQueryBinaryExpression>();
            binaryExpression.LeftExpression.Should().NotBeNull();
            binaryExpression.RightExpression.Should().BeOfType<CmsqlQueryBinaryExpression>();
            binaryExpression.RightExpression.Should().NotBeNull();

            var leftParenthesizedBinaryExpression = (CmsqlQueryBinaryExpression)binaryExpression.LeftExpression;
            leftParenthesizedBinaryExpression.LeftExpression.Should().BeOfType<CmsqlQueryCondition>();
            leftParenthesizedBinaryExpression.LeftExpression.Should().NotBeNull();
            leftParenthesizedBinaryExpression.RightExpression.Should().BeOfType<CmsqlQueryCondition>();
            leftParenthesizedBinaryExpression.RightExpression.Should().NotBeNull();

            var rightParenthesizedBinaryExpression = (CmsqlQueryBinaryExpression)binaryExpression.RightExpression;
            rightParenthesizedBinaryExpression.LeftExpression.Should().BeOfType<CmsqlQueryCondition>();
            rightParenthesizedBinaryExpression.LeftExpression.Should().NotBeNull();
            rightParenthesizedBinaryExpression.RightExpression.Should().BeOfType<CmsqlQueryCondition>();
            rightParenthesizedBinaryExpression.RightExpression.Should().NotBeNull();
        }
    }
}
