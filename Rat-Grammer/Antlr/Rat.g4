grammar Rat;
/*
 * Parser Rules
 */
tokens { INDENT, DEDENT } 

@lexer::members {
  // A queue where extra tokens are pushed on (see the NEWLINE lexer rule).
  private System.Collections.Generic.List<IToken> tokens = new System.Collections.Generic.List<IToken>();
  // The stack that keeps track of the indentation level.
  private System.Collections.Generic.Stack<int> indents = new System.Collections.Generic.Stack<int>();
  // The amount of opened braces, brackets and parenthesis.
  private int opened = 0;
  public const int EOF = -1;
  System.Text.RegularExpressions.Regex NotLfRegex = new System.Text.RegularExpressions.Regex("[^\r\n\f]+");
  System.Text.RegularExpressions.Regex LfRegex = new System.Text.RegularExpressions.Regex("[\r\n\f]+");

  public override void Emit(IToken t) {
      base.Emit(t);
      tokens.Add(t);
  }

  public override IToken NextToken() 
  {
    // Check if the end-of-file is ahead and there are still some DEDENTS expected.
    if (HitEOF && this.indents.Count > 0) {
      // Remove any trailing EOF tokens from our buffer.
      for (int i = tokens.Count - 1; i >= 0; i--) {
        if (tokens[i].Type == -1) {
          tokens.RemoveAt(i);
        }
      }

      // First emit an extra line break that serves as the end of the statement.
      this.Emit(CommonToken(RatParser.NEWLINE, "\n"));

      // Now emit as much DEDENT tokens as needed.
      while (indents.Count > 0) {
        this.Emit(CreateDedent());
        indents.Pop();
      }

      // Put the EOF back on the token stream.
      this.Emit(CommonToken(EOF, "<EOF>"));
    }

    IToken next = NextToken();

    if (next.Channel == DefaultTokenChannel) {
      // Keep track of the last token on the default channel.
      base.Emit(next);
    }

	if (tokens.Count != 0)
	{
      next = tokens[0];
      tokens.RemoveAt(0);
	}

	return next;
  }

  private IToken CreateDedent() {
    CommonToken dedent = CommonToken(RatParser.DEDENT, "");
    dedent.Line = Token.Line;
    return dedent;
  }

  private CommonToken CommonToken(int type, string text) {
    int stop = this.CharIndex - 1;
    int start = text.Length == 0 ? stop : stop - text.Length + 1;
    return new CommonToken(Tuple.Create((ITokenSource)this, ((ITokenSource)this).InputStream), type, DefaultTokenChannel, start, stop);
  }
  
  static int GetIndentationCount(string spaces) {
    int count = 0;
    foreach(var ch in spaces.ToCharArray()) {
      switch (ch) {
        case '\t':
          count += 4 - (count % 4);
          break;
        default:
          // A normal space char.
          count++;
          break;
      }
    }

    return count;
  }

  bool AtStartOfInput() {
    return CharIndex == 0 && Line == 1;
  }
}

code : statement+;

statementblock : NEWLINE INDENT statement+ DEDENT;
statement : ((funcdef | ifstmt | funccall | externstmt) SEMICOLON? NEWLINE) | NEWLINE;

externstmt : EXTERN string COLON NEWLINE INDENT line* DEDENT;

expressions : expression (EXPRSEPARATOR expression)*? ;
expression : funccall 
    | ifexpr 
    | expression COMPAREOPERATOR expression |
    leftoperator expression |
    expression zerooperator expression |
    expression firstoperator expression |
    expression secondoperator expression |
    expression thirdoperator expression
    | string | bool | range | number | ID;

leftoperator : NOT | MINUS;
zerooperator : SET;
firstoperator : ADD | OR | MINUS ;
secondoperator : PRODUCT | DIVIDE | AND ;
thirdoperator : IN | IS ;


funcdef : LET ID LPARENTHESIS (funcargs)? RPARENTHESIS SET expressions ;
funcargs : funcarg (COMMA funcarg)*?;
funcarg : ID ;

funccall : ID LPARENTHESIS exprargs RPARENTHESIS;
exprargs : expressions (COMMA expressions)*? ;

ifexpr : IF LPARENTHESIS expression RPARENTHESIS expression ELSE expression;
ifstmt : IF LPARENTHESIS expression RPARENTHESIS statementblock ELSE statementblock;

line : string NEWLINE? ;
/*
 * Lexer Rules
 */
EXTERN : 'extern' ;
COMPAREOPERATOR : EQUALS | INEQUALS | GREATER | LESS |GREATEREQUAL | LESSEQUAL ;
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
LETG : 'LET' ;
TYPEKW : 'type' ;
EXT : 'ext' ;
ENDEXT : 'endext' ;
AND : 'and' ;
ANDEQUALS : '&=' ;
OR : 'or' ;
NOT : 'not' ;
OREQUALS : '|=' ;
TWODOTS : '..' ;
COLON : ':' ;
SEMICOLON : ';';
EXPRSEPARATOR : '&' | '|';
ID : [A-Za-z_] [A-Za-z_0-9]* ;
COMMA : ',' ;
XOREQUALS : '^=' ;
XOR : 'xor' | '^' ;
ADDEQUALS : '+=' ;
PLUSPLUS : '++' ;
ADD : '+' ;
MINUSEQUALS : '-=' ;
MINUS : '-' ;
PRODUCTEQUALS : '*=' ;
PRODUCT : '*' ;
DIVIDEEQUALS : '/=' ;
DIVIDE : '/' ;
EQUALS : '==' ;
INEQUALS : '!=' ;
GREATER : '>' ;
LESS : '<' ;
GREATEREQUAL : '>=' ;
LESSEQUAL : '<=' ;
SET : '=' ;
LPARENTHESIS : '(' ;
RPARENTHESIS : ')' ;
LBRACE : '{' ;
RBRACE : '}' ;
MEMDERCALLOPERATOR : '::' ;
bool : TRUE | FALSE ;
range : number TWODOTS number ;
string : QUOTEDSTRING | DOUBLEQUOTEDSTRING ;
number : ( '-' | '+' )? DIGIT+ ;
TRUE : 'true' ;
FALSE : 'false' ;
DIGIT : [0-9];
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
QUOTEDSTRING : QUOTE ( NULLCHAR | CHAR1 | CHAR2 | CHAR3 | CHAR4 | CHAR5 | CHAR6 | CHAR7 | CHAR8 | CHAR9 | CHAR10 | CHAR11 | CHAR12 | CHAR13 | CHAR14 | CHAR15 | CHAR16 | CHAR17 | CHAR18 | CHAR19 | CHAR20 | CHAR21 | CHAR22 | CHAR23 | CHAR24 | CHAR25 | CHAR26 | CHAR27 | CHAR28 | CHAR29 | CHAR30 | CHAR31 | ESCAPEDQUOTE | ESCAPEDDOUBLEQUOTE | DOUBLEQUOTE | . )*? QUOTE;
DOUBLEQUOTEDSTRING : DOUBLEQUOTE ( NULLCHAR | CHAR1 | CHAR2 | CHAR3 | CHAR4 | CHAR5 | CHAR6 | CHAR7 | CHAR8 | CHAR9 | CHAR10 | CHAR11 | CHAR12 | CHAR13 | CHAR14 | CHAR15 | CHAR16 | CHAR17 | CHAR18 | CHAR19 | CHAR20 | CHAR21 | CHAR22 | CHAR23 | CHAR24 | CHAR25 | CHAR26 | CHAR27 | CHAR28 | CHAR29 | CHAR30 | CHAR31 | ESCAPEDQUOTE | ESCAPEDDOUBLEQUOTE | QUOTE | . )*? DOUBLEQUOTE;
WHITESPACE : ' ' -> skip;

NEWLINE
 : ( {AtStartOfInput()}?   WHITESPACE
   | ( '\r'? '\n' | '\r' | '\f' ) WHITESPACE?
   )
   {
     String newLine = NotLfRegex.Replace(Text,"");
     String spaces = LfRegex.Replace(Text,"");
     
     // Strip newlines inside open clauses except if we are near EOF. We keep NEWLINEs near EOF to
     // satisfy the final newline needed by the single_put rule used by the REPL.
     int next = InputStream.LA(1);
     int nextnext = InputStream.LA(2);
     if (opened > 0 || (nextnext != -1 && (next == '\r' || next == '\n' || next == '\f' || next == '#'))) {
       // If we're inside a list or on a blank line, ignore all indents, 
       // dedents and line breaks.
       Skip();
     }
     else {
       Emit(CommonToken(NEWLINE, newLine));
       int indent = GetIndentationCount(spaces);
       int previous = indents.Count == 0 ? 0 : indents.Peek();
       if (indent == previous) {
         // skip indents of the same size as the present indent-size
         Skip();
       }
       else if (indent > previous) {
         indents.Push(indent);
         Emit(CommonToken(RatParser.INDENT, spaces));
       }
       else {
         // Possibly emit more than 1 DEDENT token.
         while(indents.Count > 0 && indents.Peek() > indent) {
           this.Emit(CreateDedent());
           indents.Pop();
         }
       }
     }
   }
 ;
LINEFEED : '\n' -> skip;