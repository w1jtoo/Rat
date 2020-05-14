namespace Rat_Compiler
{
    public interface IRat
    {
        void Compile();
        string GetVersion();
        void InitDirectory();
    }
}