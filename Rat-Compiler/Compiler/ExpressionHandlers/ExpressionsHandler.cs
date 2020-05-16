using System;
using Antlr4.Runtime;
using Rat_Compiler.Compiler.CompiledContexts;
using Rat_Compiler.Compiler.CompiledContexts.Expressions;

namespace Rat_Compiler.Compiler.ExpressionHandlers
{
    public class ExpressionsHandler : IExpressionHandler
    {
        public Type GetExpressionType()
        {
            return typeof(RatParser.ExpressionsContext);
        }

        public ICompiledContext Handle(IContextHandler contextHandler, ParserRuleContext ruleContext)
        {
            if (ruleContext is RatParser.ExpressionsContext expressions)
            {
                if (expressions.expr != null)
                {
                    return new ExpressionsIlContext(contextHandler.Handle(expressions.expr)
                        .AssertHasType<ExpressionIlContext>());
                }

                return new ExpressionsIlContext(
                    contextHandler.Handle(expressions.left).AssertHasType<ExpressionIlContext>(),
                    contextHandler.Handle(expressions.right).AssertHasType<ExpressionIlContext>(),
                    GetSeparator(expressions.separator.Text));
            }

            return null;
        }

        private static ExpressionsIlContext.ExpressionSeparator GetSeparator(string separatorText)
        {
            return separatorText.Trim() switch
            {
                "&" => ExpressionsIlContext.ExpressionSeparator.And,
                "|" => ExpressionsIlContext.ExpressionSeparator.Or,
                _ => ExpressionsIlContext.ExpressionSeparator.And
            };
        }
    }
}