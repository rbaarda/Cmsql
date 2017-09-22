using Cql.Query;

namespace Cql.Grammar.Parsing.Internal
{
    internal class ConditionVisitor : CqlBaseVisitor<CqlQueryCondition>
    {
        public override CqlQueryCondition VisitCondition(CqlParser.ConditionContext context)
        {
            return new CqlQueryCondition
            {
                Operator = GetEqualityOperator(context.op.Type),
                Identifier = context.IDENTIFIER().GetText(),
                Value = context.LITERAL().GetText().Trim('\'')
            };
        }

        private EqualityOperator GetEqualityOperator(int token)
        {
            switch (token)
            {
                case CqlParser.EQUALS:
                    return EqualityOperator.Equals;
                case CqlParser.NOTEQUALS:
                    return EqualityOperator.NotEquals;
                case CqlParser.LARGERTHAN:
                    return EqualityOperator.LargerThan;
                case CqlParser.LESSTHAN:
                    return EqualityOperator.LessThan;
                case CqlParser.LARGERTHANOREQUALS:
                    return EqualityOperator.LargerThanOrEquals;
                case CqlParser.LESSTHANOREQUALS:
                    return EqualityOperator.LessThanOrEquals;
            }

            return EqualityOperator.None;
        }
    }
}
