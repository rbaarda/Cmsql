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
		WHITESPACE=16, ERRORCHAR=17;
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	public static final String[] ruleNames = {
		"SELECT", "FROM", "WHERE", "OR", "AND", "NUMBER", "START", "ROOT", "LITERAL", 
		"IDENTIFIER", "LPAREN", "RPAREN", "TERMINATOR", "EQUALS", "NOTEQUALS", 
		"WHITESPACE", "ERRORCHAR", "S", "E", "L", "C", "T", "F", "R", "O", "M", 
		"P", "A", "G", "W", "H", "N", "D"
	};

	private static final String[] _LITERAL_NAMES = {
		null, null, null, null, null, null, null, null, null, null, null, "'('", 
		"')'", "';'", "'='", "'!='"
	};
	private static final String[] _SYMBOLIC_NAMES = {
		null, "SELECT", "FROM", "WHERE", "OR", "AND", "NUMBER", "START", "ROOT", 
		"LITERAL", "IDENTIFIER", "LPAREN", "RPAREN", "TERMINATOR", "EQUALS", "NOTEQUALS", 
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
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2\23\u00b5\b\1\4\2"+
		"\t\2\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4"+
		"\13\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22"+
		"\t\22\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31"+
		"\t\31\4\32\t\32\4\33\t\33\4\34\t\34\4\35\t\35\4\36\t\36\4\37\t\37\4 \t"+
		" \4!\t!\4\"\t\"\3\2\3\2\3\2\3\2\3\2\3\2\3\2\3\3\3\3\3\3\3\3\3\3\3\4\3"+
		"\4\3\4\3\4\3\4\3\4\3\5\3\5\3\5\3\6\3\6\3\6\3\6\3\7\6\7`\n\7\r\7\16\7a"+
		"\3\b\3\b\3\b\3\b\3\b\3\b\3\t\3\t\3\t\3\t\3\t\3\n\3\n\6\nq\n\n\r\n\16\n"+
		"r\3\n\3\n\3\13\6\13x\n\13\r\13\16\13y\3\13\7\13}\n\13\f\13\16\13\u0080"+
		"\13\13\3\f\3\f\3\r\3\r\3\16\3\16\3\17\3\17\3\20\3\20\3\20\3\21\6\21\u008e"+
		"\n\21\r\21\16\21\u008f\3\21\3\21\3\22\3\22\3\23\3\23\3\24\3\24\3\25\3"+
		"\25\3\26\3\26\3\27\3\27\3\30\3\30\3\31\3\31\3\32\3\32\3\33\3\33\3\34\3"+
		"\34\3\35\3\35\3\36\3\36\3\37\3\37\3 \3 \3!\3!\3\"\3\"\2\2#\3\3\5\4\7\5"+
		"\t\6\13\7\r\b\17\t\21\n\23\13\25\f\27\r\31\16\33\17\35\20\37\21!\22#\23"+
		"%\2\'\2)\2+\2-\2/\2\61\2\63\2\65\2\67\29\2;\2=\2?\2A\2C\2\3\2\27\3\2\62"+
		";\b\2\"\"//\62;C\\aac|\4\2C\\c|\5\2\62;C\\c|\5\2\13\f\17\17\"\"\4\2UU"+
		"uu\4\2GGgg\4\2NNnn\4\2EEee\4\2VVvv\4\2HHhh\4\2TTtt\4\2QQqq\4\2OOoo\4\2"+
		"RRrr\4\2CCcc\4\2IIii\4\2YYyy\4\2JJjj\4\2PPpp\4\2FFff\2\u00a9\2\3\3\2\2"+
		"\2\2\5\3\2\2\2\2\7\3\2\2\2\2\t\3\2\2\2\2\13\3\2\2\2\2\r\3\2\2\2\2\17\3"+
		"\2\2\2\2\21\3\2\2\2\2\23\3\2\2\2\2\25\3\2\2\2\2\27\3\2\2\2\2\31\3\2\2"+
		"\2\2\33\3\2\2\2\2\35\3\2\2\2\2\37\3\2\2\2\2!\3\2\2\2\2#\3\2\2\2\3E\3\2"+
		"\2\2\5L\3\2\2\2\7Q\3\2\2\2\tW\3\2\2\2\13Z\3\2\2\2\r_\3\2\2\2\17c\3\2\2"+
		"\2\21i\3\2\2\2\23n\3\2\2\2\25w\3\2\2\2\27\u0081\3\2\2\2\31\u0083\3\2\2"+
		"\2\33\u0085\3\2\2\2\35\u0087\3\2\2\2\37\u0089\3\2\2\2!\u008d\3\2\2\2#"+
		"\u0093\3\2\2\2%\u0095\3\2\2\2\'\u0097\3\2\2\2)\u0099\3\2\2\2+\u009b\3"+
		"\2\2\2-\u009d\3\2\2\2/\u009f\3\2\2\2\61\u00a1\3\2\2\2\63\u00a3\3\2\2\2"+
		"\65\u00a5\3\2\2\2\67\u00a7\3\2\2\29\u00a9\3\2\2\2;\u00ab\3\2\2\2=\u00ad"+
		"\3\2\2\2?\u00af\3\2\2\2A\u00b1\3\2\2\2C\u00b3\3\2\2\2EF\5%\23\2FG\5\'"+
		"\24\2GH\5)\25\2HI\5\'\24\2IJ\5+\26\2JK\5-\27\2K\4\3\2\2\2LM\5/\30\2MN"+
		"\5\61\31\2NO\5\63\32\2OP\5\65\33\2P\6\3\2\2\2QR\5=\37\2RS\5? \2ST\5\'"+
		"\24\2TU\5\61\31\2UV\5\'\24\2V\b\3\2\2\2WX\5\63\32\2XY\5\61\31\2Y\n\3\2"+
		"\2\2Z[\59\35\2[\\\5A!\2\\]\5C\"\2]\f\3\2\2\2^`\t\2\2\2_^\3\2\2\2`a\3\2"+
		"\2\2a_\3\2\2\2ab\3\2\2\2b\16\3\2\2\2cd\5%\23\2de\5-\27\2ef\59\35\2fg\5"+
		"\61\31\2gh\5-\27\2h\20\3\2\2\2ij\5\61\31\2jk\5\63\32\2kl\5\63\32\2lm\5"+
		"-\27\2m\22\3\2\2\2np\7)\2\2oq\t\3\2\2po\3\2\2\2qr\3\2\2\2rp\3\2\2\2rs"+
		"\3\2\2\2st\3\2\2\2tu\7)\2\2u\24\3\2\2\2vx\t\4\2\2wv\3\2\2\2xy\3\2\2\2"+
		"yw\3\2\2\2yz\3\2\2\2z~\3\2\2\2{}\t\5\2\2|{\3\2\2\2}\u0080\3\2\2\2~|\3"+
		"\2\2\2~\177\3\2\2\2\177\26\3\2\2\2\u0080~\3\2\2\2\u0081\u0082\7*\2\2\u0082"+
		"\30\3\2\2\2\u0083\u0084\7+\2\2\u0084\32\3\2\2\2\u0085\u0086\7=\2\2\u0086"+
		"\34\3\2\2\2\u0087\u0088\7?\2\2\u0088\36\3\2\2\2\u0089\u008a\7#\2\2\u008a"+
		"\u008b\7?\2\2\u008b \3\2\2\2\u008c\u008e\t\6\2\2\u008d\u008c\3\2\2\2\u008e"+
		"\u008f\3\2\2\2\u008f\u008d\3\2\2\2\u008f\u0090\3\2\2\2\u0090\u0091\3\2"+
		"\2\2\u0091\u0092\b\21\2\2\u0092\"\3\2\2\2\u0093\u0094\13\2\2\2\u0094$"+
		"\3\2\2\2\u0095\u0096\t\7\2\2\u0096&\3\2\2\2\u0097\u0098\t\b\2\2\u0098"+
		"(\3\2\2\2\u0099\u009a\t\t\2\2\u009a*\3\2\2\2\u009b\u009c\t\n\2\2\u009c"+
		",\3\2\2\2\u009d\u009e\t\13\2\2\u009e.\3\2\2\2\u009f\u00a0\t\f\2\2\u00a0"+
		"\60\3\2\2\2\u00a1\u00a2\t\r\2\2\u00a2\62\3\2\2\2\u00a3\u00a4\t\16\2\2"+
		"\u00a4\64\3\2\2\2\u00a5\u00a6\t\17\2\2\u00a6\66\3\2\2\2\u00a7\u00a8\t"+
		"\20\2\2\u00a88\3\2\2\2\u00a9\u00aa\t\21\2\2\u00aa:\3\2\2\2\u00ab\u00ac"+
		"\t\22\2\2\u00ac<\3\2\2\2\u00ad\u00ae\t\23\2\2\u00ae>\3\2\2\2\u00af\u00b0"+
		"\t\24\2\2\u00b0@\3\2\2\2\u00b1\u00b2\t\25\2\2\u00b2B\3\2\2\2\u00b3\u00b4"+
		"\t\26\2\2\u00b4D\3\2\2\2\t\2apry~\u008f\3\b\2\2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}