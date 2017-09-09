using System.Collections.Generic;
using Cql.Query;
using FluentAssertions;
using Xunit;

namespace Cql.Grammar.Parser.Test
{
    public class CqlQueryParserTest
    {
        [Fact]
        public void Test_Can_Parse_Single_Select_Statement_Without_Terminator()
        {
            CqlQueryParser parser = new CqlQueryParser();
            IEnumerable<CqlQuery> queries = parser.Parse("select pages from start where publishdate = 'lala'");

            queries.Should().NotBeNullOrEmpty();
        }
    }
}
