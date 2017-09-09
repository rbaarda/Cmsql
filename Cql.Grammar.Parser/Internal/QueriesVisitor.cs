using System.Collections.Generic;
using System.Linq;
using Cql.Query;

namespace Cql.Grammar.Parser.Internal
{
    internal class QueriesVisitor : CqlBaseVisitor<IEnumerable<CqlQuery>>
    {
        public override IEnumerable<CqlQuery> VisitQueries(CqlParser.QueriesContext context)
        {
            QueryVisitor queryVisitor = new QueryVisitor();
            
            return context.query().Select(query => query.Accept(queryVisitor)).ToList();
        }
    }
}
