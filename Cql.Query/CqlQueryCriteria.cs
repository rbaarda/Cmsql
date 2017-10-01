namespace Cql.Query
{
    public class CqlQueryCriteria
    {
        public ConditionalOperator Operator { get; set; }

        public ICqlQueryExpression FirstExpression { get; set; }

        public ICqlQueryExpression SecondExpression { get; set; }
    }
}
