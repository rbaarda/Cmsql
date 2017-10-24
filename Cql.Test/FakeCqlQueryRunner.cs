using System.Collections.Generic;
using Cql.Query;
using Cql.Query.Execution;

namespace Cql.Test
{
    public class FakeCqlQueryRunner : ICqlQueryRunner
    {
        public CqlQueryExecutionResult ExecuteQueries(IEnumerable<CqlQuery> queries)
        {
            return new CqlQueryExecutionResult();
        }
    }
}
