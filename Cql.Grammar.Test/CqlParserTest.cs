using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Antlr4.Runtime.Tree.Pattern;
using FluentAssertions;
using Xunit;

namespace Cql.Grammar.Test
{
    public class CqlParserTest
    {
        [Theory]
        [InlineData("select test from start;select test from root;select test from 123 where bla = 'foo'")]
        public void Test_queries_rule_can_parse_multiple(string query)
        {
            CqlParser parser = CreateParserForQuery(query);
            IParseTree tree = parser.queries();

            ParseTreePattern pattern = parser.CompileParseTreePattern(
                "<query> <TERMINATOR> <query> <TERMINATOR> <query> <EOF>",
                CqlParser.RULE_queries);
            ParseTreeMatch match = pattern.Match(tree);

            match.Succeeded.Should().BeTrue();
        }

        [Theory]
        [InlineData("This is bogus")]
        [InlineData("123select test from start")]
        [InlineData("select123 test from start")]
        [InlineData("select test,test from start")]
        [InlineData("select test-test from start")]
        [InlineData("select test_test from start")]
        [InlineData("select test from foo")]
        [InlineData("select test from start where")]
        [InlineData("select test from start where 123foo = 'bar'")]
        [InlineData("select test from start where foo = bar")]
        [InlineData("select test from start where (foo = 'bar') bla (bar = 'foo')")]
        [InlineData("select test from start where (foo = 'bar'")]
        [InlineData("select test from start; select")]
        [InlineData("select; select test from start")]
        public void Test_queries_rule_cannot_parse_invalid_query(string query)
        {
            CqlParser parser = CreateParserForQuery(query);
            parser.Invoking(x => x.queries()).ShouldThrow<ParseCanceledException>();
        }

        [Theory]
        [InlineData("select test from start")]
        [InlineData("select test from root")]
        [InlineData("select test from 12345")]
        public void Test_query_rule_can_parse_query_without_where_clause(string query)
        {
            CqlParser parser = CreateParserForQuery(query);
            IParseTree tree = parser.query();

            ParseTreePattern pattern = parser.CompileParseTreePattern(
                "<selectClause> <fromClause>",
                CqlParser.RULE_query);

            ParseTreeMatch match = pattern.Match(tree);
            match.Succeeded.Should().BeTrue();
        }

        [Theory]
        [InlineData("select test from start;")]
        [InlineData("select test from root;")]
        [InlineData("select test from 12345;")]
        public void Test_query_rule_can_parse_query_without_where_clause_with_terminator(string query)
        {
            CqlParser parser = CreateParserForQuery(query);
            IParseTree tree = parser.query();

            ParseTreePattern pattern = parser.CompileParseTreePattern(
                "<selectClause> <fromClause> <TERMINATOR>",
                CqlParser.RULE_query);

            ParseTreeMatch match = pattern.Match(tree);
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
            CqlParser parser = CreateParserForQuery(query);
            IParseTree tree = parser.query();

            ParseTreePattern pattern = parser.CompileParseTreePattern(
                "<selectClause> <fromClause> <whereClause>",
                CqlParser.RULE_query);

            ParseTreeMatch match = pattern.Match(tree);
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
            CqlParser parser = CreateParserForQuery(query);
            IParseTree tree = parser.query();

            ParseTreePattern pattern = parser.CompileParseTreePattern(
                "<selectClause> <fromClause> <whereClause> <TERMINATOR>",
                CqlParser.RULE_query);

            ParseTreeMatch match = pattern.Match(tree);
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
            CqlParser parser = CreateParserForQuery($"select {identifier}");
            IParseTree tree = parser.selectClause();

            ParseTreePattern pattern = parser.CompileParseTreePattern(
                "<SELECT> <IDENTIFIER>",
                CqlParser.RULE_selectClause);

            ParseTreeMatch match = pattern.Match(tree);
            match.Succeeded.Should().BeTrue();
        }

        [Theory]
        [InlineData("123")]
        [InlineData("123test")]
        [InlineData("-test")]
        [InlineData("_test")]
        [InlineData("!test")]
        public void Test_selectClause_rule_cannot_parse_invalid_identifier(string identifier)
        {
            CqlParser parser = CreateParserForQuery($"select {identifier}");
            parser.Invoking(x => x.selectClause()).ShouldThrow<ParseCanceledException>();
        }

        [Theory]
        [InlineData("(foo = 'data')")]
        [InlineData("(foo = 'bar' and foo = 'bar')")]
        [InlineData("((foo = 'bar' and foo = 'bar') or (foo = 'bar' and foo = 'bar'))")]
        [InlineData("((foo = 'bar' and foo = 'bar') and (foo = 'bar' and foo = 'bar'))")]
        public void Test_expression_rule_can_parse_parenthesized_expression(string query)
        {
            CqlParser parser = CreateParserForQuery(query);
            IParseTree tree = parser.expression();

            ParseTreePattern pattern = parser.CompileParseTreePattern(
                "<LPAREN> <expression> <RPAREN>",
                CqlParser.RULE_expression);

            ParseTreeMatch match = pattern.Match(tree);
            match.Succeeded.Should().BeTrue();
        }

        [Theory]
        [InlineData("foo = 'bar' and foo = 'bar'")]
        public void Test_expression_rule_can_parse_binary_and_expression(string query)
        {
            CqlParser parser = CreateParserForQuery(query);
            IParseTree tree = parser.expression();

            ParseTreePattern pattern = parser.CompileParseTreePattern(
                "<expression> <AND> <expression>",
                CqlParser.RULE_expression);

            ParseTreeMatch match = pattern.Match(tree);
            match.Succeeded.Should().BeTrue();
        }

        [Theory]
        [InlineData("foo = 'bar' or foo = 'bar'")]
        public void Test_expression_rule_can_parse_binary_or_expression(string query)
        {
            CqlParser parser = CreateParserForQuery(query);
            IParseTree tree = parser.expression();

            ParseTreePattern pattern = parser.CompileParseTreePattern(
                "<expression> <OR> <expression>",
                CqlParser.RULE_expression);

            ParseTreeMatch match = pattern.Match(tree);
            match.Succeeded.Should().BeTrue();
        }

        [Theory]
        [InlineData("foo != 'bar'")]
        [InlineData("foo = 'bar'")]
        public void Test_expression_rule_can_parse_condition(string query)
        {
            CqlParser parser = CreateParserForQuery(query);
            IParseTree tree = parser.expression();

            ParseTreePattern pattern = parser.CompileParseTreePattern(
                "<expression>",
                CqlParser.RULE_expression);

            ParseTreeMatch match = pattern.Match(tree);
            match.Succeeded.Should().BeTrue();
        }

        [Theory]
        [InlineData("foo = 'bar'")]
        public void Test_conditon_rule_can_parse_equals_condition(string query)
        {
            CqlParser parser = CreateParserForQuery(query);
            IParseTree tree = parser.condition();

            ParseTreePattern pattern = parser.CompileParseTreePattern(
                "<IDENTIFIER> <EQUALS> <LITERAL>",
                CqlParser.RULE_condition);

            ParseTreeMatch match = pattern.Match(tree);
            match.Succeeded.Should().BeTrue();
        }

        [Theory]
        [InlineData("foo != 'bar'")]
        public void Test_conditon_rule_can_parse_not_equals_condition(string query)
        {
            CqlParser parser = CreateParserForQuery(query);
            IParseTree tree = parser.condition();

            ParseTreePattern pattern = parser.CompileParseTreePattern(
                "<IDENTIFIER> <NOTEQUALS> <LITERAL>",
                CqlParser.RULE_condition);

            ParseTreeMatch match = pattern.Match(tree);
            match.Succeeded.Should().BeTrue();
        }

        [Theory]
        [InlineData("123foo = 'bar'")]
        [InlineData("-foo = 'bar'")]
        [InlineData("_foo = 'bar'")]
        [InlineData("!foo = 'bar'")]
        public void Test_condition_rule_cannot_parse_invalid_identifier(string condition)
        {
            CqlParser parser = CreateParserForQuery(condition);
            parser.Invoking(x => x.condition()).ShouldThrow<ParseCanceledException>();
        }

        private CqlParser CreateParserForQuery(string query)
        {
            return new CqlParser(
                new CommonTokenStream(
                    new CqlLexer(
                        new AntlrInputStream(query))))
            {
                ErrorHandler = new BailErrorStrategy(),
                Interpreter = {PredictionMode = PredictionMode.Sll}
            };
        }
    }
}
