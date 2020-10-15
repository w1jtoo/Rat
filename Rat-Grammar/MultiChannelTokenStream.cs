using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;

namespace Rat_Grammar
{
public class MultiChannelTokenStream : BufferedTokenStream
    {
        private List<int> _channels = new List<int> {TokenConstants.DefaultChannel};

        public MultiChannelTokenStream(ITokenSource tokenSource) : base(tokenSource)
        {
        }

        public void Enable(int channel)
        {
            foreach (var existingChannel in _channels)
            {
                if (channel == existingChannel)
                {
                    return;
                }
            }

            _channels.Add(channel);

            var i = p - 1;
            while (i >= 0)
            {
                var token = tokens[i];
                if (token.Channel == channel || !Matches(token.Channel, _channels))
                {
                    i--;
                }
                else
                {
                    break;
                }
            }

            p = i + 1;
        }

        public void Disable(int channel)
        {
            var remainder = 0;
            for (var i = 0; i < _channels.Count; i++)
            {
                if (_channels[i] != channel)
                {
                    _channels[remainder] = _channels[i];
                    remainder++;
                }
            }

            _channels.Remove(_channels.Count - 1);
        }

        protected override int AdjustSeekIndex(int i)
        {
            return nextTokenOnChannel(i, _channels);
        }

        protected override IToken Lb(int k)
        {
            if (k == 0 || (p - k) < 0)
            {
                return null;
            }

            int i = p;
            for (int n = 1; n <= k; n++)
            {
                i = previousTokenOnChannel(i - 1, _channels);
            }

            if (i < 0)
            {
                return null;
            }

            return tokens[i];
        }

        public override IToken LT(int k)
        {
            LazyInit();

            if (k == 0)
            {
                return null;
            }

            if (k < 0)
            {
                return Lb(-k);
            }

            int i = p;
            for (int n = 1; n < k; n++)
            {
                if (Sync(i + 1))
                {
                    i = nextTokenOnChannel(i + 1, _channels);
                }
            }

            return tokens[i];
        }

        public int GetNumberOfOnChannelTokens()
        {
            var n = 0;
            Fill();
            foreach (var t in tokens)
            {
                foreach (var channel in _channels)
                {
                    if (t.Channel == channel)
                    {
                        n++;
                    }
                }

                if (t.Type == TokenConstants.EOF)
                {
                    break;
                }
            }

            return n;
        }

        protected int nextTokenOnChannel(int i, List<int> channels)
        {
            Sync(i);
            if (i >= Size)
            {
                return Size - 1;
            }

            var token = tokens[i];
            while (!Matches(token.Channel, channels))
            {
                if (token.Type == TokenConstants.EOF)
                {
                    return i;
                }

                i++;
                Sync(i);
                token = tokens[i];
            }

            return i;
        }

        protected int previousTokenOnChannel(int i, List<int> channels)
        {
            Sync(i);
            if (i >= Size)
            {
                return Size - 1;
            }

            while (i >= 0)
            {
                var token = tokens[i];
                if (token.Type == TokenConstants.EOF || Matches(token.Channel, channels))
                {
                    return i;
                }

                i--;
            }

            return i;
        }

        private static bool Matches(int channel, IEnumerable<int> channels)
        {
            return channels.Any(matchChannel => matchChannel == channel);
        }
    }
}