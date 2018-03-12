using Cmsql.Query;

namespace Cmsql.Grammar.Parsing.Internal
{
    internal class WhereClauseVisitor : CmsqlBaseVisitor<ICmsqlQueryExpression>
    {
        public override ICmsqlQueryExpression VisitWhereClause(CmsqlParser.WhereClauseContext context)
        {
            ExpressionVisitor expressionVisitor = new ExpressionVisitor();

            return context.expression().Accept(expressionVisitor);
        }
    }
}
