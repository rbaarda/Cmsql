using System.Collections.Generic;
using System.Linq;
using Cql.Query;

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
