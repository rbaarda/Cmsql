using Antlr4.Runtime;
using Cql.Grammar.Parser.Internal;
using Cql.Query;
using FluentAssertions;
using Xunit;

namespace Cql.Grammar.Parser.Test.Internal
{
    public class WhereClauseVisitorTest
    {
        [Fact]
        public void Test_can_parse_single_condition_as_expression()
        {
            CqlParser cqlParser = CreateParserForQuery("where foo != 'bar'");
            CqlParser.WhereClauseContext parseTree = cqlParser.whereClause();

            WhereClauseVisitor visitor = new WhereClauseVisitor();
            ICqlQueryExpression expression = visitor.VisitWhereClause(parseTree);

            expression.Should().BeOfType<CqlQueryCondition>();
        }

        [Fact]
        public void Test_can_parse_two_conditions_as_expression()
        {
            CqlParser cqlParser = CreateParserForQuery("where foo != 'bar' and bar = 'foo'");
            CqlParser.WhereClauseContext parseTree = cqlParser.whereClause();

            WhereClauseVisitor visitor = new WhereClauseVisitor();
            CqlQueryExpression expression = visitor.VisitWhereClause(parseTree) as CqlQueryExpression;
            
            expression.Operator.ShouldBeEquivalentTo(ConditionalOperator.And);
            expression.LeftExpression.Should().BeOfType<CqlQueryCondition>();
            expression.RightExpression.Should().BeOfType<CqlQueryCondition>();
        }

        [Fact]
        public void Test_can_parse_grouped_expression()
        {
            CqlParser cqlParser = CreateParserForQuery("where (foo != 'bar' and bar = 'foo')");
            CqlParser.WhereClauseContext parseTree = cqlParser.whereClause();

            WhereClauseVisitor visitor = new WhereClauseVisitor();
            CqlQueryExpression expression = visitor.VisitWhereClause(parseTree) as CqlQueryExpression;

            expression.Operator.ShouldBeEquivalentTo(ConditionalOperator.And);
            expression.LeftExpression.Should().BeOfType<CqlQueryCondition>();
            expression.RightExpression.Should().BeOfType<CqlQueryCondition>();
        }

        [Fact]
        public void Test_can_parse_condition_and_grouped_expression()
        {
            CqlParser cqlParser = CreateParserForQuery("where foo != 'bar' or (bar = 'foo' and foo = 'bar')");
            CqlParser.WhereClauseContext parseTree = cqlParser.whereClause();

            WhereClauseVisitor visitor = new WhereClauseVisitor();
            CqlQueryExpression expression = visitor.VisitWhereClause(parseTree) as CqlQueryExpression;

            expression.Operator.ShouldBeEquivalentTo(ConditionalOperator.Or);
            expression.LeftExpression.Should().BeOfType<CqlQueryCondition>();
            expression.RightExpression.Should().BeOfType<CqlQueryExpression>();
        }

        [Fact]
        public void Test_can_parse_grouped_expression_and_condition()
        {
            CqlParser cqlParser = CreateParserForQuery("where (bar = 'foo' and foo = 'bar') or foo != 'bar'");
            CqlParser.WhereClauseContext parseTree = cqlParser.whereClause();

            WhereClauseVisitor visitor = new WhereClauseVisitor();
            CqlQueryExpression expression = visitor.VisitWhereClause(parseTree) as CqlQueryExpression;

            expression.Operator.ShouldBeEquivalentTo(ConditionalOperator.Or);
            expression.LeftExpression.Should().BeOfType<CqlQueryExpression>();
            expression.RightExpression.Should().BeOfType<CqlQueryCondition>();
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
