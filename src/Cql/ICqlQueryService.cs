namespace Cmsql
{
    public interface ICqlQueryService
    {
        CqlQueryResultSet ExecuteQuery(string queries);
    }
}
