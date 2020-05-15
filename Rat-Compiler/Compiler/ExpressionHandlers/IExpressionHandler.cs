using System;
using Rat_Compiler.Compiler.GeneratorHelper;

namespace Rat_Compiler.Compiler.ExpressionHandlers
{
    public interface IExpressionHandler
    {
        Type GetExpressionType();
        void Handle(ICodeGenerator generator);
    }
}