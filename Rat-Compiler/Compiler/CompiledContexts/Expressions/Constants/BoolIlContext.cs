namespace Rat_Compiler.Compiler.CompiledContexts.Expressions.Constants
{
    public class BoolIlContext : ExpressionIlContext
    {
        public bool Value { get; }

        public BoolIlContext(bool value)
        {
            Value = value;
        }
        
        public override void Write()
        {
            throw new System.NotImplementedException();
        }
    }
}