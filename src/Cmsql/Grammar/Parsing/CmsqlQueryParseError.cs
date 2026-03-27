namespace Cmsql.Grammar.Parsing
{
    public record CmsqlQueryParseError
    {
        public int Line { get; internal init; }

        public int CharPositionInLine { get; internal init; }

        public string Message { get; internal init; }
    }
}
