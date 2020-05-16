namespace Rat_Compiler.Compiler.CompiledContexts.Expressions
{
    public class FirstOperatorIlContext : ExpressionIlContext
    {
        public FirstOperator Operator { get; }
        public ExpressionIlContext Left { get; }
        public ExpressionIlContext Right { get; }

        public enum FirstOperator
        {
            Add,
            Or,
            Minus
        }

        public FirstOperatorIlContext(FirstOperator @operator, ExpressionIlContext left, ExpressionIlContext right)
        {
            Operator = @operator;
            Left = left;
            Right = right;
        }

        public override void Write()
        {
            throw new System.NotImplementedException();
        }
    }
}