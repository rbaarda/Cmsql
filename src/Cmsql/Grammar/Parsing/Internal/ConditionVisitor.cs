using Cmsql.Query;
using System;

namespace Cmsql.Grammar.Parsing.Internal
{
    internal class ConditionVisitor : CmsqlBaseVisitor<CmsqlQueryCondition>
    {
        public override CmsqlQueryCondition VisitCondition(CmsqlParser.ConditionContext context)
        {
            return new CmsqlQueryCondition
            {
                Operator = GetEqualityOperator(context.op.Type),
                Identifier = context.IDENTIFIER().GetText(),
                Value = context.LITERAL().GetText().Trim('\'')
            };
        }

        private static EqualityOperator GetEqualityOperator(int token)
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
                default:
                    throw new InvalidOperationException($"Unrecognised equality operator token: {token}");
            }
        }
    }
}
