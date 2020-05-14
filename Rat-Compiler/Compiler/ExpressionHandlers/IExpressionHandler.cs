using System;

namespace Rat_Compiler.Compiler.ExpressionHandlers
{
    public interface IExpressionHandler
    {
        Type GetExpressionType();
        void Handle(ICodeGenerator generator);
    }
}