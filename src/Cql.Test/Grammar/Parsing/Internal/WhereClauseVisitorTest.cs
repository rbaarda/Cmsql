using Cmsql.Grammar;
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
            CmsqlParser cmsqlParser = CmsqlParserFactory.CreateParserForQuery("where foo != 'bar'");
            CmsqlParser.WhereClauseContext parseTree = cmsqlParser.whereClause();

            WhereClauseVisitor visitor = new WhereClauseVisitor();
            ICqlQueryExpression expression = visitor.VisitWhereClause(parseTree);

            expression.Should().BeOfType<CqlQueryCondition>();
        }

        [Fact]
        public void Test_can_parse_two_conditions_as_expression()
        {
            CmsqlParser cmsqlParser = CmsqlParserFactory.CreateParserForQuery("where foo != 'bar' and bar = 'foo'");
            CmsqlParser.WhereClauseContext parseTree = cmsqlParser.whereClause();

            WhereClauseVisitor visitor = new WhereClauseVisitor();
            CqlQueryBinaryExpression binaryExpression = visitor.VisitWhereClause(parseTree) as CqlQueryBinaryExpression;
            
            binaryExpression.Operator.ShouldBeEquivalentTo(ConditionalOperator.And);
            binaryExpression.LeftExpression.Should().BeOfType<CqlQueryCondition>();
            binaryExpression.RightExpression.Should().BeOfType<CqlQueryCondition>();
        }

        [Fact]
        public void Test_can_parse_grouped_expression()
        {
            CmsqlParser cmsqlParser = CmsqlParserFactory.CreateParserForQuery("where (foo != 'bar' and bar = 'foo')");
            CmsqlParser.WhereClauseContext parseTree = cmsqlParser.whereClause();

            WhereClauseVisitor visitor = new WhereClauseVisitor();
            CqlQueryBinaryExpression binaryExpression = visitor.VisitWhereClause(parseTree) as CqlQueryBinaryExpression;

            binaryExpression.Operator.ShouldBeEquivalentTo(ConditionalOperator.And);
            binaryExpression.LeftExpression.Should().BeOfType<CqlQueryCondition>();
            binaryExpression.RightExpression.Should().BeOfType<CqlQueryCondition>();
        }

        [Fact]
        public void Test_can_parse_condition_and_grouped_expression()
        {
            CmsqlParser cmsqlParser = CmsqlParserFactory.CreateParserForQuery("where foo != 'bar' or (bar = 'foo' and foo = 'bar')");
            CmsqlParser.WhereClauseContext parseTree = cmsqlParser.whereClause();

            WhereClauseVisitor visitor = new WhereClauseVisitor();
            CqlQueryBinaryExpression binaryExpression = visitor.VisitWhereClause(parseTree) as CqlQueryBinaryExpression;

            binaryExpression.Operator.ShouldBeEquivalentTo(ConditionalOperator.Or);
            binaryExpression.LeftExpression.Should().BeOfType<CqlQueryCondition>();
            binaryExpression.RightExpression.Should().BeOfType<CqlQueryBinaryExpression>();
        }

        [Fact]
        public void Test_can_parse_grouped_expression_and_condition()
        {
            CmsqlParser cmsqlParser = CmsqlParserFactory.CreateParserForQuery("where (bar = 'foo' and foo = 'bar') or foo != 'bar'");
            CmsqlParser.WhereClauseContext parseTree = cmsqlParser.whereClause();

            WhereClauseVisitor visitor = new WhereClauseVisitor();
            CqlQueryBinaryExpression binaryExpression = visitor.VisitWhereClause(parseTree) as CqlQueryBinaryExpression;

            binaryExpression.Operator.ShouldBeEquivalentTo(ConditionalOperator.Or);
            binaryExpression.LeftExpression.Should().BeOfType<CqlQueryBinaryExpression>();
            binaryExpression.RightExpression.Should().BeOfType<CqlQueryCondition>();
        }
    }
}
