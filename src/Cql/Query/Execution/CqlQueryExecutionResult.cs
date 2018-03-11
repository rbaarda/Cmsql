using System.Collections.Generic;
using System.Linq;

namespace Cql.Query.Execution
{
    public class CqlQueryExecutionResult
    {
        public IEnumerable<ICqlQueryResult> QueryResults { get; }

        public IEnumerable<CqlQueryExecutionError> Errors { get; }

        public CqlQueryExecutionResult(
            IEnumerable<ICqlQueryResult> queryResults,
            IEnumerable<CqlQueryExecutionError> errors)
        {
            QueryResults = queryResults;
            Errors = errors;
        }

        public CqlQueryExecutionResult()
            : this(Enumerable.Empty<ICqlQueryResult>(), Enumerable.Empty<CqlQueryExecutionError>())
        {

        }
    }
}
