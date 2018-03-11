using System.Collections.Generic;
using Antlr4.Runtime;

namespace Cql.Grammar.Parsing.Internal
{
    internal class CqlParserErrorListener : BaseErrorListener
    {
        private readonly IList<CqlQueryParseError> _parseErrors;

        internal IEnumerable<CqlQueryParseError> ParseErrors => _parseErrors;

        internal CqlParserErrorListener()
        {
            _parseErrors = new List<CqlQueryParseError>();
        }

        public override void SyntaxError(
            IRecognizer recognizer,
            IToken offendingSymbol,
            int line,
            int charPositionInLine,
            string msg,
            RecognitionException e)
        {
            _parseErrors.Add(new CqlQueryParseError(line, charPositionInLine, msg));
        }
    }
}
