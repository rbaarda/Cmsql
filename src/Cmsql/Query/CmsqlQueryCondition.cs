using Cmsql.Query.Execution;

namespace Cmsql.Query
{
    public record CmsqlQueryCondition : ICmsqlQueryExpression
    {
        public EqualityOperator Operator { get; init; }

        public string Identifier { get; init; }

        public string Value { get; init; }

        public void Accept(ICmsqlQueryExpressionVisitor expressionVisitor)
        {
            expressionVisitor.VisitQueryCondition(this);
        }
    }
}
