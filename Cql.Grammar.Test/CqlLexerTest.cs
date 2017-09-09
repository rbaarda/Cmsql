using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;
using FluentAssertions;
using Xunit;

namespace Cql.Grammar.Test
{
    public class CqlLexerTest
    {
        [Fact]
        public void Test_can_parse_query_from_start()
        {
            CqlLexer lexer = CreateLexerForQuery("select foo from start");
            IEnumerable<int> tokens = GetTokensAsList(lexer);

            tokens.ShouldAllBeEquivalentTo(
                GetTokensAsList(
                    CqlLexer.SELECT,
                    CqlLexer.IDENTIFIER,
                    CqlLexer.FROM,
                    CqlLexer.START));
        }

        [Fact]
        public void Test_can_parse_query_from_root()
        {
            CqlLexer lexer = CreateLexerForQuery("select foo from root");
            IEnumerable<int> tokens = GetTokensAsList(lexer);

            tokens.ShouldAllBeEquivalentTo(
                GetTokensAsList(
                    CqlLexer.SELECT,
                    CqlLexer.IDENTIFIER,
                    CqlLexer.FROM,
                    CqlLexer.ROOT));
        }

        [Fact]
        public void Test_can_parse_query_from_arbitrary()
        {
            CqlLexer lexer = CreateLexerForQuery("select foo from 15");
            IEnumerable<int> tokens = GetTokensAsList(lexer);

            tokens.ShouldAllBeEquivalentTo(
                GetTokensAsList(
                    CqlLexer.SELECT,
                    CqlLexer.IDENTIFIER,
                    CqlLexer.FROM,
                    CqlLexer.NUMBER));
        }

        [Fact]
        public void Test_can_parse_start_query()
        {
            CqlLexer lexer = CreateLexerForQuery("select foo from start");
            IEnumerable<int> tokens = GetTokensAsList(lexer);

            tokens.ShouldAllBeEquivalentTo(
                GetTokensAsList(
                    CqlLexer.SELECT,
                    CqlLexer.IDENTIFIER,
                    CqlLexer.FROM,
                    CqlLexer.START));
        }

        [Fact]
        public void Test_can_parse_query_with_terminator()
        {
            CqlLexer lexer = CreateLexerForQuery("select foo from start;");
            IEnumerable<int> tokens = GetTokensAsList(lexer);

            tokens.ShouldAllBeEquivalentTo(
                GetTokensAsList(
                    CqlLexer.SELECT,
                    CqlLexer.IDENTIFIER,
                    CqlLexer.FROM,
                    CqlLexer.START,
                    CqlLexer.TERMINATOR));
        }

        [Fact]
        public void Test_can_parse_two_queries_separated_by_terminator()
        {
            CqlLexer lexer = CreateLexerForQuery("select foo from start; select bar from root");
            IEnumerable<int> tokens = GetTokensAsList(lexer);

            tokens.ShouldAllBeEquivalentTo(
                GetTokensAsList(
                    CqlLexer.SELECT,
                    CqlLexer.IDENTIFIER,
                    CqlLexer.FROM,
                    CqlLexer.START,
                    CqlLexer.TERMINATOR,
                    CqlLexer.SELECT,
                    CqlLexer.IDENTIFIER,
                    CqlLexer.FROM,
                    CqlLexer.ROOT));
        }

        [Fact]
        public void Test_can_parse_query_with_single_condition()
        {
            CqlLexer lexer = CreateLexerForQuery("select foo from start where foo = 'bar'");
            IEnumerable<int> tokens = GetTokensAsList(lexer);

            tokens.ShouldAllBeEquivalentTo(
                GetTokensAsList(
                    CqlLexer.SELECT,
                    CqlLexer.IDENTIFIER,
                    CqlLexer.FROM,
                    CqlLexer.START,
                    CqlLexer.WHERE,
                    CqlLexer.IDENTIFIER,
                    CqlLexer.EQUALS,
                    CqlLexer.LITERAL));
        }

        [Fact]
        public void Test_can_parse_query_with_single_conditional_and_expression()
        {
            CqlLexer lexer = CreateLexerForQuery(
                "select foo from start where foo = 'bar' and bar = 'foo'");
            IEnumerable<int> tokens = GetTokensAsList(lexer);

            tokens.ShouldAllBeEquivalentTo(
                GetTokensAsList(
                    CqlLexer.SELECT,
                    CqlLexer.IDENTIFIER,
                    CqlLexer.FROM,
                    CqlLexer.START,
                    CqlLexer.WHERE,
                    CqlLexer.IDENTIFIER,
                    CqlLexer.EQUALS,
                    CqlLexer.LITERAL,
                    CqlLexer.AND,
                    CqlLexer.IDENTIFIER,
                    CqlLexer.EQUALS,
                    CqlLexer.LITERAL));
        }

        [Fact]
        public void Test_can_parse_query_with_single_conditional_or_expression()
        {
            CqlLexer lexer = CreateLexerForQuery(
                "select foo from start where foo = 'bar' or bar = 'foo'");
            IEnumerable<int> tokens = GetTokensAsList(lexer);

            tokens.ShouldAllBeEquivalentTo(
                GetTokensAsList(
                    CqlLexer.SELECT,
                    CqlLexer.IDENTIFIER,
                    CqlLexer.FROM,
                    CqlLexer.START,
                    CqlLexer.WHERE,
                    CqlLexer.IDENTIFIER,
                    CqlLexer.EQUALS,
                    CqlLexer.LITERAL,
                    CqlLexer.OR,
                    CqlLexer.IDENTIFIER,
                    CqlLexer.EQUALS,
                    CqlLexer.LITERAL));
        }

        [Fact]
        public void Test_can_parse_query_with_single_parenthesized_expression()
        {
            CqlLexer lexer = CreateLexerForQuery(
                "select foo from start where (foo = 'bar' and bar = 'foo')");
            IEnumerable<int> tokens = GetTokensAsList(lexer);

            tokens.ShouldAllBeEquivalentTo(
                GetTokensAsList(
                    CqlLexer.SELECT,
                    CqlLexer.IDENTIFIER,
                    CqlLexer.FROM,
                    CqlLexer.START,
                    CqlLexer.WHERE,
                    CqlLexer.LPAREN,
                    CqlLexer.IDENTIFIER,
                    CqlLexer.EQUALS,
                    CqlLexer.LITERAL,
                    CqlLexer.AND,
                    CqlLexer.IDENTIFIER,
                    CqlLexer.EQUALS,
                    CqlLexer.LITERAL,
                    CqlLexer.RPAREN));
        }

        [Fact]
        public void Test_can_parse_query_with_two_parenthesized_expressions()
        {
            CqlLexer lexer = CreateLexerForQuery(
                "select foo from start where (foo = 'bar' and bar = 'foo') or (bla = 'test' and test = 'bla')");
            IEnumerable<int> tokens = GetTokensAsList(lexer);

            tokens.ShouldAllBeEquivalentTo(
                GetTokensAsList(
                    CqlLexer.SELECT,
                    CqlLexer.IDENTIFIER,
                    CqlLexer.FROM,
                    CqlLexer.START,
                    CqlLexer.WHERE,
                    CqlLexer.LPAREN,
                    CqlLexer.IDENTIFIER,
                    CqlLexer.EQUALS,
                    CqlLexer.LITERAL,
                    CqlLexer.AND,
                    CqlLexer.IDENTIFIER,
                    CqlLexer.EQUALS,
                    CqlLexer.LITERAL,
                    CqlLexer.RPAREN,
                    CqlLexer.OR,
                    CqlLexer.LPAREN,
                    CqlLexer.IDENTIFIER,
                    CqlLexer.EQUALS,
                    CqlLexer.LITERAL,
                    CqlLexer.AND,
                    CqlLexer.IDENTIFIER,
                    CqlLexer.EQUALS,
                    CqlLexer.LITERAL,
                    CqlLexer.RPAREN));
        }

        private CqlLexer CreateLexerForQuery(string query)
        {
            return new CqlLexer(new AntlrInputStream(new StringReader(query)));
        }

        private IEnumerable<int> GetTokensAsList(CqlLexer lexer)
        {
            LinkedList<int> tokens = new LinkedList<int>();

            IToken token = lexer.NextToken();
            while (token.Type != -1)
            {
                tokens.AddLast(token.Type);
                token = lexer.NextToken();
            }
            return tokens;
        }

        private IEnumerable<int> GetTokensAsList(params int[] tokens)
        {
            return new LinkedList<int>(tokens);
        }
    }
}
