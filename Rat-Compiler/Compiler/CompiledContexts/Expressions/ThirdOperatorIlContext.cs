namespace Rat_Compiler.Compiler.CompiledContexts.Expressions
{
    public class ThirdOperatorIlContext : ExpressionIlContext
    {
        public ThirdOperator Operator { get; }
        public ExpressionIlContext Left { get; }
        public ExpressionIlContext Right { get; }

        public enum ThirdOperator
        {
            In,
            Is
        }

        public ThirdOperatorIlContext(ThirdOperator @operator, ExpressionIlContext left, ExpressionIlContext right)
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