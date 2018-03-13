using System.Collections.Generic;
using Antlr4.Runtime;

namespace Cmsql.Grammar.Parsing.Internal
{
    internal class CmsqlParserErrorListener : BaseErrorListener
    {
        private readonly IList<CmsqlQueryParseError> _parseErrors;

        internal IEnumerable<CmsqlQueryParseError> ParseErrors => _parseErrors;

        internal CmsqlParserErrorListener()
        {
            _parseErrors = new List<CmsqlQueryParseError>();
        }

        public override void SyntaxError(
            IRecognizer recognizer,
            IToken offendingSymbol,
            int line,
            int charPositionInLine,
            string msg,
            RecognitionException e)
        {
            _parseErrors.Add(new CmsqlQueryParseError(line, charPositionInLine, msg));
        }
    }
}
