using System;
using System.Collections.Generic;
using Cql.Query;
using Cql.Query.Execution;
using EPiServer;
using EPiServer.Core;
using EPiServer.Filters;

namespace Cql.EpiServer.Internal
{
    internal class ExpressionVisitor : ICqlQueryExpressionVisitor
    {
        private readonly PropertyDataTypeResolver _propertyDataTypeResolver;

        public Stack<PropertyCriteriaCollection> PropertyCriteriaCollectionStack { get; }

        public ExpressionVisitor(
            PropertyDataTypeResolver propertyDataTypeResolver,
            Stack<PropertyCriteriaCollection> propertyCriteriaCollectionStack)
        {
            _propertyDataTypeResolver = propertyDataTypeResolver;
            PropertyCriteriaCollectionStack = propertyCriteriaCollectionStack;
        }

        public void VisitQueryCondition(CqlQueryCondition condition)
        {
            if (_propertyDataTypeResolver.TryResolve(
                condition.Identifier,
                out PropertyDataType propertyDataType))
            {
                CompareCondition compareCondition = MapEqualityOperatorToCompareCondition(condition.Operator);
                PropertyCriteriaCollectionStack.Peek().Add(new PropertyCriteria
                {
                    Condition = compareCondition,
                    Value = condition.Value,
                    Name = condition.Identifier,
                    Type = propertyDataType,
                    Required = true
                });
            }
        }

        public void VisitQueryExpression(CqlQueryBinaryExpression binaryExpression)
        {
            if (binaryExpression.Operator == ConditionalOperator.Or)
            {
                PropertyCriteriaCollectionStack.Push(new PropertyCriteriaCollection());
            }

            ExpressionVisitor leftExpressionVisitor = new ExpressionVisitor(
                _propertyDataTypeResolver,
                PropertyCriteriaCollectionStack);
            binaryExpression.LeftExpression.Accept(leftExpressionVisitor);

            if (binaryExpression.Operator == ConditionalOperator.Or)
            {
                PropertyCriteriaCollectionStack.Push(new PropertyCriteriaCollection());
            }

            ExpressionVisitor rightExpressionVisitor = new ExpressionVisitor(
                _propertyDataTypeResolver,
                PropertyCriteriaCollectionStack);
            binaryExpression.RightExpression.Accept(rightExpressionVisitor);
        }

        private CompareCondition MapEqualityOperatorToCompareCondition(EqualityOperator operatr)
        {
            switch (operatr)
            {
                case EqualityOperator.Equals:
                    return CompareCondition.Equal;
                case EqualityOperator.GreaterThan:
                    return CompareCondition.GreaterThan;
                case EqualityOperator.LessThan:
                    return CompareCondition.LessThan;
                case EqualityOperator.NotEquals:
                    return CompareCondition.NotEqual;
            }

            throw new InvalidOperationException($"Equality operator '{operatr}' not supported.");
        }
    }
}
