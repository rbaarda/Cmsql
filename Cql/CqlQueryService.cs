using System.Collections.Generic;
using System.Linq;
using Cql.Grammar.Parser;
using Cql.Provider;
using Cql.Query;

namespace Cql
{
    public class CqlQueryService : ICqlQueryService
    {
        private readonly ICqlQueryProvider _queryProvider;

        public CqlQueryService(ICqlQueryProvider queryProvider)
        {
            _queryProvider = queryProvider;
        }

        public IEnumerable<CqlQueryResult> ExecuteQuery(string query)
        {
            CqlQueryParser parser = new CqlQueryParser();
            IEnumerable<CqlQuery> parsedQuery = parser.Parse(query);
            return parsedQuery != null
                ? _queryProvider.ExecuteQuery(parsedQuery)
                : Enumerable.Empty<CqlQueryResult>();
        }
    }
}
