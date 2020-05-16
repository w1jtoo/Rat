using System;
using Rat_Compiler.Compiler.CompiledContexts;

namespace Rat_Compiler.Compiler.Exceptions
{
    public class ExpressionTypeMismatchException : Exception
    {
        public ExpressionTypeMismatchException(ICompiledContext context, Type type) : base(
            $"{context} required to have type {type.FullName}")
        {
        }
    }
}