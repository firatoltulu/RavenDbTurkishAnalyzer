using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Tokenattributes;
using Lucene.Net.Util;

namespace TurkishAnalyzer
{
    public sealed class TurkishAsciiFoldingFilter : TokenFilter
    {
        public TurkishAsciiFoldingFilter(TokenStream input) : base(input)
        {
            termAtt = AddAttribute<ITermAttribute>();
        }

        private char[] output = new char[512];
        private int outputPos;
        private readonly ITermAttribute termAtt;

        public override bool IncrementToken()
        {
            if (input.IncrementToken())
            {
                char[] buffer = termAtt.TermBuffer();
                int length = termAtt.TermLength();

                for (int i = 0; i < length; ++i)
                {
                    char c = buffer[i];
                    if (c >= '\u0080')
                    {
                        FoldToASCII(buffer, length);
                        termAtt.SetTermBuffer(output, 0, outputPos);
                        break;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public void FoldToASCII(char[] input, int length)
        {
            // Worst-case length required:
            int maxSizeNeeded = 4 * length;
            if (output.Length < maxSizeNeeded)
            {
                output = new char[ArrayUtil.GetNextSize(maxSizeNeeded)];
            }

            outputPos = 0;

            for (int pos = 0; pos < length; ++pos)
            {
                char c = input[pos];
                if (c < '\u0080')
                {
                    output[outputPos++] = c;
                }
                else
                {
                    switch (c)
                    {
                        case '\u0049':
                        case '\u0130':
                            output[outputPos++] = 'I';
                            break;

                        case '\u0069':
                        case '\u0131':
                            output[outputPos++] = 'i';
                            break;

                        default:
                            output[outputPos++] = c;
                            break;
                    }
                }
            }
        }
    }
}