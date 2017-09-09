using System.Collections.Generic;
using Cql.Query;

namespace Cql.Provider
{
    public interface ICqlQueryProvider
    {
        IEnumerable<CqlQueryResult> ExecuteQuery(IEnumerable<CqlQuery> queries);
    }
}
