grammar Cmsql;

/*
 * Parser rules
 */
queries				: query (TERMINATOR query)* EOF
					;

query				: selectClause fromClause whereClause? TERMINATOR*
					;

selectClause		: SELECT IDENTIFIER
					;

fromClause			: FROM (NUMBER|START|ROOT)
					;

whereClause			: WHERE expression
					;

expression			: condition										# conditionExpression
					| left=expression op=(AND|OR) right=expression	# binaryExpression
					| LPAREN expression RPAREN						# parenthesizedExpression
					;

condition			: IDENTIFIER op=(EQUALS|NOTEQUALS|GREATERTHAN|LESSTHAN|GREATERTHANOREQUALS|LESSTHANOREQUALS) LITERAL
					;

/*
* Lexer rules
*/
SELECT				: S E L E C T ;
FROM				: F R O M ;
WHERE				: W H E R E ;
OR					: O R ;
AND					: A N D ;
NUMBER				: [0-9]+ ;
START				: S T A R T ;
ROOT				: R O O T ;
LITERAL				: '\''([ a-zA-Z0-9]|[_]|[-])+'\'' ;
IDENTIFIER			: [a-zA-Z]+[a-zA-Z0-9]* ;
LPAREN				: '(' ;
RPAREN				: ')' ;
TERMINATOR			: ';' ;
EQUALS				: '=' ;
NOTEQUALS			: '!=' ;
GREATERTHAN			: '>' ;
LESSTHAN			: '<' ;
GREATERTHANOREQUALS	: '>=' ;
LESSTHANOREQUALS	: '<=' ;
WHITESPACE			: [ \r\n\t]+ -> skip ;
ERRORCHAR			: . ;

fragment S			: [Ss] ;
fragment E			: [Ee] ;
fragment L			: [Ll] ;
fragment C			: [Cc] ;
fragment T			: [Tt] ;
fragment F			: [Ff] ;
fragment R			: [Rr] ;
fragment O			: [Oo] ;
fragment M			: [Mm] ;
fragment P			: [Pp] ;
fragment A			: [Aa] ;
fragment G			: [Gg] ;
fragment W			: [Ww] ;
fragment H			: [Hh] ;
fragment N			: [Nn] ;
fragment D			: [Dd] ;
