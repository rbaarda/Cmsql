using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;
using FluentAssertions;
using Xunit;

namespace Cmsql.Grammar.Test
{
    public class CmsqlLexerTest
    {
        [Fact]
        public void Test_can_tokenize_query_from_start()
        {
            CmsqlLexer lexer = CreateLexerForQuery("select foo from start");
            IEnumerable<int> tokens = GetTokensAsList(lexer);

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
            CmsqlLexer lexer = CreateLexerForQuery("select foo from root");
            IEnumerable<int> tokens = GetTokensAsList(lexer);

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
            CmsqlLexer lexer = CreateLexerForQuery("select foo from 15");
            IEnumerable<int> tokens = GetTokensAsList(lexer);

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
            CmsqlLexer lexer = CreateLexerForQuery("select foo from start");
            IEnumerable<int> tokens = GetTokensAsList(lexer);

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
            CmsqlLexer lexer = CreateLexerForQuery("select foo from start;");
            IEnumerable<int> tokens = GetTokensAsList(lexer);

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
            CmsqlLexer lexer = CreateLexerForQuery("select foo from start; select bar from root");
            IEnumerable<int> tokens = GetTokensAsList(lexer);

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
            CmsqlLexer lexer = CreateLexerForQuery("select foo from start where foo = 'bar'");
            IEnumerable<int> tokens = GetTokensAsList(lexer);

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
            CmsqlLexer lexer = CreateLexerForQuery("select foo from start where foo != 'bar'");
            IEnumerable<int> tokens = GetTokensAsList(lexer);

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
            CmsqlLexer lexer = CreateLexerForQuery(
                "select foo from start where foo = 'bar' and bar = 'foo'");
            IEnumerable<int> tokens = GetTokensAsList(lexer);

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
            CmsqlLexer lexer = CreateLexerForQuery(
                "select foo from start where foo = 'bar' or bar = 'foo'");
            IEnumerable<int> tokens = GetTokensAsList(lexer);

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
            CmsqlLexer lexer = CreateLexerForQuery(
                "select foo from start where (foo = 'bar' and bar = 'foo')");
            IEnumerable<int> tokens = GetTokensAsList(lexer);

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
            CmsqlLexer lexer = CreateLexerForQuery(
                "select foo from start where (foo = 'bar' and bar = 'foo') or (bla = 'test' and test = 'bla')");
            IEnumerable<int> tokens = GetTokensAsList(lexer);

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
            CmsqlLexer lexer = CreateLexerForQuery(
                "select foo-bar from start");
            IEnumerable<int> tokens = GetTokensAsList(lexer);

            tokens.Should().BeEquivalentTo(
                GetTokensAsList(
                    CmsqlLexer.SELECT,
                    CmsqlLexer.IDENTIFIER,
                    CmsqlLexer.ERRORCHAR,
                    CmsqlLexer.IDENTIFIER,
                    CmsqlLexer.FROM,
                    CmsqlLexer.START));
        }

        private CmsqlLexer CreateLexerForQuery(string query)
        {
            return new CmsqlLexer(new AntlrInputStream(new StringReader(query)));
        }

        private IEnumerable<int> GetTokensAsList(CmsqlLexer lexer)
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
