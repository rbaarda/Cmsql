using System.Collections.Generic;
using Cql.Query;

namespace Cql
{
    public interface ICqlQueryService
    {
        IEnumerable<CqlQueryResult> ExecuteQuery(string query);
    }
}