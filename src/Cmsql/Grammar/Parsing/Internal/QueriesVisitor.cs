﻿using System.Collections.Generic;
using System.Linq;
using Cmsql.Query;

namespace Cmsql.Grammar.Parsing.Internal
{
    internal class QueriesVisitor : CmsqlBaseVisitor<IEnumerable<CmsqlQuery>>
    {
        public override IEnumerable<CmsqlQuery> VisitQueries(CmsqlParser.QueriesContext context)
        {
            QueryVisitor queryVisitor = new QueryVisitor();
            
            return context.query().Select(query => query.Accept(queryVisitor)).ToList();
        }
    }
}
