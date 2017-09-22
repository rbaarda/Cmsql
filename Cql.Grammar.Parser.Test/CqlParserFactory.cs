using Antlr4.Runtime;

namespace Cql.Grammar.Parsing.Test
{
    internal static class CqlParserFactory
    {
        public static CqlParser CreateParserForQuery(string query)
        {
            return new CqlParser(
                new CommonTokenStream(
                    new CqlLexer(
                        new AntlrInputStream(query))))
            {
                ErrorHandler = new BailErrorStrategy()
            };
        }
    }
}
