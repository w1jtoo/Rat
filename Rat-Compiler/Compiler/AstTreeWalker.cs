using System;
using System.Collections.Generic;
using System.Linq;
using Rat_Compiler.Compiler.ExpressionHandlers;

namespace Rat_Compiler.Compiler
{
    public class AstTreeWalker : ITreeWalker
    {
        private readonly Dictionary<Type, IExpressionHandler> typeToHandler;
        private readonly ICodeGenerator generator;

        public AstTreeWalker(IEnumerable<IExpressionHandler> handlers, ICodeGenerator generator)
        {
            this.generator = generator;
            typeToHandler = handlers
                .ToDictionary(el => el.GetExpressionType(), v => v);
        }

        public void Walk(ratParser parser)
        {
            foreach (var expr in parser.code().expressions())
            {
                typeToHandler[expr.GetType()].Handle(generator);
            }
        }
    }
}