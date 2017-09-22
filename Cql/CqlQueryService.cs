using System;
using System.Linq;
using Cql.Grammar.Parsing;
using Cql.Provider;
using Cql.Query;

namespace Cql
{
    public class CqlQueryService : ICqlQueryService
    {
        private readonly ICqlQueryRunner _queryRunner;

        public CqlQueryService(ICqlQueryRunner queryRunner)
        {
            _queryRunner = queryRunner;
        }

        public CqlQueryExecutionResult ExecuteQuery(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException($"Parameter '{nameof(query)}' is null or whitespace.");
            }

            CqlQueryParser parser = new CqlQueryParser();
            CqlQueryParseResult parseResult = parser.Parse(query);
            if (parseResult.Errors != null && parseResult.Errors.Any())
            {
                return new CqlQueryExecutionResult
                {
                    QueryResults = Enumerable.Empty<CqlQueryResult>()
                };
            }

            return _queryRunner.ExecuteQueries(parseResult.Queries);
        }
    }
}
