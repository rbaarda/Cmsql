using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;
using Cmsql.Grammar;
using FluentAssertions;
using Xunit;

namespace Cmsql.Test.Grammar
{
    public class CmsqlLexerTest
    {
        [Fact]
        public void Test_can_tokenize_query_from_start()
        {
            var lexer = CreateLexerForQuery("select foo from start");
            var tokens = GetTokensAsList(lexer);

            tokens.Should().BeEquivalentTo(
                GetTokensAsList(
                    CmsqlLexer.SELECT,
                    CmsqlLexer.IDENTIFIER,
                    CmsqlLexer.FROM,
                    CmsqlLexer.START));
        }

        [Fact]
        public void Test_can_tokenize_query_from_root()
        {
            var lexer = CreateLexerForQuery("select foo from root");
            var tokens = GetTokensAsList(lexer);

            tokens.Should().BeEquivalentTo(
                GetTokensAsList(
                    CmsqlLexer.SELECT,
                    CmsqlLexer.IDENTIFIER,
                    CmsqlLexer.FROM,
                    CmsqlLexer.ROOT));
        }

        [Fact]
        public void Test_can_tokenize_query_from_arbitrary()
        {
            var lexer = CreateLexerForQuery("select foo from 15");
            var tokens = GetTokensAsList(lexer);

            tokens.Should().BeEquivalentTo(
                GetTokensAsList(
                    CmsqlLexer.SELECT,
                    CmsqlLexer.IDENTIFIER,
                    CmsqlLexer.FROM,
                    CmsqlLexer.NUMBER));
        }

        [Fact]
        public void Test_can_tokenize_start_query()
        {
            var lexer = CreateLexerForQuery("select foo from start");
            var tokens = GetTokensAsList(lexer);

            tokens.Should().BeEquivalentTo(
                GetTokensAsList(
                    CmsqlLexer.SELECT,
                    CmsqlLexer.IDENTIFIER,
                    CmsqlLexer.FROM,
                    CmsqlLexer.START));
        }

        [Fact]
        public void Test_can_tokenize_query_with_terminator()
        {
            var lexer = CreateLexerForQuery("select foo from start;");
            var tokens = GetTokensAsList(lexer);

            tokens.Should().BeEquivalentTo(
                GetTokensAsList(
                    CmsqlLexer.SELECT,
                    CmsqlLexer.IDENTIFIER,
                    CmsqlLexer.FROM,
                    CmsqlLexer.START,
                    CmsqlLexer.TERMINATOR));
        }

        [Fact]
        public void Test_can_tokenize_two_queries_separated_by_terminator()
        {
            var lexer = CreateLexerForQuery("select foo from start; select bar from root");
            var tokens = GetTokensAsList(lexer);

            tokens.Should().BeEquivalentTo(
                GetTokensAsList(
                    CmsqlLexer.SELECT,
                    CmsqlLexer.IDENTIFIER,
                    CmsqlLexer.FROM,
                    CmsqlLexer.START,
                    CmsqlLexer.TERMINATOR,
                    CmsqlLexer.SELECT,
                    CmsqlLexer.IDENTIFIER,
                    CmsqlLexer.FROM,
                    CmsqlLexer.ROOT));
        }

        [Fact]
        public void Test_can_tokenize_query_with_single_equals_condition()
        {
            var lexer = CreateLexerForQuery("select foo from start where foo = 'bar'");
            var tokens = GetTokensAsList(lexer);

            tokens.Should().BeEquivalentTo(
                GetTokensAsList(
                    CmsqlLexer.SELECT,
                    CmsqlLexer.IDENTIFIER,
                    CmsqlLexer.FROM,
                    CmsqlLexer.START,
                    CmsqlLexer.WHERE,
                    CmsqlLexer.IDENTIFIER,
                    CmsqlLexer.EQUALS,
                    CmsqlLexer.LITERAL));
        }

        [Fact]
        public void Test_can_tokenize_query_with_single_not_equals_condition()
        {
            var lexer = CreateLexerForQuery("select foo from start where foo != 'bar'");
            var tokens = GetTokensAsList(lexer);

            tokens.Should().BeEquivalentTo(
                GetTokensAsList(
                    CmsqlLexer.SELECT,
                    CmsqlLexer.IDENTIFIER,
                    CmsqlLexer.FROM,
                    CmsqlLexer.START,
                    CmsqlLexer.WHERE,
                    CmsqlLexer.IDENTIFIER,
                    CmsqlLexer.NOTEQUALS,
                    CmsqlLexer.LITERAL));
        }

        [Fact]
        public void Test_can_tokenize_query_with_single_conditional_and_expression()
        {
            var lexer = CreateLexerForQuery(
                "select foo from start where foo = 'bar' and bar = 'foo'");
            var tokens = GetTokensAsList(lexer);

            tokens.Should().BeEquivalentTo(
                GetTokensAsList(
                    CmsqlLexer.SELECT,
                    CmsqlLexer.IDENTIFIER,
                    CmsqlLexer.FROM,
                    CmsqlLexer.START,
                    CmsqlLexer.WHERE,
                    CmsqlLexer.IDENTIFIER,
                    CmsqlLexer.EQUALS,
                    CmsqlLexer.LITERAL,
                    CmsqlLexer.AND,
                    CmsqlLexer.IDENTIFIER,
                    CmsqlLexer.EQUALS,
                    CmsqlLexer.LITERAL));
        }

        [Fact]
        public void Test_can_tokenize_query_with_single_conditional_or_expression()
        {
            var lexer = CreateLexerForQuery(
                "select foo from start where foo = 'bar' or bar = 'foo'");
            var tokens = GetTokensAsList(lexer);

            tokens.Should().BeEquivalentTo(
                GetTokensAsList(
                    CmsqlLexer.SELECT,
                    CmsqlLexer.IDENTIFIER,
                    CmsqlLexer.FROM,
                    CmsqlLexer.START,
                    CmsqlLexer.WHERE,
                    CmsqlLexer.IDENTIFIER,
                    CmsqlLexer.EQUALS,
                    CmsqlLexer.LITERAL,
                    CmsqlLexer.OR,
                    CmsqlLexer.IDENTIFIER,
                    CmsqlLexer.EQUALS,
                    CmsqlLexer.LITERAL));
        }

        [Fact]
        public void Test_can_tokenize_query_with_single_parenthesized_expression()
        {
            var lexer = CreateLexerForQuery(
                "select foo from start where (foo = 'bar' and bar = 'foo')");
            var tokens = GetTokensAsList(lexer);

            tokens.Should().BeEquivalentTo(
                GetTokensAsList(
                    CmsqlLexer.SELECT,
                    CmsqlLexer.IDENTIFIER,
                    CmsqlLexer.FROM,
                    CmsqlLexer.START,
                    CmsqlLexer.WHERE,
                    CmsqlLexer.LPAREN,
                    CmsqlLexer.IDENTIFIER,
                    CmsqlLexer.EQUALS,
                    CmsqlLexer.LITERAL,
                    CmsqlLexer.AND,
                    CmsqlLexer.IDENTIFIER,
                    CmsqlLexer.EQUALS,
                    CmsqlLexer.LITERAL,
                    CmsqlLexer.RPAREN));
        }

        [Fact]
        public void Test_can_tokenize_query_with_two_parenthesized_expressions()
        {
            var lexer = CreateLexerForQuery(
                "select foo from start where (foo = 'bar' and bar = 'foo') or (bla = 'test' and test = 'bla')");
            var tokens = GetTokensAsList(lexer);

            tokens.Should().BeEquivalentTo(
                GetTokensAsList(
                    CmsqlLexer.SELECT,
                    CmsqlLexer.IDENTIFIER,
                    CmsqlLexer.FROM,
                    CmsqlLexer.START,
                    CmsqlLexer.WHERE,
                    CmsqlLexer.LPAREN,
                    CmsqlLexer.IDENTIFIER,
                    CmsqlLexer.EQUALS,
                    CmsqlLexer.LITERAL,
                    CmsqlLexer.AND,
                    CmsqlLexer.IDENTIFIER,
                    CmsqlLexer.EQUALS,
                    CmsqlLexer.LITERAL,
                    CmsqlLexer.RPAREN,
                    CmsqlLexer.OR,
                    CmsqlLexer.LPAREN,
                    CmsqlLexer.IDENTIFIER,
                    CmsqlLexer.EQUALS,
                    CmsqlLexer.LITERAL,
                    CmsqlLexer.AND,
                    CmsqlLexer.IDENTIFIER,
                    CmsqlLexer.EQUALS,
                    CmsqlLexer.LITERAL,
                    CmsqlLexer.RPAREN));
        }

        [Fact]
        public void Test_can_tokenize_query_with_invalid_type_identifier()
        {
            var lexer = CreateLexerForQuery(
                "select foo-bar from start");
            var tokens = GetTokensAsList(lexer);

            tokens.Should().BeEquivalentTo(
                GetTokensAsList(
                    CmsqlLexer.SELECT,
                    CmsqlLexer.IDENTIFIER,
                    CmsqlLexer.ERRORCHAR,
                    CmsqlLexer.IDENTIFIER,
                    CmsqlLexer.FROM,
                    CmsqlLexer.START));
        }

        private static CmsqlLexer CreateLexerForQuery(string query)
        {
            return new CmsqlLexer(new AntlrInputStream(new StringReader(query)));
        }

        private static LinkedList<int> GetTokensAsList(CmsqlLexer lexer)
        {
            var tokens = new LinkedList<int>();

            var token = lexer.NextToken();
            while (token.Type != -1)
            {
                tokens.AddLast(token.Type);
                token = lexer.NextToken();
            }

            return tokens;
        }

        private static LinkedList<int> GetTokensAsList(params int[] tokens)
        {
            return new LinkedList<int>(tokens);
        }
    }
}
