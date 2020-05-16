using System;
using System.Linq;
using Antlr4.Runtime;
using log4net;
using Rat_Compiler.Compiler.CompiledContexts;
using Rat_Compiler.Compiler.CompiledContexts.Expressions;
using Rat_Compiler.Compiler.GeneratorHelper;

namespace Rat_Compiler.Compiler.ExpressionHandlers
{
    public class IfStatementHandler : IExpressionHandler
    {
        public Type GetExpressionType() => typeof(RatParser.IfstmtContext);

        public ICompiledContext Handle(IContextHandler contextHandler, ParserRuleContext ruleContext)
        {
            if (ruleContext is RatParser.IfstmtContext ifstmt)
            {
                var expr = contextHandler.Handle(ifstmt.expression()).AssertHasType<ExpressionIlContext>();
                var truth = contextHandler.Handle(ifstmt.truth).AssertHasType<StatementBlockIlContext>();
                var lie = contextHandler.Handle(ifstmt.lie).AssertHasType<StatementBlockIlContext>();
                
                return new IfIlContext(expr, truth, lie);
            }

            return null;
        }
    }
}