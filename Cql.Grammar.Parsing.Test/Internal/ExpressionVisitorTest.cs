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
            CqlQueryBinaryExpression binaryExpression = (CqlQueryBinaryExpression)visitor.VisitBinaryExpression(parseTree);

            binaryExpression.LeftExpression.Should().BeOfType<CqlQueryCondition>();
            binaryExpression.LeftExpression.Should().NotBeNull();
            binaryExpression.RightExpression.Should().BeOfType<CqlQueryCondition>();
            binaryExpression.RightExpression.Should().NotBeNull();
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
            CqlQueryBinaryExpression binaryExpression = (CqlQueryBinaryExpression)visitor.VisitBinaryExpression(parseTree);

            binaryExpression.LeftExpression.Should().BeOfType<CqlQueryCondition>();
            binaryExpression.LeftExpression.Should().NotBeNull();
            binaryExpression.RightExpression.Should().BeOfType<CqlQueryCondition>();
            binaryExpression.RightExpression.Should().NotBeNull();
        }

        [Fact]
        public void Test_can_parse_parenthesized_expression_for_binary_expression()
        {
            CqlParser cqlParser = CqlParserFactory.CreateParserForQuery("(foo != 'bar' and bar = 'foo')");
            CqlParser.ParenthesizedExpressionContext parseTree =
                (CqlParser.ParenthesizedExpressionContext)cqlParser.expression();

            ExpressionVisitor visitor = new ExpressionVisitor();
            CqlQueryBinaryExpression binaryExpression = (CqlQueryBinaryExpression)visitor.VisitParenthesizedExpression(parseTree);

            binaryExpression.LeftExpression.Should().BeOfType<CqlQueryCondition>();
            binaryExpression.LeftExpression.Should().NotBeNull();
            binaryExpression.RightExpression.Should().BeOfType<CqlQueryCondition>();
            binaryExpression.RightExpression.Should().NotBeNull();
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
            CqlQueryBinaryExpression binaryExpression = (CqlQueryBinaryExpression)visitor.VisitBinaryExpression(parseTree);

            binaryExpression.LeftExpression.Should().BeOfType<CqlQueryBinaryExpression>();
            binaryExpression.LeftExpression.Should().NotBeNull();
            binaryExpression.RightExpression.Should().BeOfType<CqlQueryBinaryExpression>();
            binaryExpression.RightExpression.Should().NotBeNull();

            CqlQueryBinaryExpression leftParenthesizedBinaryExpression = (CqlQueryBinaryExpression) binaryExpression.LeftExpression;
            leftParenthesizedBinaryExpression.LeftExpression.Should().BeOfType<CqlQueryCondition>();
            leftParenthesizedBinaryExpression.LeftExpression.Should().NotBeNull();
            leftParenthesizedBinaryExpression.RightExpression.Should().BeOfType<CqlQueryCondition>();
            leftParenthesizedBinaryExpression.RightExpression.Should().NotBeNull();

            CqlQueryBinaryExpression rightParenthesizedBinaryExpression = (CqlQueryBinaryExpression)binaryExpression.RightExpression;
            rightParenthesizedBinaryExpression.LeftExpression.Should().BeOfType<CqlQueryCondition>();
            rightParenthesizedBinaryExpression.LeftExpression.Should().NotBeNull();
            rightParenthesizedBinaryExpression.RightExpression.Should().BeOfType<CqlQueryCondition>();
            rightParenthesizedBinaryExpression.RightExpression.Should().NotBeNull();
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
            CqlQueryBinaryExpression binaryExpression = (CqlQueryBinaryExpression)visitor.VisitParenthesizedExpression(parseTree);

            binaryExpression.LeftExpression.Should().BeOfType<CqlQueryBinaryExpression>();
            binaryExpression.LeftExpression.Should().NotBeNull();
            binaryExpression.RightExpression.Should().BeOfType<CqlQueryBinaryExpression>();
            binaryExpression.RightExpression.Should().NotBeNull();

            CqlQueryBinaryExpression leftParenthesizedBinaryExpression = (CqlQueryBinaryExpression)binaryExpression.LeftExpression;
            leftParenthesizedBinaryExpression.LeftExpression.Should().BeOfType<CqlQueryCondition>();
            leftParenthesizedBinaryExpression.LeftExpression.Should().NotBeNull();
            leftParenthesizedBinaryExpression.RightExpression.Should().BeOfType<CqlQueryCondition>();
            leftParenthesizedBinaryExpression.RightExpression.Should().NotBeNull();

            CqlQueryBinaryExpression rightParenthesizedBinaryExpression = (CqlQueryBinaryExpression)binaryExpression.RightExpression;
            rightParenthesizedBinaryExpression.LeftExpression.Should().BeOfType<CqlQueryCondition>();
            rightParenthesizedBinaryExpression.LeftExpression.Should().NotBeNull();
            rightParenthesizedBinaryExpression.RightExpression.Should().BeOfType<CqlQueryCondition>();
            rightParenthesizedBinaryExpression.RightExpression.Should().NotBeNull();
        }
    }
}
