using System;
using System.Linq;
using System.Text;

namespace Petzold.Code
{
    public static class CharExtensions
    {
        public static BrailleChar ToBraille(this Char chr)
        {
            switch (chr)
            {
                case 'a':
                case 'A':
                    return BrailleChar.A;
                case 'b':
                case 'B':
                    return BrailleChar.B;
                case 'c':
                case 'C':
                    return BrailleChar.C;
                case 'd':
                case 'D':
                    return BrailleChar.D;
                case 'e':
                case 'E':
                    return BrailleChar.E;
                case 'f':
                case 'F':
                    return BrailleChar.F;
                case 'g':
                case 'G':
                    return BrailleChar.G;
                case 'h':
                case 'H':
                    return BrailleChar.H;
                case 'i':
                case 'I':
                    return BrailleChar.I;
                case 'j':
                case 'J':
                    return BrailleChar.J;
                case 'k':
                case 'K':
                    return BrailleChar.K;
                case 'l':
                case 'L':
                    return BrailleChar.L;
                case 'm':
                case 'M':
                    return BrailleChar.M;
                case 'n':
                case 'N':
                    return BrailleChar.N;
                case 'o':
                case 'O':
                    return BrailleChar.O;
                case 'p':
                case 'P':
                    return BrailleChar.P;
                case 'q':
                case 'Q':
                    return BrailleChar.Q;
                case 'r':
                case 'R':
                    return BrailleChar.R;
                case 's':
                case 'S':
                    return BrailleChar.S;
                case 't':
                case 'T':
                    return BrailleChar.T;
                case 'u':
                case 'U':
                    return BrailleChar.U;
                case 'v':
                case 'V':
                    return BrailleChar.V;
                case 'w':
                case 'W':
                    return BrailleChar.W;
                case 'x':
                case 'X':
                    return BrailleChar.X;
                case 'y':
                case 'Y':
                    return BrailleChar.Y;
                case 'z':
                case 'Z':
                    return BrailleChar.Z;
                case '1':
                    return new BrailleChar("010000");
                case '2':
                    return new BrailleChar("011000");
                case '3':
                    return new BrailleChar("010010");
                case '4':
                    return new BrailleChar("010011");
                case '5':
                    return new BrailleChar("010001");
                case '6':
                    return new BrailleChar("011010");
                case '7':
                    return new BrailleChar("011011");
                case '8':
                    return new BrailleChar("011001");
                case '9':
                    return new BrailleChar("001010");
                case '0':
                    return new BrailleChar("001011");
                case '&':
                    return new BrailleChar("111101");
                case '=':
                    return new BrailleChar("111111");
                case '(':
                    return new BrailleChar("111011");
                case '!':
                    return new BrailleChar("011101");
                case ')':
                    return new BrailleChar("011111");
                case '*':
                    return new BrailleChar("100001");
                case '<':
                    return new BrailleChar("110001");
                case '%':
                    return new BrailleChar("100101");
                case '?':
                    return new BrailleChar("100111");
                case ':':
                    return new BrailleChar("100011");
                case '$':
                    return new BrailleChar("110101");
                case ']':
                    return new BrailleChar("110111");
                case '\\':
                    return new BrailleChar("110011");
                case '[':
                    return new BrailleChar("010101");
                case '/':
                    return new BrailleChar("001100");
                case '+':
                    return new BrailleChar("001101");
                case '#':
                    return new BrailleChar("001111");
                case '>':
                    return new BrailleChar("001110");
                case '\'':
                    return new BrailleChar("001000");
                case '-':
                    return new BrailleChar("001001");
                case '@':
                    return new BrailleChar("000100");
                case '^':
                    return new BrailleChar("000110");
                case '_':
                    return new BrailleChar("000111");
                case '\"':
                    return new BrailleChar("000010");
                case '.':
                    return new BrailleChar("000101");
                case ';':
                    return new BrailleChar("000011");
                case ',':
                    return new BrailleChar("000001");
                default:
                    return BrailleChar.Empty;
            }
        }

        public static MorseChar ToMorse(this Char chr)
        {
            switch (chr)
            {
                case 'a':
                case 'A':
                    return MorseChar.A;
                case 'b':
                case 'B':
                    return MorseChar.B;
                case 'c':
                case 'C':
                    return MorseChar.C;
                case 'd':
                case 'D':
                    return MorseChar.D;
                case 'e':
                case 'E':
                    return MorseChar.E;
                case 'f':
                case 'F':
                    return MorseChar.F;
                case 'g':
                case 'G':
                    return MorseChar.G;
                case 'h':
                case 'H':
                    return MorseChar.H;
                case 'i':
                case 'I':
                    return MorseChar.I;
                case 'j':
                case 'J':
                    return MorseChar.J;
                case 'k':
                case 'K':
                    return MorseChar.K;
                case 'l':
                case 'L':
                    return MorseChar.L;
                case 'm':
                case 'M':
                    return MorseChar.M;
                case 'n':
                case 'N':
                    return MorseChar.N;
                case 'o':
                case 'O':
                    return MorseChar.O;
                case 'p':
                case 'P':
                    return MorseChar.P;
                case 'q':
                case 'Q':
                    return MorseChar.Q;
                case 'r':
                case 'R':
                    return MorseChar.R;
                case 's':
                case 'S':
                    return MorseChar.S;
                case 't':
                case 'T':
                    return MorseChar.T;
                case 'u':
                case 'U':
                    return MorseChar.U;
                case 'v':
                case 'V':
                    return MorseChar.V;
                case 'w':
                case 'W':
                    return MorseChar.W;
                case 'x':
                case 'X':
                    return MorseChar.X;
                case 'y':
                case 'Y':
                    return MorseChar.Y;
                case 'z':
                case 'Z':
                    return MorseChar.Z;
                case '1':
                    return new MorseChar(".----");
                case '2':
                    return new MorseChar("..---");
                case '3':
                    return new MorseChar("...--");
                case '4':
                    return new MorseChar("....-");
                case '5':
                    return new MorseChar(".....");
                case '6':
                    return new MorseChar("-....");
                case '7':
                    return new MorseChar("--...");
                case '8':
                    return new MorseChar("---..");
                case '9':
                    return new MorseChar("----.");
                case '0':
                    return new MorseChar("-----");
                default:
                    return MorseChar.Empty;
            }
        }

        public static string ToBinaryString(this Char chr)
        {
            return string.Join(" ", new ASCIIEncoding().GetBytes(new char[] { chr }).Select(byt => Convert.ToString(byt, 2)));
        }
    }
}
