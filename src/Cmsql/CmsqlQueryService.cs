using System;
using System.Linq;
using Cmsql.Grammar.Parsing;
using Cmsql.Query.Execution;

namespace Cmsql
{
    public class CmsqlQueryService : ICmsqlQueryService
    {
        private readonly ICmsqlQueryRunner _queryRunner;

        public CmsqlQueryService(ICmsqlQueryRunner queryRunner)
        {
            _queryRunner = queryRunner ?? throw new ArgumentNullException(nameof(queryRunner));
        }

        public CmsqlQueryResultSet ExecuteQuery(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException($"Parameter '{nameof(query)}' is null or whitespace.");
            }

            CmsqlQueryParser parser = new CmsqlQueryParser();
            CmsqlQueryParseResult parseResult = parser.Parse(query);
            if (parseResult.Errors != null && parseResult.Errors.Any())
            {
                return new CmsqlQueryResultSet(parseResult, new CmsqlQueryExecutionResult());
            }

            CmsqlQueryExecutionResult executionResult = _queryRunner.ExecuteQueries(parseResult.Queries);

            return new CmsqlQueryResultSet(parseResult, executionResult);
        }
    }
}
