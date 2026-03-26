using Cmsql.Query;

namespace Cmsql.Grammar.Parsing.Internal
{
    internal class QueryVisitor : CmsqlBaseVisitor<CmsqlQuery>
    {
        public override CmsqlQuery VisitQuery(CmsqlParser.QueryContext context)
        {
            var whereClauseContext = context.whereClause();
            return new CmsqlQuery
            {
                ContentType = context.selectClause().Accept(new SelectClauseVisitor()),
                StartNode = context.fromClause().Accept(new FromClauseVisitor()),
                Criteria = whereClauseContext != null
                    ? whereClauseContext.Accept(new WhereClauseVisitor())
                    : null
            };
        }
    }
}
