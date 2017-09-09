namespace Cql.Query
{
    public class CqlQueryConditionalExpression : ICqlQueryExpression
    {
        public ConditionalOperator Operator { get; set; }

        public CqlQueryCondition FirstExpression { get; set; }

        public CqlQueryCondition SecondExpression { get; set; }
    }
}
