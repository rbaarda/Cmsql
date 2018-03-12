using Cmsql.Grammar.Parsing;
using Cmsql.Query.Execution;

namespace Cmsql
{
    public class CqlQueryResultSet
    {
        public CqlQueryParseResult ParseResult { get; }

        public CqlQueryExecutionResult ExecutionResult { get; }

        public CqlQueryResultSet(
            CqlQueryParseResult parseResult,
            CqlQueryExecutionResult executionResult)
        {
            ParseResult = parseResult;
            ExecutionResult = executionResult;
        }
    }
}
