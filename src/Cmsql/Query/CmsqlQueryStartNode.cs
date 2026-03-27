namespace Cmsql.Query
{
    public record CmsqlQueryStartNode
    {
        public string StartNodeId { get; init; }

        public CmsqlQueryStartNodeType StartNodeType { get; init; }
    }
}
