using System;
using System.Drawing.Drawing2D;
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

        public Type ResultTypeBuilder => moduleBuilder.GetType("Result");

        public ConstructorInfo ResultTypeConstructor =>
            ResultTypeBuilder.GetConstructor(new[] {typeof(Func<object, object>)});

        public FieldInfo ResultLazyValueField => ResultTypeBuilder.GetField("lazyValue");
        public FieldInfo GetValueField => ResultTypeBuilder.GetField("GetValue");

        private void GenerateResultClass()
        {
            var resultType = moduleBuilder.DefineType("Result", TypeAttributes.Class // Define public class
                                                                | TypeAttributes.Public
                                                                | TypeAttributes.AnsiClass
                                                                | TypeAttributes.AutoClass);
            // Function Field 
            var valueInfo = resultType.DefineField("lazyValue", typeof(Func<object, object>), FieldAttributes.Private);
            // Constructor
            {
                var constructor = resultType.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard,
                    new[] {typeof(Func<object, object>)});

                constructor.DefineParameter(0, ParameterAttributes.In, "f"); // NECESSARY TO DEBUG 

                // public Result(Func<object, object> f) { ...
                using var il = new GroboIL(constructor);
                il.Ldarg(0); // this to stack
                il.Ldarg(1); // function from args to stack
                il.Stfld(valueInfo); // this.lazyValue = *function_from_args*}
            }

            //GetValue function
            // a => lazyValue(a)
            {
                var getValueMethod = resultType.DefineMethod("GetValue", MethodAttributes.Public,
                    CallingConventions.Standard,
                    resultType, new[] {typeof(object)});

                getValueMethod.DefineParameter(0, ParameterAttributes.In, "arg"); // NECESSARY TO DEBUG
                {
                    using var il = new GroboIL(getValueMethod);
                    il.Ldarg(0); // load this
                    il.Ldfld(ResultLazyValueField); // load this.lazyValue
                    il.Ldarg(1); // load argument
                    il.Calli(CallingConventions.Standard, typeof(void), new[] {typeof(object)}); // call this.lazyValue
                    il.Ret(); // return value
                }
            }

            //Map function: 
            {
                var mapMethod = resultType.DefineMethod("Map", MethodAttributes.Public, CallingConventions.Standard,
                    resultType, new[] {typeof(Func<object, object>)});

                mapMethod.DefineParameter(0, ParameterAttributes.In, "f"); // NECESSARY TO DEBUG 
                {
                    using var il = new GroboIL(mapMethod);
                    
                    il.Newobj(ResultTypeConstructor);
                    var localBuilder = il.DeclareLocal(ResultTypeBuilder, "tempResult");
                    var mapped = new GroboIL.Local(localBuilder, "mapped");
                    
                    il.Ldarg(0);
                    il.Ldfld(GetValueField);
                    il.Newobj(typeof(Func<object, object>).GetConstructor(new[] {typeof(Func<object, object>)}));
                    il.Stloc(mapped); // var mapped = new Func<...>(GetValue)

                    il.Ldloc(mapped);
                    il.Newobj(ResultTypeConstructor); // new Result(mapped)
                    
                    il.Ret(); // return 
                }
            }
        }
    }