using System.Collections.Generic;
using Cql.Query;

namespace Cql.Grammar.Parsing
{
    public class CqlQueryParseResult
    {
        public IEnumerable<CqlQueryParseError> Errors { get; internal set; }

        public IEnumerable<CqlQuery> Queries { get; internal set; }
    }
}
