using Cmsql.Query;

namespace Cmsql.Grammar.Parsing.Internal
{
    internal class WhereClauseVisitor : CmsqlBaseVisitor<ICqlQueryExpression>
    {
        public override ICqlQueryExpression VisitWhereClause(CmsqlParser.WhereClauseContext context)
        {
            ExpressionVisitor expressionVisitor = new ExpressionVisitor();

            return context.expression().Accept(expressionVisitor);
        }
    }
}
