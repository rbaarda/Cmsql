namespace Cmsql.Grammar.Parsing.Internal
{
    internal class SelectClauseVisitor : CmsqlBaseVisitor<string>
    {
        public override string VisitSelectClause(CmsqlParser.SelectClauseContext context)
        {
            return context.IDENTIFIER().GetText();
        }
    }
}
