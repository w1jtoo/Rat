using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Antlr4.Runtime;
using GrEmit;
using log4net;
using Rat_Compiler.Infrastructure;

namespace Rat_Compiler.Compiler
{
    public class Compiler : ICompiler
    {
        public ModuleBuilder ModuleBuilder { get; private set; } // needed to windows file save
        private AssemblyName assemblyName;
        private AssemblyBuilder assemblyBuilder;

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
            assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder = assemblyBuilder.DefineDynamicModule(projectData.ProjectName);
            // TODO: preprocessor directive:
            // Why? Because we use ILAMS to compile 
            // generated IL code to OS with mono support,
            // But in fact, better to use assemblyBuilder.Save(); - that works only in windows 
            // TODO: Check if it works
        }

        private void InitEntryPoint()
        {
            var mainClass = ModuleBuilder.DefineType($"{projectData.ProjectName}",
                TypeAttributes.Public | TypeAttributes.BeforeFieldInit);
            var main = mainClass.DefineMethod("Main", MethodAttributes.Public
                                           | MethodAttributes.Static
                                           | MethodAttributes.Final);
            // using (var il =new GroboIL(main))
            // {
            //     il.
            // }
            Console.WriteLine(assemblyBuilder.CodeBase);
            // public static class *name_of_project* { 
            // public static void Main() { -- will be entry point 
            // }
        }
    }
}