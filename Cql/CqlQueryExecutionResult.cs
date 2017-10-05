using System.Collections.Generic;
using System.Linq;

namespace Cql
{
    public class CqlQueryExecutionResult
    {
        public IEnumerable<CqlQueryResult> QueryResults { get; internal set; }

        public CqlQueryExecutionResult()
        {
            QueryResults = Enumerable.Empty<CqlQueryResult>();
        }
    }
}
