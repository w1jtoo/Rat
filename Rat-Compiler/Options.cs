using CommandLine;

namespace Rat_Compiler
{
    public class Options
    {
        // TODO
        [Option('v', "verbose", Required = false, HelpText = "Write hint")]
        public bool Verbose { get; set; }
    }
}