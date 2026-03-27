using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Cmsql.Query.Execution
{
    public record CmsqlQueryExecutionResult
    {
        public required IEnumerable<ICmsqlQueryResult> QueryResults { get; init; }

        public required IEnumerable<CmsqlQueryExecutionError> Errors { get; init; }

        [SetsRequiredMembers]
        public CmsqlQueryExecutionResult(
            IEnumerable<ICmsqlQueryResult> queryResults,
            IEnumerable<CmsqlQueryExecutionError> errors)
        {
            QueryResults = queryResults;
            Errors = errors;
        }

        [SetsRequiredMembers]
        public CmsqlQueryExecutionResult()
            : this(Enumerable.Empty<ICmsqlQueryResult>(), Enumerable.Empty<CmsqlQueryExecutionError>())
        {
        }
    }
}
