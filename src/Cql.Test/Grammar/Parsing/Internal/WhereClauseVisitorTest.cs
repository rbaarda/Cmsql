using Cql.Grammar;
using Cql.Grammar.Parsing.Internal;
using Cql.Query;
using FluentAssertions;
using Xunit;

namespace Cql.Test.Grammar.Parsing.Internal
{
    public class WhereClauseVisitorTest
    {
        [Fact]
        public void Test_can_parse_single_condition_as_expression()
        {
            CqlParser cqlParser = CqlParserFactory.CreateParserForQuery("where foo != 'bar'");
            CqlParser.WhereClauseContext parseTree = cqlParser.whereClause();

            WhereClauseVisitor visitor = new WhereClauseVisitor();
            ICqlQueryExpression expression = visitor.VisitWhereClause(parseTree);

            expression.Should().BeOfType<CqlQueryCondition>();
        }

        [Fact]
        public void Test_can_parse_two_conditions_as_expression()
        {
            CqlParser cqlParser = CqlParserFactory.CreateParserForQuery("where foo != 'bar' and bar = 'foo'");
            CqlParser.WhereClauseContext parseTree = cqlParser.whereClause();

            WhereClauseVisitor visitor = new WhereClauseVisitor();
            CqlQueryBinaryExpression binaryExpression = visitor.VisitWhereClause(parseTree) as CqlQueryBinaryExpression;
            
            binaryExpression.Operator.ShouldBeEquivalentTo(ConditionalOperator.And);
            binaryExpression.LeftExpression.Should().BeOfType<CqlQueryCondition>();
            binaryExpression.RightExpression.Should().BeOfType<CqlQueryCondition>();
        }

        [Fact]
        public void Test_can_parse_grouped_expression()
        {
            CqlParser cqlParser = CqlParserFactory.CreateParserForQuery("where (foo != 'bar' and bar = 'foo')");
            CqlParser.WhereClauseContext parseTree = cqlParser.whereClause();

            WhereClauseVisitor visitor = new WhereClauseVisitor();
            CqlQueryBinaryExpression binaryExpression = visitor.VisitWhereClause(parseTree) as CqlQueryBinaryExpression;

            binaryExpression.Operator.ShouldBeEquivalentTo(ConditionalOperator.And);
            binaryExpression.LeftExpression.Should().BeOfType<CqlQueryCondition>();
            binaryExpression.RightExpression.Should().BeOfType<CqlQueryCondition>();
        }

        [Fact]
        public void Test_can_parse_condition_and_grouped_expression()
        {
            CqlParser cqlParser = CqlParserFactory.CreateParserForQuery("where foo != 'bar' or (bar = 'foo' and foo = 'bar')");
            CqlParser.WhereClauseContext parseTree = cqlParser.whereClause();

            WhereClauseVisitor visitor = new WhereClauseVisitor();
            CqlQueryBinaryExpression binaryExpression = visitor.VisitWhereClause(parseTree) as CqlQueryBinaryExpression;

            binaryExpression.Operator.ShouldBeEquivalentTo(ConditionalOperator.Or);
            binaryExpression.LeftExpression.Should().BeOfType<CqlQueryCondition>();
            binaryExpression.RightExpression.Should().BeOfType<CqlQueryBinaryExpression>();
        }

        [Fact]
        public void Test_can_parse_grouped_expression_and_condition()
        {
            CqlParser cqlParser = CqlParserFactory.CreateParserForQuery("where (bar = 'foo' and foo = 'bar') or foo != 'bar'");
            CqlParser.WhereClauseContext parseTree = cqlParser.whereClause();

            WhereClauseVisitor visitor = new WhereClauseVisitor();
            CqlQueryBinaryExpression binaryExpression = visitor.VisitWhereClause(parseTree) as CqlQueryBinaryExpression;

            binaryExpression.Operator.ShouldBeEquivalentTo(ConditionalOperator.Or);
            binaryExpression.LeftExpression.Should().BeOfType<CqlQueryBinaryExpression>();
            binaryExpression.RightExpression.Should().BeOfType<CqlQueryCondition>();
        }
    }
}
