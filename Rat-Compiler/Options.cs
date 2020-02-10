using CommandLine;

namespace Rat_Compiler
{
    public class Options
    {
        [Option('f', "file_name", Required = false, HelpText = "File to compile.")]
        public string  FileName { get; set; }
    }
}