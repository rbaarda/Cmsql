using Cql.Grammar.Parsing;

namespace Cql
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
