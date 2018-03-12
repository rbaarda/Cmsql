using System.Collections.Generic;
using Cmsql.Query;

namespace Cmsql.Grammar.Parsing
{
    public class CqlQueryParseResult
    {
        public IEnumerable<CqlQueryParseError> Errors { get; internal set; }

        public IEnumerable<CqlQuery> Queries { get; internal set; }
    }
}
