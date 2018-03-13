namespace Cmsql.Query.Execution
{
    public class CmsqlQueryExecutionError
    {
        public string Message { get; }

        public CmsqlQueryExecutionError(string message)
        {
            Message = message;
        }
    }
}
