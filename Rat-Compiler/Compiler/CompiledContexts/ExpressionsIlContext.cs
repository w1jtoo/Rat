using Rat_Compiler.Compiler.CompiledContexts.Expressions;

namespace Rat_Compiler.Compiler.CompiledContexts
{
    public class ExpressionsIlContext : StatementIlContext
    {
        public ExpressionIlContext Left { get; }
        public ExpressionIlContext Right { get; }
        public ExpressionSeparator Separator { get; }

        public enum ExpressionSeparator
        {
            And,
            Or
        }

        public ExpressionsIlContext(ExpressionIlContext expression)
        {
            Left = expression;
        }

        public ExpressionsIlContext(ExpressionIlContext left, ExpressionIlContext right, ExpressionSeparator separator)
        {
            Left = left;
            Right = right;
            Separator = separator;
        }

        public override void Write()
        {
            throw new System.NotImplementedException();
        }
    }
}