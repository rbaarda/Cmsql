using Cmsql.Query;

namespace Cmsql.Grammar.Parsing.Internal
{
    internal class FromClauseVisitor : CmsqlBaseVisitor<CqlQueryStartNode>
    {
        public override CqlQueryStartNode VisitFromClause(CmsqlParser.FromClauseContext context)
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
