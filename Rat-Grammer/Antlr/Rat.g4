grammar Rat;
/*
 * Parser Rules
 */
tokens { INDENT, DEDENT } 

@lexer::members {
  // A queue where extra tokens are pushed on (see the NEWLINE lexer rule).
  private System.Collections.Generic.LinkedList<Token> tokens = new System.Collections.Generic.LinkedList<>();
  // The stack that keeps track of the indentation level.
  private System.Collections.Generic.Stack<Integer> indents = new System.Collections.Generic.Stack<>();
  // The amount of opened braces, brackets and parenthesis.
  private int opened = 0;
  // The most recently produced token.
  private Token lastToken = null;
  
  public void Emit(Token t) {
    SetToken(t);
    tokens.Offer(t);
  }


  public Token nextToken() {
    // Check if the end-of-file is ahead and there are still some DEDENTS expected.
    if (_input.LA(1) == EOF && this.indents.Count > 0) {
      // Remove any trailing EOF tokens from our buffer.
      for (int i = tokens.Count - 1; i >= 0; i--) {
        if (tokens[i].GetType() == EOF) {
          tokens.Remove(i);
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
      this.Emit(CommonToken(RatParser.EOF, "<EOF>"));
    }

    Token next = NextToken();

    if (next.GetChannel() == Token.DEFAULT_CHANNEL) {
      // Keep track of the last token on the default channel.
      this.lastToken = next;
    }

    return tokens.Length == 0 ? next : tokens.Poll();
  }

  private Token createDedent() {
    CommonToken dedent = CommonToken(RatParser.DEDENT, "");
    dedent.SetLine(this.lastToken.GetLine());
    return dedent;
  }

  private CommonToken CommonToken(int type, string text) {
    int stop = this.GetCharIndex() - 1;
    int start = text.Length == 0 ? stop : stop - text.Length + 1;
    return new CommonToken(this._tokenFactorySourcePair, type, DEFAULT_TOKEN_CHANNEL, start, stop);
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
      }
    }

    return count;
  }

  boolean AtStartOfInput() {
    return GetCharPositionInLine() == 0 && GetLine() == 1;
  }
}

code : statement+;

statementblock : NEWLINE INDENT statement+ DEDENT;
statement : ((funcdef | ifstmt | funccall) SEMICOLON? NEWLINE) | NEWLINE;

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


/*
 * Lexer Rules
 */
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
 : ( {atStartOfInput()}?   WHITESPACE
   | ( '\r'? '\n' | '\r' | '\f' ) WHITESPACE?
   )
   {
     String newLine = GetText().ReplaceAll("[^\r\n\f]+", "");
     String spaces = GetText().ReplaceAll("[\r\n\f]+", "");
	 
     // Strip newlines inside open clauses except if we are near EOF. We keep NEWLINEs near EOF to
     // satisfy the final newline needed by the single_put rule used by the REPL.
     int next = _input.LA(1);
     int nextnext = _input.LA(2);
     if (opened > 0 || (nextnext != -1 && (next == '\r' || next == '\n' || next == '\f' || next == '#'))) {
       // If we're inside a list or on a blank line, ignore all indents, 
       // dedents and line breaks.
       skip();
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
         while(!indents.Count > 0 && indents.Peek() > indent) {
           this.Emit(CreateDedent());
           indents.Pop();
         }
       }
     }
   }
 ;
LINEFEED : '\n' -> skip;