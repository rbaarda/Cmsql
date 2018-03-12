namespace Cmsql.Query.Execution
{
    public class CqlQueryExecutionError
    {
        public string Message { get; }

        public CqlQueryExecutionError(string message)
        {
            Message = message;
        }
    }
}
