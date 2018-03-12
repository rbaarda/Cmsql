using Antlr4.Runtime;
using Cmsql.Grammar;

namespace Cmsql.Test.Grammar.Parsing
{
    internal static class CmsqlParserFactory
    {
        public static CmsqlParser CreateParserForQuery(string query)
        {
            return new CmsqlParser(
                new CommonTokenStream(
                    new CmsqlLexer(
                        new AntlrInputStream(query))))
            {
                ErrorHandler = new BailErrorStrategy()
            };
        }
    }
}
