// Generated from ..\Cql.g4 by ANTLR 4.7
import org.antlr.v4.runtime.Lexer;
import org.antlr.v4.runtime.CharStream;
import org.antlr.v4.runtime.Token;
import org.antlr.v4.runtime.TokenStream;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.misc.*;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class CqlLexer extends Lexer {
	static { RuntimeMetaData.checkVersion("4.7", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		SELECT=1, FROM=2, WHERE=3, OR=4, AND=5, NUMBER=6, START=7, ROOT=8, LITERAL=9, 
		IDENTIFIER=10, LPAREN=11, RPAREN=12, TERMINATOR=13, EQUALS=14, NOTEQUALS=15, 
		GREATERTHAN=16, LESSTHAN=17, GREATERTHANOREQUALS=18, LESSTHANOREQUALS=19, 
		WHITESPACE=20, ERRORCHAR=21;
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	public static final String[] ruleNames = {
		"SELECT", "FROM", "WHERE", "OR", "AND", "NUMBER", "START", "ROOT", "LITERAL", 
		"IDENTIFIER", "LPAREN", "RPAREN", "TERMINATOR", "EQUALS", "NOTEQUALS", 
		"GREATERTHAN", "LESSTHAN", "GREATERTHANOREQUALS", "LESSTHANOREQUALS", 
		"WHITESPACE", "ERRORCHAR", "S", "E", "L", "C", "T", "F", "R", "O", "M", 
		"P", "A", "G", "W", "H", "N", "D"
	};

	private static final String[] _LITERAL_NAMES = {
		null, null, null, null, null, null, null, null, null, null, null, "'('", 
		"')'", "';'", "'='", "'!='", "'>'", "'<'", "'>='", "'<='"
	};
	private static final String[] _SYMBOLIC_NAMES = {
		null, "SELECT", "FROM", "WHERE", "OR", "AND", "NUMBER", "START", "ROOT", 
		"LITERAL", "IDENTIFIER", "LPAREN", "RPAREN", "TERMINATOR", "EQUALS", "NOTEQUALS", 
		"GREATERTHAN", "LESSTHAN", "GREATERTHANOREQUALS", "LESSTHANOREQUALS", 
		"WHITESPACE", "ERRORCHAR"
	};
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}


	public CqlLexer(CharStream input) {
		super(input);
		_interp = new LexerATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@Override
	public String getGrammarFileName() { return "Cql.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public String[] getChannelNames() { return channelNames; }

	@Override
	public String[] getModeNames() { return modeNames; }

	@Override
	public ATN getATN() { return _ATN; }

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2\27\u00c7\b\1\4\2"+
		"\t\2\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4"+
		"\13\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22"+
		"\t\22\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31"+
		"\t\31\4\32\t\32\4\33\t\33\4\34\t\34\4\35\t\35\4\36\t\36\4\37\t\37\4 \t"+
		" \4!\t!\4\"\t\"\4#\t#\4$\t$\4%\t%\4&\t&\3\2\3\2\3\2\3\2\3\2\3\2\3\2\3"+
		"\3\3\3\3\3\3\3\3\3\3\4\3\4\3\4\3\4\3\4\3\4\3\5\3\5\3\5\3\6\3\6\3\6\3\6"+
		"\3\7\6\7h\n\7\r\7\16\7i\3\b\3\b\3\b\3\b\3\b\3\b\3\t\3\t\3\t\3\t\3\t\3"+
		"\n\3\n\6\ny\n\n\r\n\16\nz\3\n\3\n\3\13\6\13\u0080\n\13\r\13\16\13\u0081"+
		"\3\13\7\13\u0085\n\13\f\13\16\13\u0088\13\13\3\f\3\f\3\r\3\r\3\16\3\16"+
		"\3\17\3\17\3\20\3\20\3\20\3\21\3\21\3\22\3\22\3\23\3\23\3\23\3\24\3\24"+
		"\3\24\3\25\6\25\u00a0\n\25\r\25\16\25\u00a1\3\25\3\25\3\26\3\26\3\27\3"+
		"\27\3\30\3\30\3\31\3\31\3\32\3\32\3\33\3\33\3\34\3\34\3\35\3\35\3\36\3"+
		"\36\3\37\3\37\3 \3 \3!\3!\3\"\3\"\3#\3#\3$\3$\3%\3%\3&\3&\2\2\'\3\3\5"+
		"\4\7\5\t\6\13\7\r\b\17\t\21\n\23\13\25\f\27\r\31\16\33\17\35\20\37\21"+
		"!\22#\23%\24\'\25)\26+\27-\2/\2\61\2\63\2\65\2\67\29\2;\2=\2?\2A\2C\2"+
		"E\2G\2I\2K\2\3\2\27\3\2\62;\b\2\"\"//\62;C\\aac|\4\2C\\c|\5\2\62;C\\c"+
		"|\5\2\13\f\17\17\"\"\4\2UUuu\4\2GGgg\4\2NNnn\4\2EEee\4\2VVvv\4\2HHhh\4"+
		"\2TTtt\4\2QQqq\4\2OOoo\4\2RRrr\4\2CCcc\4\2IIii\4\2YYyy\4\2JJjj\4\2PPp"+
		"p\4\2FFff\2\u00bb\2\3\3\2\2\2\2\5\3\2\2\2\2\7\3\2\2\2\2\t\3\2\2\2\2\13"+
		"\3\2\2\2\2\r\3\2\2\2\2\17\3\2\2\2\2\21\3\2\2\2\2\23\3\2\2\2\2\25\3\2\2"+
		"\2\2\27\3\2\2\2\2\31\3\2\2\2\2\33\3\2\2\2\2\35\3\2\2\2\2\37\3\2\2\2\2"+
		"!\3\2\2\2\2#\3\2\2\2\2%\3\2\2\2\2\'\3\2\2\2\2)\3\2\2\2\2+\3\2\2\2\3M\3"+
		"\2\2\2\5T\3\2\2\2\7Y\3\2\2\2\t_\3\2\2\2\13b\3\2\2\2\rg\3\2\2\2\17k\3\2"+
		"\2\2\21q\3\2\2\2\23v\3\2\2\2\25\177\3\2\2\2\27\u0089\3\2\2\2\31\u008b"+
		"\3\2\2\2\33\u008d\3\2\2\2\35\u008f\3\2\2\2\37\u0091\3\2\2\2!\u0094\3\2"+
		"\2\2#\u0096\3\2\2\2%\u0098\3\2\2\2\'\u009b\3\2\2\2)\u009f\3\2\2\2+\u00a5"+
		"\3\2\2\2-\u00a7\3\2\2\2/\u00a9\3\2\2\2\61\u00ab\3\2\2\2\63\u00ad\3\2\2"+
		"\2\65\u00af\3\2\2\2\67\u00b1\3\2\2\29\u00b3\3\2\2\2;\u00b5\3\2\2\2=\u00b7"+
		"\3\2\2\2?\u00b9\3\2\2\2A\u00bb\3\2\2\2C\u00bd\3\2\2\2E\u00bf\3\2\2\2G"+
		"\u00c1\3\2\2\2I\u00c3\3\2\2\2K\u00c5\3\2\2\2MN\5-\27\2NO\5/\30\2OP\5\61"+
		"\31\2PQ\5/\30\2QR\5\63\32\2RS\5\65\33\2S\4\3\2\2\2TU\5\67\34\2UV\59\35"+
		"\2VW\5;\36\2WX\5=\37\2X\6\3\2\2\2YZ\5E#\2Z[\5G$\2[\\\5/\30\2\\]\59\35"+
		"\2]^\5/\30\2^\b\3\2\2\2_`\5;\36\2`a\59\35\2a\n\3\2\2\2bc\5A!\2cd\5I%\2"+
		"de\5K&\2e\f\3\2\2\2fh\t\2\2\2gf\3\2\2\2hi\3\2\2\2ig\3\2\2\2ij\3\2\2\2"+
		"j\16\3\2\2\2kl\5-\27\2lm\5\65\33\2mn\5A!\2no\59\35\2op\5\65\33\2p\20\3"+
		"\2\2\2qr\59\35\2rs\5;\36\2st\5;\36\2tu\5\65\33\2u\22\3\2\2\2vx\7)\2\2"+
		"wy\t\3\2\2xw\3\2\2\2yz\3\2\2\2zx\3\2\2\2z{\3\2\2\2{|\3\2\2\2|}\7)\2\2"+
		"}\24\3\2\2\2~\u0080\t\4\2\2\177~\3\2\2\2\u0080\u0081\3\2\2\2\u0081\177"+
		"\3\2\2\2\u0081\u0082\3\2\2\2\u0082\u0086\3\2\2\2\u0083\u0085\t\5\2\2\u0084"+
		"\u0083\3\2\2\2\u0085\u0088\3\2\2\2\u0086\u0084\3\2\2\2\u0086\u0087\3\2"+
		"\2\2\u0087\26\3\2\2\2\u0088\u0086\3\2\2\2\u0089\u008a\7*\2\2\u008a\30"+
		"\3\2\2\2\u008b\u008c\7+\2\2\u008c\32\3\2\2\2\u008d\u008e\7=\2\2\u008e"+
		"\34\3\2\2\2\u008f\u0090\7?\2\2\u0090\36\3\2\2\2\u0091\u0092\7#\2\2\u0092"+
		"\u0093\7?\2\2\u0093 \3\2\2\2\u0094\u0095\7@\2\2\u0095\"\3\2\2\2\u0096"+
		"\u0097\7>\2\2\u0097$\3\2\2\2\u0098\u0099\7@\2\2\u0099\u009a\7?\2\2\u009a"+
		"&\3\2\2\2\u009b\u009c\7>\2\2\u009c\u009d\7?\2\2\u009d(\3\2\2\2\u009e\u00a0"+
		"\t\6\2\2\u009f\u009e\3\2\2\2\u00a0\u00a1\3\2\2\2\u00a1\u009f\3\2\2\2\u00a1"+
		"\u00a2\3\2\2\2\u00a2\u00a3\3\2\2\2\u00a3\u00a4\b\25\2\2\u00a4*\3\2\2\2"+
		"\u00a5\u00a6\13\2\2\2\u00a6,\3\2\2\2\u00a7\u00a8\t\7\2\2\u00a8.\3\2\2"+
		"\2\u00a9\u00aa\t\b\2\2\u00aa\60\3\2\2\2\u00ab\u00ac\t\t\2\2\u00ac\62\3"+
		"\2\2\2\u00ad\u00ae\t\n\2\2\u00ae\64\3\2\2\2\u00af\u00b0\t\13\2\2\u00b0"+
		"\66\3\2\2\2\u00b1\u00b2\t\f\2\2\u00b28\3\2\2\2\u00b3\u00b4\t\r\2\2\u00b4"+
		":\3\2\2\2\u00b5\u00b6\t\16\2\2\u00b6<\3\2\2\2\u00b7\u00b8\t\17\2\2\u00b8"+
		">\3\2\2\2\u00b9\u00ba\t\20\2\2\u00ba@\3\2\2\2\u00bb\u00bc\t\21\2\2\u00bc"+
		"B\3\2\2\2\u00bd\u00be\t\22\2\2\u00beD\3\2\2\2\u00bf\u00c0\t\23\2\2\u00c0"+
		"F\3\2\2\2\u00c1\u00c2\t\24\2\2\u00c2H\3\2\2\2\u00c3\u00c4\t\25\2\2\u00c4"+
		"J\3\2\2\2\u00c5\u00c6\t\26\2\2\u00c6L\3\2\2\2\t\2ixz\u0081\u0086\u00a1"+
		"\3\b\2\2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}