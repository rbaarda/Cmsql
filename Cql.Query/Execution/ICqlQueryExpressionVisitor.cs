namespace Cql.Query.Execution
{
    public interface ICqlQueryExpressionVisitor
    {
        void VisitQueryCondition(CqlQueryCondition condition);
        void VisitQueryExpression(CqlQueryBinaryExpression binaryExpression);
    }
}
