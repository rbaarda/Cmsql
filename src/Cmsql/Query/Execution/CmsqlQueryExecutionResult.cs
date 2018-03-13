using System.Collections.Generic;
using System.Linq;

namespace Cmsql.Query.Execution
{
    public class CmsqlQueryExecutionResult
    {
        public IEnumerable<ICmsqlQueryResult> QueryResults { get; }

        public IEnumerable<CmsqlQueryExecutionError> Errors { get; }

        public CmsqlQueryExecutionResult(
            IEnumerable<ICmsqlQueryResult> queryResults,
            IEnumerable<CmsqlQueryExecutionError> errors)
        {
            QueryResults = queryResults;
            Errors = errors;
        }

        public CmsqlQueryExecutionResult()
            : this(Enumerable.Empty<ICmsqlQueryResult>(), Enumerable.Empty<CmsqlQueryExecutionError>())
        {

        }
    }
}
