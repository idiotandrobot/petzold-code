using System.Collections;
using System.Collections.Generic;

namespace Petzold.Code
{
    public class MorseChar : IEnumerable<bool>
    {
        private readonly List<bool> Sequence = new List<bool>();

        public bool? Signal1 { get { return Sequence.Count > 0 ? Sequence[0] as bool? : null; } }
        public bool? Signal2 { get { return Sequence.Count > 1 ? Sequence[1] as bool? : null; } }
        public bool? Signal3 { get { return Sequence.Count > 2 ? Sequence[2] as bool? : null; } }
        public bool? Signal4 { get { return Sequence.Count > 3 ? Sequence[3] as bool? : null; } }

        public MorseChar()
        {

        }

        public MorseChar(bool signal1, bool? signal2 = null, bool? signal3 = null, bool? signal4 = null)
        {
            Sequence.Add(signal1);
            if (signal2.HasValue) Sequence.Add(signal2.Value);
            if (signal3.HasValue) Sequence.Add(signal3.Value);
            if (signal4.HasValue) Sequence.Add(signal4.Value);
        }

        public MorseChar(bool[] sequence)
        {
            //needs guarding for invalid sequence
            foreach (var code in sequence)
            {
                Sequence.Add(code);
            }
        }

        public MorseChar(string sequence)
        {
            //needs guarding for invalid sequence
            foreach (var code in sequence.ToCharArray())
            {
                switch (code)
                {
                    case '.':
                        Sequence.Add(false);
                        break;
                    case '-':
                        Sequence.Add(true);
                        break;
                    default:
                        break;
                }
            }
        }

        public IEnumerator<bool> GetEnumerator()
        {
            return Sequence.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Sequence.GetEnumerator();
        }

        public static MorseChar A = new MorseChar(".-");
        public static MorseChar B = new MorseChar("-...");
        public static MorseChar C = new MorseChar("-.-.");
        public static MorseChar D = new MorseChar("-..");
        public static MorseChar E = new MorseChar(".");
        public static MorseChar F = new MorseChar("..-.");
        public static MorseChar G = new MorseChar("--.");
        public static MorseChar H = new MorseChar("....");
        public static MorseChar I = new MorseChar("..");
        public static MorseChar J = new MorseChar(".---");
        public static MorseChar K = new MorseChar("-.-");
        public static MorseChar L = new MorseChar(".-..");
        public static MorseChar M = new MorseChar("--");
        public static MorseChar N = new MorseChar("-.");
        public static MorseChar O = new MorseChar("---");
        public static MorseChar P = new MorseChar(".--.");
        public static MorseChar Q = new MorseChar("--.-");
        public static MorseChar R = new MorseChar(".-.");
        public static MorseChar S = new MorseChar("...");
        public static MorseChar T = new MorseChar("-");
        public static MorseChar U = new MorseChar("..-");
        public static MorseChar V = new MorseChar("...-");
        public static MorseChar W = new MorseChar(".--");
        public static MorseChar X = new MorseChar("-..-");
        public static MorseChar Y = new MorseChar("-.--");
        public static MorseChar Z = new MorseChar("--..");
        public static MorseChar Empty = new MorseChar();
    }
}
