using Rat_Compiler.Compiler.CompiledContexts.Expressions;

namespace Rat_Compiler.Compiler.CompiledContexts
{
    public class IfIlContext : StatementIlContext
    {
        public ExpressionIlContext Expression { get; }
        public StatementBlockIlContext IfTrue { get; }
        public StatementBlockIlContext IfFalse { get; }

        public IfIlContext(ExpressionIlContext expression,
            StatementBlockIlContext ifTrue, StatementBlockIlContext ifFalse)
        {
            Expression = expression;
            IfTrue = ifTrue;
            IfFalse = ifFalse;
        }

        public override void Write()
        {
            throw new System.NotImplementedException();
        }
    }
}