using Cmsql.Query;

namespace Cmsql.Grammar.Parsing.Internal
{
    internal class FromClauseVisitor : CmsqlBaseVisitor<CmsqlQueryStartNode>
    {
        public override CmsqlQueryStartNode VisitFromClause(CmsqlParser.FromClauseContext context)
        {
            if (context.NUMBER() != null)
            {
                return new CmsqlQueryStartNode
                {
                    StartNodeId = context.NUMBER().GetText(),
                    StartNodeType = CmsqlQueryStartNodeType.Id
                };
            }
            if (context.START() != null)
            {
                return new CmsqlQueryStartNode
                {
                    StartNodeType = CmsqlQueryStartNodeType.Start
                };
            }
            if (context.ROOT() != null)
            {
                return new CmsqlQueryStartNode
                {
                    StartNodeType = CmsqlQueryStartNodeType.Root
                };
            }

            return null;
        }
    }
}
