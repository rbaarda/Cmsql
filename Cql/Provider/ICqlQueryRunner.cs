using System.Collections.Generic;
using Cql.Query;

namespace Cql.Provider
{
    public interface ICqlQueryRunner
    {
        CqlQueryExecutionResult ExecuteQueries(IEnumerable<CqlQuery> queries);
    }
}
