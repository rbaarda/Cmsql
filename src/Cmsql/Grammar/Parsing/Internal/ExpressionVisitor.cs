using Cmsql.Query;
using System;

namespace Cmsql.Grammar.Parsing.Internal
{
    internal class ExpressionVisitor : CmsqlBaseVisitor<ICmsqlQueryExpression>
    {
        private static readonly ConditionVisitor _conditionVisitor = new ConditionVisitor();

        public override ICmsqlQueryExpression VisitConditionExpression(CmsqlParser.ConditionExpressionContext context)
        {
            return context.condition().Accept(_conditionVisitor);
        }

        public override ICmsqlQueryExpression VisitParenthesizedExpression(CmsqlParser.ParenthesizedExpressionContext context)
        {
            return context.expression().Accept(this);
        }

        public override ICmsqlQueryExpression VisitBinaryExpression(CmsqlParser.BinaryExpressionContext context)
        {
            return new CmsqlQueryBinaryExpression
            {
                Operator = GetConditionalOperator(context.op.Type),
                LeftExpression = context.left.Accept(this),
                RightExpression = context.right.Accept(this)
            };
        }

        private static ConditionalOperator GetConditionalOperator(int token)
        {
            switch (token)
            {
                case CmsqlParser.AND:
                    return ConditionalOperator.And;
                case CmsqlParser.OR:
                    return ConditionalOperator.Or;
                default:
                    throw new InvalidOperationException($"Unrecognised conditional operator token: {token}");
            }
        }
    }
}
