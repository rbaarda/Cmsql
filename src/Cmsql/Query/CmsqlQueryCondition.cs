using Cmsql.Query.Execution;

namespace Cmsql.Query
{
    public class CmsqlQueryCondition : ICmsqlQueryExpression
    {
        public EqualityOperator Operator { get; set; }

        public string Identifier { get; set; }

        public string Value { get; set; }

        public void Accept(ICmsqlQueryExpressionVisitor expressionVisitor)
        {
            expressionVisitor.VisitQueryCondition(this);
        }
    }
}
