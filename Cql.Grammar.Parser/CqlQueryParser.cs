using System;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Cql.Grammar.Parser.Internal;
using Cql.Query;

namespace Cql.Grammar.Parser
{
    public class CqlQueryParser
    {
        public IEnumerable<CqlQuery> Parse(string cqlQuery)
        {
            if (string.IsNullOrWhiteSpace(cqlQuery))
            {
                throw new ArgumentException($"Parameter '{nameof(cqlQuery)}' is null, empty or whitespace.");
            }
            
            CqlParser parser = CreateCqlParser(cqlQuery);
            IParseTree parseTree = parser.queries();
            QueriesVisitor queriesVisitor = new QueriesVisitor();
            return queriesVisitor.Visit(parseTree);
        }

        internal CqlParser CreateCqlParser(string cqlQuery)
        {
            return new CqlParser(
                new CommonTokenStream(
                    new CqlLexer(
                        new AntlrInputStream(cqlQuery))))
            {
                ErrorHandler = new BailErrorStrategy()
            };
        }
    }
}
