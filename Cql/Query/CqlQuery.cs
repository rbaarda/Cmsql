namespace Cql.Query
{
    public class CqlQuery
    {
        public string ContentType { get; set; }

        public CqlQueryStartNode StartNode { get; set; }

        public ICqlQueryExpression Criteria { get; set; }
    }
}
