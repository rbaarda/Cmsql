using System.Collections.Generic;
using Cql.Query;

namespace Cql
{
    public interface ICqlQueryRunner
    {
        CqlQueryExecutionResult ExecuteQueries(IEnumerable<CqlQuery> queries);
    }
}
