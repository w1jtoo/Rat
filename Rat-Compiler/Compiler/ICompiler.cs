using System.IO;
using System.Reflection.Emit;

namespace Rat_Compiler.Compiler
{
    public interface ICompiler
    {
        void Compile(StreamReader streamReader);
        public ModuleBuilder ModuleBuilder { get; }
    }
}