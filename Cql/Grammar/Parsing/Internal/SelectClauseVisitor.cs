namespace Cql.Grammar.Parsing.Internal
{
    internal class SelectClauseVisitor : CqlBaseVisitor<string>
    {
        public override string VisitSelectClause(CqlParser.SelectClauseContext context)
        {
            return context.IDENTIFIER().GetText();
        }
    }
}
