using System;
using System.Linq;
using Cql.Grammar.Parsing;
using Cql.Query.Execution;

namespace Cql
{
    public class CqlQueryService : ICqlQueryService
    {
        private readonly ICqlQueryRunner _queryRunner;

        public CqlQueryService(ICqlQueryRunner queryRunner)
        {
            _queryRunner = queryRunner ?? throw new ArgumentNullException(nameof(queryRunner));
        }

        public CqlQueryResultSet ExecuteQuery(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException($"Parameter '{nameof(query)}' is null or whitespace.");
            }

            CqlQueryParser parser = new CqlQueryParser();
            CqlQueryParseResult parseResult = parser.Parse(query);
            if (parseResult.Errors != null && parseResult.Errors.Any())
            {
                return new CqlQueryResultSet(parseResult, new CqlQueryExecutionResult());
            }

            CqlQueryExecutionResult executionResult = _queryRunner.ExecuteQueries(parseResult.Queries);

            return new CqlQueryResultSet(parseResult, executionResult);
        }
    }
}
