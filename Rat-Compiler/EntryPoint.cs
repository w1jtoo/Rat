using System;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using CommandLine;
using GrEmit;
using Lokad.ILPack;

namespace Rat_Compiler
{
    internal class EntryPoint
    {
        static void Main(string[] args)
        {
            Parser.Default
                .ParseArguments<Options>(args)
                .WithParsed<Options>(
                    o => { Console.WriteLine(""); });
        }
    }
}