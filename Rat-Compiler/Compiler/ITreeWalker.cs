using RatLangGrammar;

namespace Rat_Compiler.Compiler
{
    public interface ITreeWalker
    {
        void Walk(RatParser parser);
    }
}