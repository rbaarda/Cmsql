using Cmsql.Query.Execution;

namespace Cmsql.Query
{
    public record CmsqlQueryBinaryExpression : ICmsqlQueryExpression
    {
        public ConditionalOperator Operator { get; init; }

        public ICmsqlQueryExpression LeftExpression { get; init; }

        public ICmsqlQueryExpression RightExpression { get; init; }

        public void Accept(ICmsqlQueryExpressionVisitor expressionVisitor)
        {
            expressionVisitor.VisitQueryExpression(this);
        }
    }
}
