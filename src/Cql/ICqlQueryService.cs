namespace Cql
{
    public interface ICqlQueryService
    {
        CqlQueryResultSet ExecuteQuery(string queries);
    }
}
