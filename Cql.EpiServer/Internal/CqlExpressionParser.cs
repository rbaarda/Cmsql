using System.Collections.Generic;
using System.Linq;
using Cql.Query;
using EPiServer;
using EPiServer.DataAbstraction;

namespace Cql.EpiServer.Internal
{
    internal class CqlExpressionParser
    {
        public IEnumerable<PropertyCriteriaCollection> Parse(ContentType contentType, ICqlQueryExpression expression)
        {
            if (expression == null)
            {
                return Enumerable.Empty<PropertyCriteriaCollection>();
            }

            Stack<PropertyCriteriaCollection> propertyCriteriaCollectionStack = new Stack<PropertyCriteriaCollection>();
            propertyCriteriaCollectionStack.Push(new PropertyCriteriaCollection());

            QueryConditionToPropertyCriteriaMapper mapper =
                new QueryConditionToPropertyCriteriaMapper(new PropertyDataTypeResolver(contentType));
            CqlExpressionVisitor visitor = new CqlExpressionVisitor(mapper, propertyCriteriaCollectionStack);
            expression.Accept(visitor);

            return propertyCriteriaCollectionStack;
        }
    }
}
