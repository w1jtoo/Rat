using System.Collections.Generic;
using Rat_Compiler.Compiler.CompiledContexts;
using Rat_Compiler.Compiler.CompiledContexts.Expressions;

namespace Rat_Compiler.Compiler.GeneratorHelper
{
    /// <summary>
    /// Common snippets of Il code
    /// </summary>
    public class IlGenerator : ICodeGenerator
    {
        public ExternIlContext Extern(string filename, IReadOnlyList<string> properties)
        {
            throw new System.NotImplementedException();
        }

        public FunctionDefinitionIlContext FunctionDefinition(string id, IReadOnlyList<string> funcArgs, ExpressionsIlContext expressions)
        {
            throw new System.NotImplementedException();
        }

        public IfIlContext IfStatement(ExpressionIlContext expr, StatementBlockIlContext truth, StatementBlockIlContext lie)
        {
            throw new System.NotImplementedException();
        }
    }
}