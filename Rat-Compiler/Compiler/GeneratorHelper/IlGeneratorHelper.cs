using System;
using System.Reflection;
using System.Reflection.Emit;
using GrEmit;
using Rat_Compiler.Infrastructure;

namespace Rat_Compiler.Compiler.GeneratorHelper
{
    public class IlGeneratorHelper : IGeneratorHelper
    {
        private AssemblyBuilder assemblyBuilder;
        private AssemblyName assemblyName;
        private ModuleBuilder moduleBuilder;

        private readonly IProjectData projectData;

        public IlGeneratorHelper(IProjectData projectData)
        {
            this.projectData = projectData;
        }

        public void SaveAssembly(string name)
        {
            assemblyBuilder.Save(name);
        }
        
        public void AssemblyInit()
        {
            assemblyName = new AssemblyName(projectData.ProjectName);
            assemblyBuilder =
                AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
            moduleBuilder = assemblyBuilder.DefineDynamicModule(projectData.ProjectName);

            InitEntryPoint();
            GenerateResultClass();
        }
        
        private void InitEntryPoint()
        {
            var mainClass = moduleBuilder.DefineType($"{projectData.ProjectName}",
                TypeAttributes.Public | TypeAttributes.BeforeFieldInit | TypeAttributes.Class);
            var main = mainClass.DefineMethod("Main", MethodAttributes.Public
                                                      | MethodAttributes.Static
                                                      | MethodAttributes.Final);
            assemblyBuilder.SetEntryPoint(main);
            // Will generate next code: 
            //     public static class *name_of_project* { 
            //     public static void Main() { -- will be entry point 
            //     }
        }

        public void GenerateResultClass()
        {
            var resultType = moduleBuilder.DefineType("Result", TypeAttributes.Class // Define public class
                                                          | TypeAttributes.Public
                                                          | TypeAttributes.AnsiClass
                                                          | TypeAttributes.AutoClass);
            var valueInfo = resultType.DefineField("lazyValue", typeof(Func<object, object>), FieldAttributes.Private);
            var constructor = resultType.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard,
                new[] {typeof(Func<object, object>)});

            using var il = new GroboIL(constructor);
            il.Ldarg(0); // this to stack
            il.Ldarg(1); // function from args to stack
            il.Stfld(valueInfo); // this.lazyValue = *function_from_args*
        }
    }
}