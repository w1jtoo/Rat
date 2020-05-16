namespace Rat_Compiler.Compiler.CompiledContexts
{
    public class FunctionDefinitionIlContext : StatementIlContext
    {
        public string Id { get; }
        public string[] Args { get; }
        public ExpressionsIlContext Expressions { get; }

        public FunctionDefinitionIlContext(string id, string[] args, ExpressionsIlContext expressions)
        {
            Id = id;
            Args = args;
            Expressions = expressions;
        }
        
        public override void Write()
        {
            throw new System.NotImplementedException();
        }
    }
}