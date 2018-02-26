using System.Collections.Generic;
using System.Linq;
using Cql.Query;
using Cql.Query.Execution;
using EPiServer;

namespace Cql.EpiServer.Internal
{
    internal class ExpressionVisitor : ICqlQueryExpressionVisitor
    {
        private readonly QueryConditionToPropertyCriteriaMapper _conditionToCriteriaMapper;

        internal Stack<PropertyCriteriaCollection> PropertyCriteriaCollectionStack { get; }

        internal ExpressionVisitor(
            QueryConditionToPropertyCriteriaMapper conditionToCriteriaMapper,
            Stack<PropertyCriteriaCollection> propertyCriteriaCollectionStack)
        {
            _conditionToCriteriaMapper = conditionToCriteriaMapper;
            PropertyCriteriaCollectionStack = propertyCriteriaCollectionStack;
        }

        public void VisitQueryCondition(CqlQueryCondition condition)
        {
            if (_conditionToCriteriaMapper.TryMap(condition, out PropertyCriteria criteria))
            {
                if (!PropertyCriteriaCollectionStack.Any())
                {
                    PropertyCriteriaCollectionStack.Push(new PropertyCriteriaCollection());
                }

                PropertyCriteriaCollectionStack.Peek().Add(criteria);
            }
        }

        public void VisitQueryExpression(CqlQueryBinaryExpression binaryExpression)
        {
            if (binaryExpression.Operator == ConditionalOperator.Or)
            {
                PropertyCriteriaCollectionStack.Push(new PropertyCriteriaCollection());
            }

            ExpressionVisitor leftExpressionVisitor = new ExpressionVisitor(
                _conditionToCriteriaMapper,
                PropertyCriteriaCollectionStack);
            binaryExpression.LeftExpression.Accept(leftExpressionVisitor);

            if (binaryExpression.Operator == ConditionalOperator.Or)
            {
                PropertyCriteriaCollectionStack.Push(new PropertyCriteriaCollection());
            }

            ExpressionVisitor rightExpressionVisitor = new ExpressionVisitor(
                _conditionToCriteriaMapper,
                PropertyCriteriaCollectionStack);
            binaryExpression.RightExpression.Accept(rightExpressionVisitor);
        }
    }
}
