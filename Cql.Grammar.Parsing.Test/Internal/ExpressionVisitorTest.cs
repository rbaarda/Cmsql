using Cql.Grammar.Parsing.Internal;
using Cql.Query;
using FluentAssertions;
using Xunit;

namespace Cql.Grammar.Parsing.Test.Internal
{
    public class ExpressionVisitorTest
    {
        [Fact]
        public void Test_can_parse_condition_as_expression()
        {
            CqlParser cqlParser = CqlParserFactory.CreateParserForQuery("foo != 'bar'");
            CqlParser.ConditionExpressionContext parseTree =
                (CqlParser.ConditionExpressionContext)cqlParser.expression();

            ExpressionVisitor visitor = new ExpressionVisitor();
            ICqlQueryExpression expression = visitor.VisitConditionExpression(parseTree);

            expression.Should().BeOfType<CqlQueryCondition>();
            expression.Should().NotBeNull();
        }

        [Fact]
        public void Test_can_parse_parenthesized_expression_containing_condition_expression()
        {
            CqlParser cqlParser = CqlParserFactory.CreateParserForQuery("(foo != 'bar')");
            CqlParser.ParenthesizedExpressionContext parseTree =
                (CqlParser.ParenthesizedExpressionContext)cqlParser.expression();

            ExpressionVisitor visitor = new ExpressionVisitor();
            ICqlQueryExpression expression = visitor.VisitParenthesizedExpression(parseTree);

            expression.Should().BeOfType<CqlQueryCondition>();
            expression.Should().NotBeNull();
        }

        [Fact]
        public void Test_can_binary_expression_containing_two_condition_expressions()
        {
            CqlParser cqlParser = CqlParserFactory.CreateParserForQuery("foo != 'bar' and bar = 'foo'");
            CqlParser.BinaryExpressionContext parseTree =
                (CqlParser.BinaryExpressionContext)cqlParser.expression();

            ExpressionVisitor visitor = new ExpressionVisitor();
            CqlQueryExpression expression = (CqlQueryExpression)visitor.VisitBinaryExpression(parseTree);

            expression.LeftExpression.Should().BeOfType<CqlQueryCondition>();
            expression.LeftExpression.Should().NotBeNull();
            expression.RightExpression.Should().BeOfType<CqlQueryCondition>();
            expression.RightExpression.Should().NotBeNull();
        }

        [Theory]
        [InlineData("(foo != 'bar') and bar = 'foo'")]
        [InlineData("foo != 'bar' and (bar = 'foo')")]
        public void Test_can_parse_binary_expression_containing_parenthesized_expression_and_condition_expression(string queryExpression)
        {
            CqlParser cqlParser = CqlParserFactory.CreateParserForQuery(queryExpression);
            CqlParser.BinaryExpressionContext parseTree =
                (CqlParser.BinaryExpressionContext)cqlParser.expression();

            ExpressionVisitor visitor = new ExpressionVisitor();
            CqlQueryExpression expression = (CqlQueryExpression)visitor.VisitBinaryExpression(parseTree);

            expression.LeftExpression.Should().BeOfType<CqlQueryCondition>();
            expression.LeftExpression.Should().NotBeNull();
            expression.RightExpression.Should().BeOfType<CqlQueryCondition>();
            expression.RightExpression.Should().NotBeNull();
        }

        [Fact]
        public void Test_can_parse_parenthesized_expression_for_binary_expression()
        {
            CqlParser cqlParser = CqlParserFactory.CreateParserForQuery("(foo != 'bar' and bar = 'foo')");
            CqlParser.ParenthesizedExpressionContext parseTree =
                (CqlParser.ParenthesizedExpressionContext)cqlParser.expression();

            ExpressionVisitor visitor = new ExpressionVisitor();
            CqlQueryExpression expression = (CqlQueryExpression)visitor.VisitParenthesizedExpression(parseTree);

            expression.LeftExpression.Should().BeOfType<CqlQueryCondition>();
            expression.LeftExpression.Should().NotBeNull();
            expression.RightExpression.Should().BeOfType<CqlQueryCondition>();
            expression.RightExpression.Should().NotBeNull();
        }

        [Fact]
        public void Test_can_parse_binary_expression_for_two_parenthesized_expressions()
        {
            CqlParser cqlParser =
                CqlParserFactory.CreateParserForQuery(
                    "(jon != 'stark' and john = 'snow') or (arya = 'stark' and sansa = 'stark')");
            CqlParser.BinaryExpressionContext parseTree =
                (CqlParser.BinaryExpressionContext)cqlParser.expression();

            ExpressionVisitor visitor = new ExpressionVisitor();
            CqlQueryExpression expression = (CqlQueryExpression)visitor.VisitBinaryExpression(parseTree);

            expression.LeftExpression.Should().BeOfType<CqlQueryExpression>();
            expression.LeftExpression.Should().NotBeNull();
            expression.RightExpression.Should().BeOfType<CqlQueryExpression>();
            expression.RightExpression.Should().NotBeNull();

            CqlQueryExpression leftParenthesizedExpression = (CqlQueryExpression) expression.LeftExpression;
            leftParenthesizedExpression.LeftExpression.Should().BeOfType<CqlQueryCondition>();
            leftParenthesizedExpression.LeftExpression.Should().NotBeNull();
            leftParenthesizedExpression.RightExpression.Should().BeOfType<CqlQueryCondition>();
            leftParenthesizedExpression.RightExpression.Should().NotBeNull();

            CqlQueryExpression rightParenthesizedExpression = (CqlQueryExpression)expression.RightExpression;
            rightParenthesizedExpression.LeftExpression.Should().BeOfType<CqlQueryCondition>();
            rightParenthesizedExpression.LeftExpression.Should().NotBeNull();
            rightParenthesizedExpression.RightExpression.Should().BeOfType<CqlQueryCondition>();
            rightParenthesizedExpression.RightExpression.Should().NotBeNull();
        }

        [Fact]
        public void Test_can_parse_parenthesized_expression_for_binary_expression_for_two_parenthesized_expressions()
        {
            CqlParser cqlParser =
                CqlParserFactory.CreateParserForQuery(
                    "((jon != 'stark' and john = 'snow') or (arya = 'stark' and sansa = 'stark'))");
            CqlParser.ParenthesizedExpressionContext parseTree =
                (CqlParser.ParenthesizedExpressionContext)cqlParser.expression();

            ExpressionVisitor visitor = new ExpressionVisitor();
            CqlQueryExpression expression = (CqlQueryExpression)visitor.VisitParenthesizedExpression(parseTree);

            expression.LeftExpression.Should().BeOfType<CqlQueryExpression>();
            expression.LeftExpression.Should().NotBeNull();
            expression.RightExpression.Should().BeOfType<CqlQueryExpression>();
            expression.RightExpression.Should().NotBeNull();

            CqlQueryExpression leftParenthesizedExpression = (CqlQueryExpression)expression.LeftExpression;
            leftParenthesizedExpression.LeftExpression.Should().BeOfType<CqlQueryCondition>();
            leftParenthesizedExpression.LeftExpression.Should().NotBeNull();
            leftParenthesizedExpression.RightExpression.Should().BeOfType<CqlQueryCondition>();
            leftParenthesizedExpression.RightExpression.Should().NotBeNull();

            CqlQueryExpression rightParenthesizedExpression = (CqlQueryExpression)expression.RightExpression;
            rightParenthesizedExpression.LeftExpression.Should().BeOfType<CqlQueryCondition>();
            rightParenthesizedExpression.LeftExpression.Should().NotBeNull();
            rightParenthesizedExpression.RightExpression.Should().BeOfType<CqlQueryCondition>();
            rightParenthesizedExpression.RightExpression.Should().NotBeNull();
        }
    }
}
