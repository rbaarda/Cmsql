using Cmsql.Query;

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

        public override ICmsqlQueryExpression VisitAndExpression(CmsqlParser.AndExpressionContext context)
        {
            return new CmsqlQueryBinaryExpression
            {
                Operator = ConditionalOperator.And,
                LeftExpression = context.left.Accept(this),
                RightExpression = context.right.Accept(this)
            };
        }

        public override ICmsqlQueryExpression VisitOrExpression(CmsqlParser.OrExpressionContext context)
        {
            return new CmsqlQueryBinaryExpression
            {
                Operator = ConditionalOperator.Or,
                LeftExpression = context.left.Accept(this),
                RightExpression = context.right.Accept(this)
            };
        }
    }
}
