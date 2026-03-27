namespace Cmsql.Query
{
    public record CmsqlQuery
    {
        public string ContentType { get; init; }

        public CmsqlQueryStartNode StartNode { get; init; }

        public ICmsqlQueryExpression Criteria { get; init; }
    }
}
