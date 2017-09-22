using System.Collections.Generic;
using Cql.Query;

namespace Cql.Grammar.Parser
{
    public class CqlQueryParseResult
    {
        public IEnumerable<CqlQueryParseError> Errors { get; internal set; }

        public IEnumerable<CqlQuery> Queries { get; internal set; }
    }
}
