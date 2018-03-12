using Cmsql.Query;

namespace Cmsql.Grammar.Parsing.Internal
{
    internal class ExpressionVisitor : CmsqlBaseVisitor<ICqlQueryExpression>
    {
        public override ICqlQueryExpression VisitConditionExpression(CmsqlParser.ConditionExpressionContext context)
        {
            ConditionVisitor conditionVisitor = new ConditionVisitor();
            return context.condition().Accept(conditionVisitor);
        }

        public override ICqlQueryExpression VisitParenthesizedExpression(CmsqlParser.ParenthesizedExpressionContext context)
        {
            ExpressionVisitor expressionVisitor = new ExpressionVisitor();
            return context.expression().Accept(expressionVisitor);
        }

        public override ICqlQueryExpression VisitBinaryExpression(CmsqlParser.BinaryExpressionContext context)
        {
            ExpressionVisitor expressionVisitor = new ExpressionVisitor();
            return new CqlQueryBinaryExpression
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
                case CmsqlParser.AND:
                    return ConditionalOperator.And;
                case CmsqlParser.OR:
                    return ConditionalOperator.Or;
            }

            return ConditionalOperator.None;
        }
    }
}
