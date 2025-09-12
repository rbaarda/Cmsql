using Cmsql.Grammar.Parsing;
using Cmsql.Query.Execution;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Cmsql.Test")]

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

            var parser = new CmsqlQueryParser();
            var parseResult = parser.Parse(query);
            if (parseResult.Errors != null && parseResult.Errors.Any())
            {
                return new CmsqlQueryResultSet(parseResult, new CmsqlQueryExecutionResult());
            }

            var executionResult = _queryRunner.ExecuteQueries(parseResult.Queries);

            return new CmsqlQueryResultSet(parseResult, executionResult);
        }
    }
}
