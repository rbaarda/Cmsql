using Cql.Query;

namespace Cql.Grammar.Parsing.Internal
{
    internal class QueryVisitor : CqlBaseVisitor<CqlQuery>
    {
        public override CqlQuery VisitQuery(CqlParser.QueryContext context)
        {
            CqlQuery query = new CqlQuery();

            SelectClauseVisitor selectClauseVisitor = new SelectClauseVisitor();
            query.ContentType = context.selectClause().Accept(selectClauseVisitor);

            FromClauseVisitor fromClauseVisitor = new FromClauseVisitor();
            query.StartNode = context.fromClause().Accept(fromClauseVisitor);
            
            CqlParser.WhereClauseContext whereClauseContext = context.whereClause();
            if (whereClauseContext != null)
            {
                WhereClauseVisitor whereClauseVisitor = new WhereClauseVisitor();
                query.Criteria = whereClauseContext.Accept(whereClauseVisitor);
            }

            return query;
        }
    }
}
