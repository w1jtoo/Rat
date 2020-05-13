using System;

namespace Rat_Compiler
{
    public interface IExpressionHandler
    {
        Type GetExpressionType();
        void Handle(ICodeGenerator generator);
    }
}