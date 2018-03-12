using Cmsql.Query;

namespace Cmsql.Grammar.Parsing.Internal
{
    internal class ConditionVisitor : CmsqlBaseVisitor<CqlQueryCondition>
    {
        public override CqlQueryCondition VisitCondition(CmsqlParser.ConditionContext context)
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
                case CmsqlParser.EQUALS:
                    return EqualityOperator.Equals;
                case CmsqlParser.NOTEQUALS:
                    return EqualityOperator.NotEquals;
                case CmsqlParser.GREATERTHAN:
                    return EqualityOperator.GreaterThan;
                case CmsqlParser.LESSTHAN:
                    return EqualityOperator.LessThan;
                case CmsqlParser.GREATERTHANOREQUALS:
                    return EqualityOperator.GreaterThanOrEquals;
                case CmsqlParser.LESSTHANOREQUALS:
                    return EqualityOperator.LessThanOrEquals;
            }

            return EqualityOperator.None;
        }
    }
}
