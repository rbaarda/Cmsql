using System.Collections.Generic;
using Cql.Query;

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
