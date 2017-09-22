namespace Cql
{
    public interface ICqlQueryService
    {
        CqlQueryExecutionResult ExecuteQuery(string queries);
    }
}
