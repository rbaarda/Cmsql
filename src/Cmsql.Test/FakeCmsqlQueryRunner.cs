using System.Collections.Generic;
using System.Linq;
using Cmsql.Query;
using Cmsql.Query.Execution;

namespace Cmsql.Test
{
    public class FakeCmsqlQueryRunner : ICmsqlQueryRunner
    {
        private List<CmsqlQuery> _executedQueries = new List<CmsqlQuery>();

        public CmsqlQueryExecutionResult ExecutionResultToReturn { get; set; } = new CmsqlQueryExecutionResult();

        public IReadOnlyList<CmsqlQuery> ExecutedQueries => _executedQueries;

        public int ExecuteCallCount { get; private set; }

        public CmsqlQueryExecutionResult ExecuteQueries(IEnumerable<CmsqlQuery> queries)
        {
            ExecuteCallCount++;
            _executedQueries = queries?.ToList() ?? new List<CmsqlQuery>();

            return ExecutionResultToReturn ?? new CmsqlQueryExecutionResult();
        }
    }
}
