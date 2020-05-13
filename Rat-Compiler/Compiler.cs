using System.IO;
using Antlr4.Runtime;
using log4net;

namespace Rat_Compiler
{
    public class Compiler : ICompiler
    {
        private readonly ITreeWalker walker;
        private readonly ILog logger;
        public Compiler(ILog logger, ITreeWalker walker)
        {
            this.logger = logger;
            this.walker = walker;
        }
        public void Compile(string fileName)
        {
            var path = Path.Join(Directory.GetCurrentDirectory(), fileName);
            if (! File.Exists(path))
            {
                logger.Fatal($"Can't find file: {path}");
            }
            else
            {
                using var fs = new StreamReader(path);
                
                var antlr = new AntlrInputStream(fs);
                var lexer = new ratLexer(antlr);
                var ts = new CommonTokenStream(lexer);
                var parser = new ratParser(ts);
                
                walker.Walk(parser);
            }
        }
    }
}