using System.IO;
using Antlr4.Runtime;

namespace Rat_Grammar
{
    public abstract class MultiChannelBaseParser : Parser
    {
        public MultiChannelBaseParser(ITokenStream input) : base(input)
        {
        }

        public MultiChannelBaseParser(ITokenStream input, TextWriter output, TextWriter errorOutput)
            : base(input, output, errorOutput)
        {
        }

        public void Enable(int channel)
        {
            if (this.InputStream is MultiChannelTokenStream tokenStream)
            {
                (tokenStream).Enable(channel);
            }
        }

        public void Disable(int channel)
        {
            if (this.InputStream is MultiChannelTokenStream tokenStream)
            {
                tokenStream.Disable(channel);
            }
        }
    }
}