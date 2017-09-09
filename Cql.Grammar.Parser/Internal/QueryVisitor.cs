using Cql.Query;

namespace Cql.Grammar.Parser.Internal
{
    internal class QueryVisitor : CqlBaseVisitor<CqlQuery>
    {
        public override CqlQuery VisitQuery(CqlParser.QueryContext context)
        {
            FromClauseVisitor fromClauseVisitor = new FromClauseVisitor();
            CqlQueryStartNode startNode = context.fromClause().Accept(fromClauseVisitor);

            CqlQuery query = new CqlQuery
            {
                ContentType = context.IDENTIFIER().GetText(),
                StartNode = startNode
            };
            
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
