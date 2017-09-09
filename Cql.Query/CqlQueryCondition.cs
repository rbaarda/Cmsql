namespace Cql.Query
{
    public class CqlQueryCondition : ICqlQueryExpression
    {
        public EqualityOperator Operator { get; set; }

        public string Identifier { get; set; }

        public string Value { get; set; }
    }
}
