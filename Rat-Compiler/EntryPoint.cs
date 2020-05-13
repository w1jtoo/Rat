using Autofac;
using CommandLine;

namespace Rat_Compiler
{
    internal class EntryPoint
    {
        static void Main(string[] args)
        {
            Parser.Default
                .ParseArguments<Options>(args)
                .WithParsed<Options>(
                    o =>
                    {
                        using var container = DependenciesContainer.Create().Build();
                        var compiler = container.Resolve<ICompiler>();
                        compiler.Compile(o.FileName);
                    });
        }
    }
}