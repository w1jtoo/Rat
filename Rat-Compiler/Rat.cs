using System.IO;
using log4net;
using Rat_Compiler.Compiler;
using Rat_Compiler.Infrastructure;

namespace Rat_Compiler
{
    public class Rat : IRat
    {
        private readonly ILog logger;
        private readonly ICompiler compiler;
        private readonly IProjectData projectData;

        public Rat(ILog logger, ICompiler compiler, IProjectData projectData)
        {
            this.logger = logger;
            this.compiler = compiler;
            this.projectData = projectData;
        }

        public void Compile()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), projectData.GetEntryPointFileName);
            if (!File.Exists(path))
            {
                logger.Fatal($"Can't find file: {path}");
            }
            else
            {
                using var fs = new StreamReader(path);
                compiler.Compile(fs);
                compiler.AssemblyBuilder.Save(projectData.GetEntryPointFileName);
            }
        }

        public string GetVersion()
        {
            return "v0.1";
        }

        public void InitDirectory()
        {
            throw new System.NotImplementedException();
        }
    }
}