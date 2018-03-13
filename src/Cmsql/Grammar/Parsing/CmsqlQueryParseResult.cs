using System.Collections.Generic;
using Cmsql.Query;

namespace Cmsql.Grammar.Parsing
{
    public class CmsqlQueryParseResult
    {
        public IEnumerable<CmsqlQueryParseError> Errors { get; internal set; }

        public IEnumerable<CmsqlQuery> Queries { get; internal set; }
    }
}
