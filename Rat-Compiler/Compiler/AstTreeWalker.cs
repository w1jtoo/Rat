using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using Antlr4.Runtime;
using Rat_Compiler.Compiler.CompiledContexts;
using Rat_Compiler.Compiler.ExpressionHandlers;
using Rat_Compiler.Compiler.GeneratorHelper;

namespace Rat_Compiler.Compiler
{
    public class AstTreeWalker : ITreeWalker, IContextHandler
    {
        private readonly Dictionary<Type, IExpressionHandler> typeToHandler;
        private readonly ICodeGenerator generator;

        public AstTreeWalker(IEnumerable<IExpressionHandler> handlers, ICodeGenerator generator)
        {
            this.generator = generator;
            typeToHandler = handlers
                .ToDictionary(el => el.GetExpressionType(), v => v);
        }

        public void Walk(RatParser parser)
        {
            foreach (var expr in parser.code().statement())
            {
                Handle(expr);
            }
        }

        public ICompiledContext Handle(ParserRuleContext context)
        {
            return typeToHandler[context.GetType()].Handle(this, context);
        }
    }
}