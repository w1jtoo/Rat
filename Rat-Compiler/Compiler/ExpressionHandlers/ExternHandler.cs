using System;
using System.Linq;
using Antlr4.Runtime;
using log4net;
using Rat_Compiler.Compiler.CompiledContexts;
using Rat_Compiler.Compiler.GeneratorHelper;

namespace Rat_Compiler.Compiler.ExpressionHandlers
{
    public class ExternHandler : IExpressionHandler
    {
        public Type GetExpressionType() => typeof(RatParser.ExternstmtContext);

        public ICompiledContext Handle(IContextHandler contextHandler, ParserRuleContext ruleContext)
        {
            if (ruleContext is RatParser.ExternstmtContext externstmt)
            {
                var filename = TypeHelper.Unquoute(externstmt.filename.GetText());
                var lines = externstmt.lines();
                var lineCount = lines.ChildCount;
                var externs = Enumerable.Range(0, lineCount)
                    .Select(lines.GetChild)
                    .Select(t => TypeHelper.Unquoute(t.GetChild(0).GetText()))
                    .ToArray();
                return new ExternIlContext(filename, externs);
            }

            return null;
        }
    }
}