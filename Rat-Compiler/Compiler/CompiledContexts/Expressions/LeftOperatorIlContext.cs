namespace Rat_Compiler.Compiler.CompiledContexts.Expressions
{
    public class LeftOperatorIlContext : ExpressionIlContext
    {
        public LeftOperator Operator { get; }
        public ExpressionIlContext Expression { get; }

        public enum LeftOperator
        {
            Not, UMinus
        }

        public LeftOperatorIlContext(LeftOperator @operator, ExpressionIlContext expression)
        {
            Operator = @operator;
            Expression = expression;
        }
        
        public override void Write()
        {
            throw new System.NotImplementedException();
        }
    }
}