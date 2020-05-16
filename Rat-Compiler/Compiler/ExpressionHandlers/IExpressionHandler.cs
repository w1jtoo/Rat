using System;
using Antlr4.Runtime;
using Rat_Compiler.Compiler.CompiledContexts;
using Rat_Compiler.Compiler.GeneratorHelper;

namespace Rat_Compiler.Compiler.ExpressionHandlers
{
    public interface IExpressionHandler
    {
        Type GetExpressionType();
        ICompiledContext Handle(IContextHandler contextHandler, ParserRuleContext ruleContext);
    }
}