using System.Collections.Generic;
using Rat_Compiler.Compiler.CompiledContexts;
using Rat_Compiler.Compiler.CompiledContexts.Expressions;

namespace Rat_Compiler.Compiler.GeneratorHelper
{
    public interface ICodeGenerator
    {
        ExternIlContext Extern(string filename, IReadOnlyList<string> properties);

        FunctionDefinitionIlContext FunctionDefinition(string id, IReadOnlyList<string> funcArgs,
            ExpressionsIlContext expressions);

        IfIlContext IfStatement(ExpressionIlContext expr, StatementBlockIlContext truth,
            StatementBlockIlContext lie);
    }
}