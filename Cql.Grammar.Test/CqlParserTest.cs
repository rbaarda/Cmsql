using Antlr4.Runtime;
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

        private CqlParser CreateParserForQuery(string query)
        {
            return new CqlParser(
                new CommonTokenStream(
                    new CqlLexer(
                        new AntlrInputStream(query))));
        }
    }
}
