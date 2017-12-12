using Cql.Query.Execution;

namespace Cql.Query
{
    public interface ICqlQueryExpression
    {
        void Accept(ICqlQueryExpressionVisitor expressionVisitor);
    }
}
