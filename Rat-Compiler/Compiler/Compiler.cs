using System.IO;
using Antlr4.Runtime;
using log4net;
using Rat_Compiler.Compiler.GeneratorHelper;
using Rat_Compiler.Infrastructure;

namespace Rat_Compiler.Compiler
{
    public class Compiler : ICompiler
    {
        private readonly ITreeWalker walker;
        private readonly IGeneratorHelper helper;
        private readonly ILog logger;
        private readonly IProjectData projectData;

        public Compiler(ILog logger, ITreeWalker walker, IProjectData projectData, IGeneratorHelper helper)
        {
            this.logger = logger;
            this.walker = walker;
            this.projectData = projectData;
            this.helper = helper;
        }

        public void Compile(StreamReader streamReader)
        {
            helper.AssemblyInit();

            var antlr = new AntlrInputStream(streamReader);
            var lexer = new RatLexer(antlr);
            var ts = new CommonTokenStream(lexer);
            var parser = new RatParser(ts);

            walker.Walk(parser);
        }

        public void SaveAssembly(string name)
        {
            helper.SaveAssembly(name);
        }
    }
}