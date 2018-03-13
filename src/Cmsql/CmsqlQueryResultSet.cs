using Cmsql.Grammar.Parsing;
using Cmsql.Query.Execution;

namespace Cmsql
{
    public class CmsqlQueryResultSet
    {
        public CmsqlQueryParseResult ParseResult { get; }

        public CmsqlQueryExecutionResult ExecutionResult { get; }

        public CmsqlQueryResultSet(
            CmsqlQueryParseResult parseResult,
            CmsqlQueryExecutionResult executionResult)
        {
            ParseResult = parseResult;
            ExecutionResult = executionResult;
        }
    }
}
