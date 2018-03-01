using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EPiServer;

namespace Cql.EpiServer.Internal
{
    internal class CqlExpressionVisitorContext
    {
        private readonly Stack<PropertyCriteriaCollection> _propertyCriteriaCollectionStack;

        internal CqlExpressionVisitorContext()
        {
            _propertyCriteriaCollectionStack = new Stack<PropertyCriteriaCollection>();
        }

        internal void AddPropertyCriteria(PropertyCriteria propertyCriteria)
        {
            Debug.Assert(propertyCriteria != null);

            if (!_propertyCriteriaCollectionStack.Any())
            {
                PushNewPropertyCriteriaCollection();
            }

            _propertyCriteriaCollectionStack.Peek().Add(propertyCriteria);
        }

        internal void PushNewPropertyCriteriaCollection()
        {
            _propertyCriteriaCollectionStack.Push(new PropertyCriteriaCollection());
        }

        internal IEnumerable<PropertyCriteriaCollection> GetCriteria()
        {
            return _propertyCriteriaCollectionStack;
        }
    }
}
