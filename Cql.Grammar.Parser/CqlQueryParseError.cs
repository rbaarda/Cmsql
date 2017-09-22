namespace Cql.Grammar.Parser
{
    public class CqlQueryParseError
    {
        public int Line { get; }

        public int CharPositionInLine { get; }

        public string Message { get; }

        internal CqlQueryParseError(int line, int charPositionInLine, string message)
        {
            Line = line;
            CharPositionInLine = charPositionInLine;
            Message = message;
        }
    }
}
