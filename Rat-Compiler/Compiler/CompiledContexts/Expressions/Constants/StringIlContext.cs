namespace Rat_Compiler.Compiler.CompiledContexts.Expressions.Constants
{
    public class StringIlContext : ExpressionIlContext
    {
        public string Value { get; }

        public StringIlContext(string value)
        {
            Value = value;
        }
        
        public override void Write()
        {
            throw new System.NotImplementedException();
        }
    }
}