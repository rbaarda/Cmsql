using System;
using System.Collections.Generic;
using System.Linq;
using Cql.Query;
using Cql.Query.Execution;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Filters;

namespace Cql.EpiServer.Internal
{
    internal class ExpressionVisitor : ICqlQueryExpressionVisitor
    {
        private readonly ContentType _contentType;

        public Stack<PropertyCriteriaCollection> PropertyCriteriaCollectionStack { get; }

        public ExpressionVisitor(
            ContentType contentType,
            Stack<PropertyCriteriaCollection> propertyCriteriaCollectionStack)
        {
            _contentType = contentType;
            PropertyCriteriaCollectionStack = propertyCriteriaCollectionStack;
        }

        public void VisitQueryCondition(CqlQueryCondition condition)
        {
            PropertyDataType propertyType = PropertyDataType.String;
            if (MetaDataPropertyTypeMapping.Mappings.ContainsKey(condition.Identifier))
            {
                propertyType = MetaDataPropertyTypeMapping.Mappings[condition.Identifier];
            }
            else
            {
                PropertyDefinition propDef = _contentType.PropertyDefinitions
                    .FirstOrDefault(prop =>
                        prop.Name.Equals(condition.Identifier, StringComparison.InvariantCultureIgnoreCase));
                if (propDef != null)
                {
                    propertyType = propDef.Type.DataType;
                }
            }
            
            CompareCondition compareCondition = MapEqualityOperatorToCompareCondition(condition.Operator);
            PropertyCriteriaCollectionStack.Peek().Add(new PropertyCriteria
            {
                Condition = compareCondition,
                Value = condition.Value,
                Name = condition.Identifier,
                Type = propertyType,
                Required = true
            });
        }

        public void VisitQueryExpression(CqlQueryBinaryExpression binaryExpression)
        {
            if (binaryExpression.Operator == ConditionalOperator.Or)
            {
                PropertyCriteriaCollectionStack.Push(new PropertyCriteriaCollection());
            }

            ExpressionVisitor leftExpressionVisitor = new ExpressionVisitor(
                _contentType,
                PropertyCriteriaCollectionStack);
            binaryExpression.LeftExpression.Accept(leftExpressionVisitor);

            if (binaryExpression.Operator == ConditionalOperator.Or)
            {
                PropertyCriteriaCollectionStack.Push(new PropertyCriteriaCollection());
            }

            ExpressionVisitor rightExpressionVisitor = new ExpressionVisitor(
                _contentType,
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
