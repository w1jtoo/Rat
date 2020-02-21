using System;
using CommandLine;

namespace Rat_Compiler
{
    internal class EntryPoint
    {
        static void Main(string[] args)
        {
            // Parser.Default
            //     .ParseArguments<Options>(args)
            //     .WithParsed<Options>(
            //         o => { Console.WriteLine(""); });
            Compiler.Compile("test.rat");
        }
    }
}