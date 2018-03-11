﻿using Cql.Query;

namespace Cql.Grammar.Parsing.Internal
{
    internal class WhereClauseVisitor : CqlBaseVisitor<ICqlQueryExpression>
    {
        public override ICqlQueryExpression VisitWhereClause(CqlParser.WhereClauseContext context)
        {
            ExpressionVisitor expressionVisitor = new ExpressionVisitor();

            return context.expression().Accept(expressionVisitor);
        }
    }
}
