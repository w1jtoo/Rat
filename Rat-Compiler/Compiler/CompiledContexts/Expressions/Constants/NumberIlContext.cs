namespace Rat_Compiler.Compiler.CompiledContexts.Expressions.Constants
{
    public class NumberIlContext : ExpressionIlContext
    {
        public double Value { get; }

        public NumberIlContext(double value)
        {
            Value = value;
        }
        
        public override void Write()
        {
            throw new System.NotImplementedException();
        }
    }
}