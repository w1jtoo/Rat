namespace Rat_Compiler.Compiler.CompiledContexts
{
    public class StatementBlockIlContext : ICompiledContext
    {
        public StatementIlContext[] Statements { get; }

        public StatementBlockIlContext(StatementIlContext[] statements)
        {
            Statements = statements;
        }
        
        public void Write()
        {
            throw new System.NotImplementedException();
        }
    }
}