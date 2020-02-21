using System;
using System.IO;
using System.Linq;
using Antlr4.Runtime;

namespace Rat_Compiler
{
    static class Compiler
    {
        public static void Compile(String path)
        {
            var input = CharStreams.fromPath(path);
            var lexer = new RatLexer(input);
            var tokenStream = new BufferedTokenStream(lexer);
            var parser = new RatParser(tokenStream, Console.Out, Console.Error);
            parser.BuildParseTree = true;
            var startContext = parser.code();
            var visitor = new RatBaseVisitor<string>();
            var result = visitor.VisitCode(startContext);
            Console.WriteLine(result);
        }
    }
}