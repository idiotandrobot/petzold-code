using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Braille
{
    public class BrailleChar : IEnumerable<bool>
    {
        bool[] Sequence = { false, false, false, false, false, false, };

        public bool Dot1 { get { return Sequence[0]; } }
        public bool Dot2 { get { return Sequence[1]; } }
        public bool Dot3 { get { return Sequence[2]; } }
        public bool Dot4 { get { return Sequence[3]; } }
        public bool Dot5 { get { return Sequence[4]; } }
        public bool Dot6 { get { return Sequence[5]; } }

        public BrailleChar()
        {

        }

        public BrailleChar(string sequence)
        {
            //Should really validate sequence string
            var array = sequence.ToCharArray();
            for (int i = 0; i < Sequence.Length; i++)
            {
                if (array[i] == '1') Sequence[i] = true;
            }
        }

        public BrailleChar(bool dot1, bool dot2, bool dot3, bool dot4, bool dot5, bool dot6)
        {
            Sequence[0] = dot1;
            Sequence[1] = dot2;
            Sequence[2] = dot3;
            Sequence[3] = dot4;
            Sequence[4] = dot5;
            Sequence[5] = dot6;
        }

        public IEnumerator<bool> GetEnumerator()
        {
            return Sequence.AsEnumerable().GetEnumerator();
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
