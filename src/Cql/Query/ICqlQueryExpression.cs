using Cmsql.Query.Execution;

namespace Cmsql.Query
{
    public interface ICqlQueryExpression
    {
        void Accept(ICqlQueryExpressionVisitor expressionVisitor);
    }
}
