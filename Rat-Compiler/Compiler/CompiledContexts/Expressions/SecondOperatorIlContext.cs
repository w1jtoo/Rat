namespace Rat_Compiler.Compiler.CompiledContexts.Expressions
{
    public class SecondOperatorIlContext : ExpressionIlContext
    {
        public SecondOperator Operator { get; }
        public ExpressionIlContext Left { get; }
        public ExpressionIlContext Right { get; }

        public enum SecondOperator
        {
            Product,
            Divide,
            And
        }

        public SecondOperatorIlContext(SecondOperator @operator, ExpressionIlContext left, ExpressionIlContext right)
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