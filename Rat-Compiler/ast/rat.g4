grammar rat;
/*
 * Parser Rules
 */
code : (funcdef | typedef | extdef | expressions | ifblock)*;
extdef : LET EXT ID LBRACE (funcdef)*? RBRACE;
ifblock : IF expression ARROW (expression | LBRACE code RBRACE) (ELSE ARROW (expression | LBRACE code RBRACE))?;
typedef : LET TYPEKW ID LBRACE tupledef RBRACE;
funcdef : LET functype ID funcarg*? (ARROW type)? LBRACE code RBRACE;
expressions : expression (EXPRSEPARATOR expression)*;
expression : expression SEMICOLON
| expression LPARENTHESIS (expression ( COMMA expression )*)? RPARENTHESIS
| expression zeroLevelOperator expression
| expression firstLevelOperator expression
| expression secondLevelOperator expression
| expression thirdLevelOperator expression
| expression forthLevelOpeator expression
| LPARENTHESIS expression (COMMA expression)*? RPARENTHESIS | LPARENTHESIS COMMA RPARENTHESIS
| LPARENTHESIS expression RPARENTHESIS
| expression MEMDERCALLOPERATOR ID
| expression MEMDERCALLOPERATOR ID
| LET ID (COLON type)? SET expression
| expression (PLUSPLUS | MINUSMINUS)
| term ;
zeroLevelOperator : PRODUCT | DIVIDE | AND;
firstLevelOperator: ADD | MINUS | OR | XOR ;
secondLevelOperator: IN | IS;
thirdLevelOperator : EQUALS ;
forthLevelOpeator: SET | ADDEQUALS | MINUSEQUALS | PRODUCTEQUALS| DIVIDEEQUALS | XOREQUALS | ANDEQUALS | OREQUALS;
term : STRING | NUMBER | TRUE | FALSE | ID ;
functype : FN | MUT | PARALLEL;
funcarg : (MUT)? arg COLON type;
tupledef: LPARENTHESIS arg (COMMA arg)*? RPARENTHESIS WHERE expression;
type : (NIL | ANY | OPTIONAL) EXPRSEPARATOR type | NIL | ANY | OPTIONAL | ID | tupletype;
tupletype : LPARENTHESIS type (COMMA type)*? RPARENTHESIS ;
arg : ID (SET expression)? ;
/*
 * Lexer Rules
 */
IF : 'if' ;
ELSE : 'else' ;
WHERE : 'where' ;
IN : 'in' ;
IS : 'is' ;
MUT : 'mut' ;
FN : 'fn' ;
PARALLEL : 'parallel' ;
ARROW: '=>' ;
ANY : 'Any' ;
OPTIONAL : 'Optional' ;
NIL : 'Nil' ;
LET : 'let' ;
TYPEKW : 'type' ;
EXT : 'ext' ;
ENDEXT : 'endext' ;
AND : 'and' ;
ANDEQUALS : '&=' ;
OR : 'or' ;
OREQUALS : '|=' ;
TRUE : 'true' ;
FALSE : 'false' ;
TWODOTS : '..' ;
COLON : ':' ;
SEMICOLON : ';';
EXPRSEPARATOR : '&' | '|';
ID : [A-Za-z_] [A-Za-z_0-9]* ;
RANGE : NUMBER TWODOTS NUMBER ;
COMMA : ',' ;
XOREQUALS : '^=' ;
XOR : 'xor' | '^' ;
ADDEQUALS : '+=' ;
PLUSPLUS : '++' ;
ADD : '+' ;
MINUSEQUALS : '-=' ;
MINUSMINUS : '--' ;
MINUS : '-' ;
PRODUCTEQUALS : '*=' ;
PRODUCT : '*' ;
DIVIDEEQUALS : '/=' ;
DIVIDE : '/' ;
EQUALS : '==' ;
SET : '=' ;
LPARENTHESIS : '(' ;
RPARENTHESIS : ')' ;
LBRACE : '{' ;
RBRACE : '}' ;
MEMDERCALLOPERATOR : '::' ;
NUMBER : ( '-' | '+' )? [0-9]+ ;
NULLCHAR : '\\0' ;
CHAR1 : '\\1' ;
CHAR2 : '\\2' ;
CHAR3 : '\\3' ;
CHAR4 : '\\4' ;
CHAR5 : '\\5' ;
CHAR6 : '\\6' ;
CHAR7 : '\\7' ;
CHAR8 : '\\8' ;
CHAR9 : '\\9' ;
CHAR10 : '\\10' ;
CHAR11 : '\\11' ;
CHAR12 : '\\12' ;
CHAR13 : '\\13' ;
CHAR14 : '\\14' ;
CHAR15 : '\\15' ;
CHAR16 : '\\16' ;
CHAR17 : '\\17' ;
CHAR18 : '\\18' ;
CHAR19 : '\\19' ;
CHAR20 : '\\20' ;
CHAR21 : '\\21' ;
CHAR22 : '\\22' ;
CHAR23 : '\\23' ;
CHAR24 : '\\24' ;
CHAR25 : '\\25' ;
CHAR26 : '\\26' ;
CHAR27 : '\\27' ;
CHAR28 : '\\28' ;
CHAR29 : '\\29' ;
CHAR30 : '\\30' ;
CHAR31 : '\\31' ;
ESCAPEDQUOTE : '\\\'' ;
ESCAPEDDOUBLEQUOTE : '\\"' ;
QUOTE : '\'' ;
DOUBLEQUOTE : '"' ;
STRING : QUOTEDSTRING | DOUBLEQUOTEDSTRING ;
QUOTEDSTRING : QUOTE ( NULLCHAR | CHAR1 | CHAR2 | CHAR3 | CHAR4 | CHAR5 | CHAR6 | CHAR7 | CHAR8 | CHAR9 | CHAR10 | CHAR11 | CHAR12 | CHAR13 | CHAR14 | CHAR15 | CHAR16 | CHAR17 | CHAR18 | CHAR19 | CHAR20 | CHAR21 | CHAR22 | CHAR23 | CHAR24 | CHAR25 | CHAR26 | CHAR27 | CHAR28 | CHAR29 | CHAR30 | CHAR31 | ESCAPEDQUOTE | ESCAPEDDOUBLEQUOTE | DOUBLEQUOTE | . )*? QUOTE;
DOUBLEQUOTEDSTRING : DOUBLEQUOTE ( NULLCHAR | CHAR1 | CHAR2 | CHAR3 | CHAR4 | CHAR5 | CHAR6 | CHAR7 | CHAR8 | CHAR9 | CHAR10 | CHAR11 | CHAR12 | CHAR13 | CHAR14 | CHAR15 | CHAR16 | CHAR17 | CHAR18 | CHAR19 | CHAR20 | CHAR21 | CHAR22 | CHAR23 | CHAR24 | CHAR25 | CHAR26 | CHAR27 | CHAR28 | CHAR29 | CHAR30 | CHAR31 | ESCAPEDQUOTE | ESCAPEDDOUBLEQUOTE | QUOTE | . )*? DOUBLEQUOTE;
WHITESPACE : ' ' -> skip;
LINEFEED : '\n' -> skip;