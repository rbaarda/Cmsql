using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Cmsql.Grammar;
using FluentAssertions;
using Xunit;

namespace Cmsql.Test.Grammar
{
    public class CmsqlParserTest
    {
        [Theory]
        [InlineData("select test from start;select test from root;select test from 123 where bla = 'foo'")]
        public void Test_queries_rule_can_parse_multiple(string query)
        {
            var parser = CreateParserForQuery(query);
            var tree = parser.queries();

            var pattern = parser.CompileParseTreePattern(
                "<query> <TERMINATOR> <query> <TERMINATOR> <query> <EOF>",
                CmsqlParser.RULE_queries);
            var match = pattern.Match(tree);

            match.Succeeded.Should().BeTrue();
        }

        [Theory]
        [InlineData("select test from start")]
        [InlineData("select test from root")]
        [InlineData("select test from 12345")]
        public void Test_query_rule_can_parse_query_without_where_clause(string query)
        {
            var parser = CreateParserForQuery(query);
            var tree = parser.query();

            var pattern = parser.CompileParseTreePattern(
                "<selectClause> <fromClause>",
                CmsqlParser.RULE_query);

            var match = pattern.Match(tree);
            match.Succeeded.Should().BeTrue();
        }

        [Theory]
        [InlineData("select test from start;")]
        [InlineData("select test from root;")]
        [InlineData("select test from 12345;")]
        public void Test_query_rule_can_parse_query_without_where_clause_with_terminator(string query)
        {
            var parser = CreateParserForQuery(query);
            var tree = parser.query();

            var pattern = parser.CompileParseTreePattern(
                "<selectClause> <fromClause> <TERMINATOR>",
                CmsqlParser.RULE_query);

            var match = pattern.Match(tree);
            match.Succeeded.Should().BeTrue();
        }

        [Theory]
        [InlineData("select test from start where foo = 'bar'")]
        [InlineData("select test from root where foo = 'bar'")]
        [InlineData("select test from 12345 where foo = 'bar'")]
        [InlineData("select test from 12345 where (foo = 'bar')")]
        [InlineData("select test from 12345 where foo = 'bar' and foo = 'bar'")]
        [InlineData("select test from 12345 where foo = 'bar' or foo = 'bar'")]
        [InlineData("select test from 12345 where (foo = 'bar' and foo = 'bar')")]
        [InlineData("select test from 12345 where (foo = 'bar' or foo = 'bar')")]
        [InlineData("select test from 12345 where (foo = 'bar' and foo = 'bar') or (foo = 'bar' and foo = 'bar')")]
        [InlineData("select test from 12345 where (foo = 'bar' and foo = 'bar') and (foo = 'bar' and foo = 'bar')")]
        [InlineData("select test from 12345 where ((foo = 'bar' and foo = 'bar') and (foo = 'bar' and foo = 'bar'))")]
        public void Test_query_rule_can_parse_query_with_where_clause(string query)
        {
            var parser = CreateParserForQuery(query);
            var tree = parser.query();

            var pattern = parser.CompileParseTreePattern(
                "<selectClause> <fromClause> <whereClause>",
                CmsqlParser.RULE_query);

            var match = pattern.Match(tree);
            match.Succeeded.Should().BeTrue();
        }

        [Theory]
        [InlineData("select test from start where foo = 'bar';")]
        [InlineData("select test from root where foo = 'bar';")]
        [InlineData("select test from 12345 where foo = 'bar';")]
        [InlineData("select test from 12345 where (foo = 'bar');")]
        [InlineData("select test from 12345 where foo = 'bar' and foo = 'bar';")]
        [InlineData("select test from 12345 where foo = 'bar' or foo = 'bar';")]
        [InlineData("select test from 12345 where (foo = 'bar' and foo = 'bar');")]
        [InlineData("select test from 12345 where (foo = 'bar' or foo = 'bar');")]
        [InlineData("select test from 12345 where (foo = 'bar' and foo = 'bar') or (foo = 'bar' and foo = 'bar');")]
        [InlineData("select test from 12345 where (foo = 'bar' and foo = 'bar') and (foo = 'bar' and foo = 'bar');")]
        [InlineData("select test from 12345 where ((foo = 'bar' and foo = 'bar') and (foo = 'bar' and foo = 'bar'));")]
        public void Test_query_rule_can_parse_query_with_where_clause_with_terminator(string query)
        {
            var parser = CreateParserForQuery(query);
            var tree = parser.query();

            var pattern = parser.CompileParseTreePattern(
                "<selectClause> <fromClause> <whereClause> <TERMINATOR>",
                CmsqlParser.RULE_query);

            var match = pattern.Match(tree);
            match.Succeeded.Should().BeTrue();
        }

        [Theory]
        [InlineData("test")]
        [InlineData("testPage")]
        [InlineData("TestPage")]
        [InlineData("test123")]
        [InlineData("testPage123")]
        [InlineData("TestPage123")]
        public void Test_selectClause_rule_can_parse_valid_identifier(string identifier)
        {
            var parser = CreateParserForQuery($"select {identifier}");
            var tree = parser.selectClause();

            var pattern = parser.CompileParseTreePattern(
                "<SELECT> <IDENTIFIER>",
                CmsqlParser.RULE_selectClause);

            var match = pattern.Match(tree);
            match.Succeeded.Should().BeTrue();
        }

        [Theory]
        [InlineData("(foo = 'data')")]
        [InlineData("(foo = 'bar' and foo = 'bar')")]
        [InlineData("((foo = 'bar' and foo = 'bar') or (foo = 'bar' and foo = 'bar'))")]
        [InlineData("((foo = 'bar' and foo = 'bar') and (foo = 'bar' and foo = 'bar'))")]
        public void Test_expression_rule_can_parse_parenthesized_expression(string query)
        {
            var parser = CreateParserForQuery(query);
            var tree = parser.expression();

            var pattern = parser.CompileParseTreePattern(
                "<LPAREN> <expression> <RPAREN>",
                CmsqlParser.RULE_expression);

            var match = pattern.Match(tree);
            match.Succeeded.Should().BeTrue();
        }

        [Theory]
        [InlineData("foo = 'bar' and foo = 'bar'")]
        public void Test_expression_rule_can_parse_binary_and_expression(string query)
        {
            var parser = CreateParserForQuery(query);
            var tree = parser.expression();

            var pattern = parser.CompileParseTreePattern(
                "<expression> <AND> <expression>",
                CmsqlParser.RULE_expression);

            var match = pattern.Match(tree);
            match.Succeeded.Should().BeTrue();
        }

        [Theory]
        [InlineData("foo = 'bar' or foo = 'bar'")]
        public void Test_expression_rule_can_parse_binary_or_expression(string query)
        {
            var parser = CreateParserForQuery(query);
            var tree = parser.expression();

            var pattern = parser.CompileParseTreePattern(
                "<expression> <OR> <expression>",
                CmsqlParser.RULE_expression);

            var match = pattern.Match(tree);
            match.Succeeded.Should().BeTrue();
        }

        [Theory]
        [InlineData("foo != 'bar'")]
        [InlineData("foo = 'bar'")]
        public void Test_expression_rule_can_parse_condition(string query)
        {
            var parser = CreateParserForQuery(query);
            var tree = parser.expression();

            var pattern = parser.CompileParseTreePattern(
                "<expression>",
                CmsqlParser.RULE_expression);

            var match = pattern.Match(tree);
            match.Succeeded.Should().BeTrue();
        }

        [Theory]
        [InlineData("foo = 'bar'")]
        public void Test_conditon_rule_can_parse_equals_condition(string query)
        {
            var parser = CreateParserForQuery(query);
            var tree = parser.condition();

            var pattern = parser.CompileParseTreePattern(
                "<IDENTIFIER> <EQUALS> <LITERAL>",
                CmsqlParser.RULE_condition);

            var match = pattern.Match(tree);
            match.Succeeded.Should().BeTrue();
        }

        [Theory]
        [InlineData("foo != 'bar'")]
        public void Test_conditon_rule_can_parse_not_equals_condition(string query)
        {
            var parser = CreateParserForQuery(query);
            var tree = parser.condition();

            var pattern = parser.CompileParseTreePattern(
                "<IDENTIFIER> <NOTEQUALS> <LITERAL>",
                CmsqlParser.RULE_condition);

            var match = pattern.Match(tree);
            match.Succeeded.Should().BeTrue();
        }

        private static CmsqlParser CreateParserForQuery(string query)
        {
            return new CmsqlParser(
                new CommonTokenStream(
                    new CmsqlLexer(
                        new AntlrInputStream(query))))
            {
                Interpreter = {PredictionMode = PredictionMode.SLL}
            };
        }
    }
}
