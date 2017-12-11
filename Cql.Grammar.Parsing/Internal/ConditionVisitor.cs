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
                case CqlParser.GREATERTHAN:
                    return EqualityOperator.GreaterThan;
                case CqlParser.LESSTHAN:
                    return EqualityOperator.LessThan;
                case CqlParser.GREATERTHANOREQUALS:
                    return EqualityOperator.GreaterThanOrEquals;
                case CqlParser.LESSTHANOREQUALS:
                    return EqualityOperator.LessThanOrEquals;
            }

            return EqualityOperator.None;
        }
    }
}
