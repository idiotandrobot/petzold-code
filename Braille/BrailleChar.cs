using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Braille
{
    public class BrailleChar : IEnumerable<bool>
    {
        List<bool> Sequence = new List<bool> { false, false, false, false, false, false, };

        public bool C1R1 { get; private set; }
        public bool C2R1 { get; private set; }
        public bool C1R2 { get; private set; }
        public bool C2R2 { get; private set; }
        public bool C1R3 { get; private set; }
        public bool C2R3 { get; private set; }

        public BrailleChar()
        {

        }

        public BrailleChar(string sequence)
        {
            //Should really validate sequence string
            var array = sequence.ToCharArray();
            for (int i = 0; i < Sequence.Count; i++)
            {
                if (array[i] == '1') Sequence[i] = true;
            }
        }

        public BrailleChar(bool c1r1, bool c2r1, bool c1r2, bool c2r2, bool c1r3, bool c2r3)
        {
            Sequence[0] = c1r1;
            Sequence[1] = c2r1;
            Sequence[2] = c1r2;
            Sequence[3] = c2r2;
            Sequence[4] = c1r3;
            Sequence[5] = c2r3;
        }

        public IEnumerator<bool> GetEnumerator()
        {
            return Sequence.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Sequence.GetEnumerator();
        }

        public static BrailleChar A = new BrailleChar("100000");
        public static BrailleChar B = new BrailleChar("110000");
        public static BrailleChar C = new BrailleChar("100100");
        public static BrailleChar D = new BrailleChar("100110");
        public static BrailleChar E = new BrailleChar("100010");
        public static BrailleChar F = new BrailleChar("110100");
        public static BrailleChar G = new BrailleChar("110110");
        public static BrailleChar H = new BrailleChar("110010");
        public static BrailleChar I = new BrailleChar("010100");
        public static BrailleChar J = new BrailleChar("010110");
        public static BrailleChar K = new BrailleChar("101000");
        public static BrailleChar L = new BrailleChar("111000");
        public static BrailleChar M = new BrailleChar("101100");
        public static BrailleChar N = new BrailleChar("101110");
        public static BrailleChar O = new BrailleChar("101010");
        public static BrailleChar P = new BrailleChar("111100");
        public static BrailleChar Q = new BrailleChar("111110");
        public static BrailleChar R = new BrailleChar("111010");
        public static BrailleChar S = new BrailleChar("011100");
        public static BrailleChar T = new BrailleChar("011110");
        public static BrailleChar U = new BrailleChar("101001");
        public static BrailleChar V = new BrailleChar("111001");
        public static BrailleChar W = new BrailleChar("010111");
        public static BrailleChar X = new BrailleChar("101101");
        public static BrailleChar Y = new BrailleChar("101111");
        public static BrailleChar Z = new BrailleChar("101011");
        public static BrailleChar Empty = new BrailleChar();
    }
}
