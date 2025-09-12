using Cmsql.Query;

namespace Cmsql.Grammar.Parsing.Internal
{
    internal class QueryVisitor : CmsqlBaseVisitor<CmsqlQuery>
    {
        public override CmsqlQuery VisitQuery(CmsqlParser.QueryContext context)
        {
            var query = new CmsqlQuery();

            var selectClauseVisitor = new SelectClauseVisitor();
            query.ContentType = context.selectClause().Accept(selectClauseVisitor);

            var fromClauseVisitor = new FromClauseVisitor();
            query.StartNode = context.fromClause().Accept(fromClauseVisitor);
            
            var whereClauseContext = context.whereClause();
            if (whereClauseContext != null)
            {
                var whereClauseVisitor = new WhereClauseVisitor();
                query.Criteria = whereClauseContext.Accept(whereClauseVisitor);
            }

            return query;
        }
    }
}
