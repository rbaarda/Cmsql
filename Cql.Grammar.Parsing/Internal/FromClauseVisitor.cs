using Cql.Query;

namespace Cql.Grammar.Parsing.Internal
{
    internal class FromClauseVisitor : CqlBaseVisitor<CqlQueryStartNode>
    {
        public override CqlQueryStartNode VisitFromClause(CqlParser.FromClauseContext context)
        {
            if (context.NUMBER() != null)
            {
                return new CqlQueryStartNode
                {
                    StartNodeId = context.NUMBER().GetText(),
                    StartNodeType = CqlQueryStartNodeType.Id
                };
            }
            if (context.START() != null)
            {
                return new CqlQueryStartNode
                {
                    StartNodeType = CqlQueryStartNodeType.Start
                };
            }
            if (context.ROOT() != null)
            {
                return new CqlQueryStartNode
                {
                    StartNodeType = CqlQueryStartNodeType.Root
                };
            }

            return null;
        }
    }
}
