using Cql.Query;

namespace Cql.Grammar.Parser.Internal
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
            }

            return EqualityOperator.None;
        }
    }
}
