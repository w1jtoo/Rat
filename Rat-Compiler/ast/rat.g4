grammar rat;
/*
 * Parser Rules
 */
SUM : TERM ADD TERM;
SUBTRACT : TERM MINUS TERM ;
MULTIPLY : TERM PRODUCT TERM ;
DIVIDEOPERATION : TERM DIVIDE TERM ;
MEMBERCALL : MEMDERCALLOPERATOR ID;
TERM : CONST | ID | MEMBERCALL ;
CONST : STRING | NUMBER ;
/*
 * Lexer Rules
 */
MEMDERCALLOPERATOR : '::' ;
ANY : 'Any' ;
OPTIONAL : 'Optional' ;
NIL : 'Nil' ;
LET : 'let' ;
TYPE : 'type' ;
AND : 'and' | '&' ;
OR : 'or' | '|' ;
XOR : 'xor' | '^' ;
ADD : '+' ;
MINUS : '-' ;
PRODUCT : '*' ;
DIVIDE : '/' ;
ID : [A-Za-z_] [A-Za-z_0-9] ;
NUMBER : ( '-' | '+' )? [0-9]+ ;
QUOTE : '"' | '\'' ;
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
ESCAPEDQUOTE : '\'' ;
ESCAPEDDOUBLEQUOTE : '"' ;
STRING : QUOTE ( NULLCHAR | CHAR1 | CHAR2 | CHAR3 | CHAR4 | CHAR5 | CHAR6 | CHAR7 | CHAR8 | CHAR9 | CHAR10 | CHAR11 | CHAR12 | CHAR13 | CHAR14 | CHAR15 | CHAR16 | CHAR17 | CHAR18 | CHAR19 | CHAR20 | CHAR21 | CHAR22 | CHAR23 | CHAR24 | CHAR25 | CHAR26 | CHAR27 | CHAR28 | CHAR29 | CHAR30 | CHAR31 | ESCAPEDQUOTE | ESCAPEDDOUBLEQUOTE | . )*? QUOTE;
WHITESPACE : ' ' -> skip ;