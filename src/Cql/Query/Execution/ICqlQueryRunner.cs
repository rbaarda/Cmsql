using System.Collections.Generic;

namespace Cql.Query.Execution
{
    public interface ICqlQueryRunner
    {
        CqlQueryExecutionResult ExecuteQueries(IEnumerable<CqlQuery> queries);
    }
}
