namespace Cmsql.Query
{
    public class CmsqlQuery
    {
        public string ContentType { get; init; }

        public CmsqlQueryStartNode StartNode { get; init; }

        public ICmsqlQueryExpression Criteria { get; init; }
    }
}
