namespace Cmsql.Query
{
    public class CmsqlQuery
    {
        public string ContentType { get; set; }

        public CmsqlQueryStartNode StartNode { get; set; }

        public ICmsqlQueryExpression Criteria { get; set; }
    }
}
