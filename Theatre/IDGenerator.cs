using System.Text;

namespace Generators
{
    public interface IDGenerator
    {
        string Next();
        void Reset();
    }

    public class BaseAlphaGenerator : IDGenerator
    {
        int idx = 0;
        string alphabet;

        public BaseAlphaGenerator(string alphabet)
        {
            this.alphabet = alphabet;
        }

        public string Next()
        {
            StringBuilder sb = new StringBuilder();
            var i = idx;
            var l = alphabet.Length;
            while (i >= l)
            {
                sb.Insert(0, alphabet[i % l]);
                i /= l;
            }
            sb.Insert(0, alphabet[i]);
            idx += 1;
            return sb.ToString();
        }

        public void Reset()
        {
            idx = 0;
        }
    }

    public class AlphabeticalGenerator : BaseAlphaGenerator
    {
        public AlphabeticalGenerator() : base("ABCDEFGHIJKLMNOPQRSTUVWXYZ")
        {
        }
    }

    public class NumericalGenerator : IDGenerator
    {
        int idx;

        public NumericalGenerator(int start = 0)
        {
            idx = start;
        }

        public string Next()
        {
            var next = idx;
            idx += 1;
            return next.ToString();
        }

        public void Reset()
        {
            idx = 0;
        }
    }
}
