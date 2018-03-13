using Cmsql.Query.Execution;

namespace Cmsql.Query
{
    public interface ICmsqlQueryExpression
    {
        void Accept(ICmsqlQueryExpressionVisitor expressionVisitor);
    }
}
