using Cmsql.Query.Execution;

namespace Cmsql.Query
{
    public class CmsqlQueryBinaryExpression : ICmsqlQueryExpression
    {
        public ConditionalOperator Operator { get; set; }

        public ICmsqlQueryExpression LeftExpression { get; set; }

        public ICmsqlQueryExpression RightExpression { get; set; }

        public void Accept(ICmsqlQueryExpressionVisitor expressionVisitor)
        {
            expressionVisitor.VisitQueryExpression(this);
        }
    }
}
