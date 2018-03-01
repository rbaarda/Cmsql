using Cql.Query;
using Cql.Query.Execution;
using EPiServer;

namespace Cql.EpiServer.Internal
{
    internal class CqlExpressionVisitor : ICqlQueryExpressionVisitor
    {
        private readonly QueryConditionToPropertyCriteriaMapper _conditionToCriteriaMapper;
        private readonly CqlExpressionVisitorContext _context;
        
        internal CqlExpressionVisitor(
            QueryConditionToPropertyCriteriaMapper conditionToCriteriaMapper,
            CqlExpressionVisitorContext context)
        {
            _conditionToCriteriaMapper = conditionToCriteriaMapper;
            _context = context;
        }

        public void VisitQueryCondition(CqlQueryCondition condition)
        {
            if (_conditionToCriteriaMapper.TryMap(condition, out PropertyCriteria criteria))
            {
                _context.AddPropertyCriteria(criteria);
            }
        }

        public void VisitQueryExpression(CqlQueryBinaryExpression binaryExpression)
        {
            if (binaryExpression.Operator == ConditionalOperator.Or)
            {
                _context.PushNewPropertyCriteriaCollection();
            }

            CqlExpressionVisitor leftCqlExpressionVisitor =
                new CqlExpressionVisitor(_conditionToCriteriaMapper, _context);
            binaryExpression.LeftExpression.Accept(leftCqlExpressionVisitor);

            if (binaryExpression.Operator == ConditionalOperator.Or)
            {
                _context.PushNewPropertyCriteriaCollection();
            }

            CqlExpressionVisitor rightCqlExpressionVisitor =
                new CqlExpressionVisitor(_conditionToCriteriaMapper, _context);
            binaryExpression.RightExpression.Accept(rightCqlExpressionVisitor);
        }
    }
}
