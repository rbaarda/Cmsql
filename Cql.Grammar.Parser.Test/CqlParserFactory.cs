using Antlr4.Runtime;

namespace Cql.Grammar.Parser.Test
{
    internal static class CqlParserFactory
    {
        public static CqlParser CreateParserForQuery(string query)
        {
            return new CqlParser(
                new CommonTokenStream(
                    new CqlLexer(
                        new AntlrInputStream(query))));
        }
    }
}
