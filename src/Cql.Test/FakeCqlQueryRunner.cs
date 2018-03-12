using System.Collections.Generic;
using Cmsql.Query;
using Cmsql.Query.Execution;

namespace Cmsql.Test
{
    public class FakeCqlQueryRunner : ICqlQueryRunner
    {
        public CqlQueryExecutionResult ExecuteQueries(IEnumerable<CqlQuery> queries)
        {
            return new CqlQueryExecutionResult();
        }
    }
}
