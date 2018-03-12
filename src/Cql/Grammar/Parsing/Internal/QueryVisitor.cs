using Cmsql.Query;

namespace Cmsql.Grammar.Parsing.Internal
{
    internal class QueryVisitor : CmsqlBaseVisitor<CmsqlQuery>
    {
        public override CmsqlQuery VisitQuery(CmsqlParser.QueryContext context)
        {
            CmsqlQuery query = new CmsqlQuery();

            SelectClauseVisitor selectClauseVisitor = new SelectClauseVisitor();
            query.ContentType = context.selectClause().Accept(selectClauseVisitor);

            FromClauseVisitor fromClauseVisitor = new FromClauseVisitor();
            query.StartNode = context.fromClause().Accept(fromClauseVisitor);
            
            CmsqlParser.WhereClauseContext whereClauseContext = context.whereClause();
            if (whereClauseContext != null)
            {
                WhereClauseVisitor whereClauseVisitor = new WhereClauseVisitor();
                query.Criteria = whereClauseContext.Accept(whereClauseVisitor);
            }

            return query;
        }
    }
}
