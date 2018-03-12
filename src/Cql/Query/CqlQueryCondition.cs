using Cmsql.Query.Execution;

namespace Cmsql.Query
{
    public class CqlQueryCondition : ICqlQueryExpression
    {
        public EqualityOperator Operator { get; set; }

        public string Identifier { get; set; }

        public string Value { get; set; }

        public void Accept(ICqlQueryExpressionVisitor expressionVisitor)
        {
            expressionVisitor.VisitQueryCondition(this);
        }
    }
}
