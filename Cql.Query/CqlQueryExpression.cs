namespace Cql.Query
{
    public class CqlQueryExpression : ICqlQueryExpression
    {
        public ConditionalOperator Operator { get; set; }

        public ICqlQueryExpression LeftExpression { get; set; }

        public ICqlQueryExpression RightExpression { get; set; }
    }
}
