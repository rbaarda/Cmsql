using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Tree;
using Cmsql.Grammar.Parsing.Internal;

namespace Cmsql.Grammar.Parsing
{
    public class CmsqlQueryParser
    {
        public CmsqlQueryParseResult Parse(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException($"Parameter '{nameof(query)}' is null, empty or whitespace.");
            }
            
            CmsqlParser parser = CreateParser(query);
            parser.RemoveErrorListeners();

            CmsqlParserErrorListener errorListener = new CmsqlParserErrorListener();
            parser.AddErrorListener(errorListener);

            IParseTree parseTree = parser.queries();
            QueriesVisitor queriesVisitor = new QueriesVisitor();
            return new CmsqlQueryParseResult
            {
                Errors = errorListener.ParseErrors,
                Queries = queriesVisitor.Visit(parseTree)
            };
        }

        private CmsqlParser CreateParser(string query)
        {
            return new CmsqlParser(
                new CommonTokenStream(
                    new CmsqlLexer(
                        new AntlrInputStream(query))))
            {
                Interpreter = {PredictionMode = PredictionMode.Sll}
            };
        }
    }
}
