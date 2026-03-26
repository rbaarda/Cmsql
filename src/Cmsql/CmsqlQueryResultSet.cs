using Cmsql.Grammar.Parsing;
using Cmsql.Query;
using Cmsql.Query.Execution;
using System.Collections.Generic;
using System.Linq;

namespace Cmsql
{
    public class CmsqlQueryResultSet
    {
        public CmsqlQueryParseResult ParseResult { get; }

        public CmsqlQueryExecutionResult ExecutionResult { get; }

        public bool IsSuccess => !HasErrors;

        public bool HasErrors => HasParseErrors || HasExecutionErrors;

        public bool HasParseErrors => ParseResult?.Errors != null && ParseResult.Errors.Any();

        public bool HasExecutionErrors => ExecutionResult?.Errors != null && ExecutionResult.Errors.Any();

        public IEnumerable<string> Errors
        {
            get
            {
                var errors = new List<string>();

                if (HasParseErrors)
                {
                    errors.AddRange(ParseResult.Errors.Select(e => $"Parse Error: {e.Message}"));
                }

                if (HasExecutionErrors)
                {
                    errors.AddRange(ExecutionResult.Errors.Select(e => $"Execution Error: {e.Message}"));
                }

                return errors;
            }
        }

        public CmsqlQueryResultSet(
            CmsqlQueryParseResult parseResult,
            CmsqlQueryExecutionResult executionResult)
        {
            ParseResult = parseResult;
            ExecutionResult = executionResult;
        }

        public static CmsqlQueryResultSet CreateParseFailure(CmsqlQueryParseResult parseResult)
        {
            return new CmsqlQueryResultSet(parseResult, new CmsqlQueryExecutionResult());
        }

        public static CmsqlQueryResultSet CreateSuccess(
            CmsqlQueryParseResult parseResult,
            CmsqlQueryExecutionResult executionResult)
        {
            return new CmsqlQueryResultSet(parseResult, executionResult);
        }

        public IEnumerable<ICmsqlQueryResult> GetResults()
        {
            if (IsSuccess && ExecutionResult?.QueryResults != null)
            {
                return ExecutionResult.QueryResults;
            }

            return Enumerable.Empty<ICmsqlQueryResult>();
        }
    }
}
