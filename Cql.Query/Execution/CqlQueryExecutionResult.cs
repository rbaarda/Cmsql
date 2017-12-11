using System.Collections.Generic;
using System.Linq;

namespace Cql.Query.Execution
{
    public class CqlQueryExecutionResult
    {
        public IEnumerable<CqlQueryResult> QueryResults { get; set; }

        public CqlQueryExecutionResult()
        {
            QueryResults = Enumerable.Empty<CqlQueryResult>();
        }
    }
}
