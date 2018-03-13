using System.Collections.Generic;

namespace Cmsql.Query.Execution
{
    public interface ICmsqlQueryRunner
    {
        CmsqlQueryExecutionResult ExecuteQueries(IEnumerable<CmsqlQuery> queries);
    }
}
