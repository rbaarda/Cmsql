using System.Collections.Generic;

namespace Cmsql.Query.Execution
{
    public interface ICqlQueryRunner
    {
        CqlQueryExecutionResult ExecuteQueries(IEnumerable<CqlQuery> queries);
    }
}
