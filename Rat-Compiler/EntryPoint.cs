using System;
using Autofac;
using CommandLine;
using Rat_Compiler.Infrastructure;

namespace Rat_Compiler
{
    public class EntryPoint
    {
        static void Main(string[] args)
        {
            Parser.Default
                .ParseArguments<Options>(args)
                .WithParsed<Options>(
                    o =>
                    {
                        var data = new ProjectData("f");
                        using var container = DependenciesContainer.Create(data).Build();
                        
                        var rat = container.Resolve<IRat>();
                        rat.Compile();
                    });
        }
    }
}