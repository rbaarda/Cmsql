using System.Collections.Generic;
using Cql.Query;

namespace Cql
{
    public class CqlQueryExecutionResult
    {
        public IEnumerable<CqlQueryResult> QueryResults { get; internal set; }
    }
}
