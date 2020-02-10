using System;
using System.IO;
using Antlr4.Runtime;

namespace Rat_Compiler
{
    static class Compiler
    {
        public static void Compile(String path)
        {
            using (var stream = new StreamReader(path))
            {
                var antlr = new AntlrInputStream(); 
            }
        }   
    }
}