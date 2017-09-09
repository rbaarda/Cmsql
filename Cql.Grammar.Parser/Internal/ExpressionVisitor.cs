using Cql.Query;

namespace Cql.Grammar.Parser.Internal
{
    internal class ExpressionVisitor : CqlBaseVisitor<ICqlQueryExpression>
    {
        public override ICqlQueryExpression VisitConditionExpression(CqlParser.ConditionExpressionContext context)
        {
            ConditionVisitor conditionVisitor = new ConditionVisitor();
            return context.condition().Accept(conditionVisitor);
        }

        public override ICqlQueryExpression VisitParenthesizedExpression(CqlParser.ParenthesizedExpressionContext context)
        {
            ExpressionVisitor expressionVisitor = new ExpressionVisitor();
            return context.expression().Accept(expressionVisitor);
        }

        public override ICqlQueryExpression VisitBinaryExpression(CqlParser.BinaryExpressionContext context)
        {
            ExpressionVisitor expressionVisitor = new ExpressionVisitor();
            return new CqlQueryExpression
            {
                Operator = GetConditionalOperator(context.op.Type),
                LeftExpression = context.left.Accept(expressionVisitor),
                RightExpression = context.right.Accept(expressionVisitor)
            };
        }

        private ConditionalOperator GetConditionalOperator(int token)
        {
            switch (token)
            {
                case CqlParser.AND:
                    return ConditionalOperator.And;
                case CqlParser.OR:
                    return ConditionalOperator.Or;
            }

            return ConditionalOperator.None;
        }
    }
}
