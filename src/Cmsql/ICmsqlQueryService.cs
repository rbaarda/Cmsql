namespace Cmsql
{
    public interface ICmsqlQueryService
    {
        CmsqlQueryResultSet ExecuteQuery(string queries);
    }
}
