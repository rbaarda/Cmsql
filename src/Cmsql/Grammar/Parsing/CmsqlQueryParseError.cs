namespace Cmsql.Grammar.Parsing
{
    public class CmsqlQueryParseError
    {
        public int Line { get; }

        public int CharPositionInLine { get; }

        public string Message { get; }

        internal CmsqlQueryParseError(int line, int charPositionInLine, string message)
        {
            Line = line;
            CharPositionInLine = charPositionInLine;
            Message = message;
        }
    }
}
