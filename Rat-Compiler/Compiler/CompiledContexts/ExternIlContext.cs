namespace Rat_Compiler.Compiler.CompiledContexts
{
    public class ExternIlContext : ICompiledContext
    {
        public string Filename { get; }
        public string[] Lines { get; }

        public ExternIlContext(string filename, string[] lines)
        {
            Filename = filename;
            Lines = lines;
        }

        public void Write()
        {
            throw new System.NotImplementedException();
        }
    }
}