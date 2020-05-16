using Rat_Compiler.Compiler.Exceptions;

namespace Rat_Compiler.Compiler.CompiledContexts
{
    public interface ICompiledContext
    {
        public void Write();
    }

    public static class CompiledContextExtensions
    {
        public static T AssertHasType<T>(this ICompiledContext context)
        {
            if (context is T t)
                return t;
            throw new ExpressionTypeMismatchException(context, typeof(T));
        }
    }
}