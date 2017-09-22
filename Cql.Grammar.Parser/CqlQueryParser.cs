using System;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Tree;
using Cql.Grammar.Parser.Internal;
using Cql.Query;

namespace Cql.Grammar.Parser
{
    public class CqlQueryParser
    {
        public CqlQueryParseResult Parse(string cqlQuery)
        {
            if (string.IsNullOrWhiteSpace(cqlQuery))
            {
                throw new ArgumentException($"Parameter '{nameof(cqlQuery)}' is null, empty or whitespace.");
            }
            
            CqlParser parser = CreateCqlParser(cqlQuery);
            CqlParserErrorListener errorListener = new CqlParserErrorListener();
            parser.AddErrorListener(errorListener);

            IParseTree parseTree = parser.queries();
            QueriesVisitor queriesVisitor = new QueriesVisitor();

            CqlQueryParseResult parseResult = new CqlQueryParseResult
            {
                Errors = errorListener.ParseErrors,
                Queries = queriesVisitor.Visit(parseTree)
            };
            return parseResult;
        }

        internal CqlParser CreateCqlParser(string cqlQuery)
        {
            return new CqlParser(
                new CommonTokenStream(
                    new CqlLexer(
                        new AntlrInputStream(cqlQuery))))
            {
                //ErrorHandler = new BailErrorStrategy(),
                Interpreter = {PredictionMode = PredictionMode.Sll}
            };
        }
    }
}
