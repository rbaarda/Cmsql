using System.Collections.Generic;
using Cmsql.Query;
using Cmsql.Query.Execution;

namespace Cmsql.Test
{
    public class FakeCmsqlQueryRunner : ICmsqlQueryRunner
    {
        public CmsqlQueryExecutionResult ExecuteQueries(IEnumerable<CmsqlQuery> queries)
        {
            return new CmsqlQueryExecutionResult();
        }
    }
}
