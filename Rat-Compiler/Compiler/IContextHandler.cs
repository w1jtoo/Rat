using Antlr4.Runtime;
using Rat_Compiler.Compiler.CompiledContexts;

namespace Rat_Compiler.Compiler
{
    public interface IContextHandler
    {
        public ICompiledContext Handle(ParserRuleContext context);
    }
}