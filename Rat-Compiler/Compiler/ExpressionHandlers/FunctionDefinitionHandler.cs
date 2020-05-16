using System;
using System.Linq;
using Antlr4.Runtime;
using log4net;
using Rat_Compiler.Compiler.CompiledContexts;
using Rat_Compiler.Compiler.CompiledContexts.Expressions;
using Rat_Compiler.Compiler.GeneratorHelper;

namespace Rat_Compiler.Compiler.ExpressionHandlers
{
    public class FunctionDefinitionHandler : IExpressionHandler
    {
        public Type GetExpressionType() => typeof(RatParser.FuncdefContext);

        public ICompiledContext Handle(IContextHandler contextHandler, ParserRuleContext ruleContext)
        {
            if (ruleContext is RatParser.FuncdefContext funcdef)
            {
                var id = TypeHelper.Unquoute(funcdef.id().GetText());
                var funcargs = funcdef
                    .funcarg()
                    .Select(c => TypeHelper.Unquoute(c.GetText()))
                    .ToArray();
                var expressions = contextHandler.Handle(funcdef.expressions()).AssertHasType<ExpressionsIlContext>();
                
                return new FunctionDefinitionIlContext(id, funcargs, expressions);
            }

            return null;
        }
    }
}