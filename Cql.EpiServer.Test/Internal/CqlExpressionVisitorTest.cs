using System.Linq;
using Cql.EpiServer.Internal;
using Cql.Query;
using EPiServer;
using EPiServer.DataAbstraction;
using EPiServer.Filters;
using FluentAssertions;
using Xunit;

namespace Cql.EpiServer.Test.Internal
{
    public class CqlExpressionVisitorTest
    {
        [Fact]
        public void Test_can_parse_query_condition_to_property_criteria()
        {
            // Arrange
            CqlQueryCondition condition = new CqlQueryCondition
            {
                Identifier = "PageName",
                Operator = EqualityOperator.GreaterThan,
                Value = "5"
            };
            
            CqlExpressionVisitorContext context = new CqlExpressionVisitorContext();

            CqlExpressionVisitor cqlExpressionVisitor =
                new CqlExpressionVisitor(
                    new QueryConditionToPropertyCriteriaMapper(
                        new PropertyDataTypeResolver(new ContentType())), context);

            // Act
            cqlExpressionVisitor.VisitQueryCondition(condition);

            PropertyCriteriaCollection propertyCriteriaCollection = context.GetCriteria().Single();

            PropertyCriteria propertyCriteria = propertyCriteriaCollection.Single();

            // Assert
            propertyCriteria.Value.ShouldBeEquivalentTo(condition.Value);
            propertyCriteria.Condition.ShouldBeEquivalentTo(CompareCondition.GreaterThan);
            propertyCriteria.Name.ShouldBeEquivalentTo(condition.Identifier);
        }
    }
}
