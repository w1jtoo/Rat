using System;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using Antlr4.Runtime;
using log4net;
using Rat_Compiler.Infrastructure;

namespace Rat_Compiler.Compiler
{
    public class Compiler : ICompiler
    {
        private ModuleBuilder moduleBuilder;
        private AssemblyName assemblyName;
        public AssemblyBuilder AssemblyBuilder { get; private set; }

        private readonly ITreeWalker walker;
        private readonly ILog logger;
        private readonly IProjectData projectData;

        public Compiler(ILog logger, ITreeWalker walker, IProjectData projectData)
        {
            this.logger = logger;
            this.walker = walker;
            this.projectData = projectData;
        }

        public void Compile(StreamReader streamReader)
        {
            AssemblyInit();
            InitEntryPoint();

            var antlr = new AntlrInputStream(streamReader);
            var lexer = new ratLexer(antlr);
            var ts = new CommonTokenStream(lexer);
            var parser = new ratParser(ts);

            walker.Walk(parser);
        }

        private void AssemblyInit()
        {
            assemblyName = new AssemblyName(projectData.ProjectName);
            AssemblyBuilder =
                AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
            moduleBuilder = AssemblyBuilder.DefineDynamicModule(projectData.ProjectName);
        }

        private void InitEntryPoint()
        {
            var mainClass = moduleBuilder.DefineType($"{projectData.ProjectName}",
                TypeAttributes.Public | TypeAttributes.BeforeFieldInit);
            var main = mainClass.DefineMethod("Main", MethodAttributes.Public
                                                      | MethodAttributes.Static
                                                      | MethodAttributes.Final);
            AssemblyBuilder.SetEntryPoint(main);
            // Will generate next code: 
            //     public static class *name_of_project* { 
            //     public static void Main() { -- will be entry point 
            //     }
        }
    }
}