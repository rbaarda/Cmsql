using Antlr4.Runtime;
using Cql.Grammar;

namespace Cql.Test.Grammar.Parsing
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
