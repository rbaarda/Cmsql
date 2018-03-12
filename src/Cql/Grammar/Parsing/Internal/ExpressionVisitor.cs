using Cmsql.Query;

namespace Cmsql.Grammar.Parsing.Internal
{
    internal class ExpressionVisitor : CmsqlBaseVisitor<ICmsqlQueryExpression>
    {
        public override ICmsqlQueryExpression VisitConditionExpression(CmsqlParser.ConditionExpressionContext context)
        {
            ConditionVisitor conditionVisitor = new ConditionVisitor();
            return context.condition().Accept(conditionVisitor);
        }

        public override ICmsqlQueryExpression VisitParenthesizedExpression(CmsqlParser.ParenthesizedExpressionContext context)
        {
            ExpressionVisitor expressionVisitor = new ExpressionVisitor();
            return context.expression().Accept(expressionVisitor);
        }

        public override ICmsqlQueryExpression VisitBinaryExpression(CmsqlParser.BinaryExpressionContext context)
        {
            ExpressionVisitor expressionVisitor = new ExpressionVisitor();
            return new CmsqlQueryBinaryExpression
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
