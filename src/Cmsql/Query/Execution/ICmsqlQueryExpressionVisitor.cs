namespace Cmsql.Query.Execution
{
    public interface ICmsqlQueryExpressionVisitor
    {
        void VisitQueryCondition(CmsqlQueryCondition condition);
        void VisitQueryExpression(CmsqlQueryBinaryExpression binaryExpression);
    }
}
