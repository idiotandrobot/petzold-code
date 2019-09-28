using Petzold.Code;
using System.Drawing;

namespace Code
{
    public class CodeCharLayout
    {
        public char Value { get; private set; }
        public BrailleChar Braille { get; private set; }
        public MorseChar Morse { get; private set; }
        public string BinaryString { get; private set; }

        public Point Location { get; private set; }

        public int Left { get; private set; }
        public int Top { get; private set; }

        public BrailleCharLayout BrailleLayout { get; private set; }
        public MorseCharLayout MorseLayout { get; private set; }
        public BinaryCharLayout BinaryLayout { get; private set; }

        public CodeCharLayout(
            char c, 
            int left,
            int top,
            CodeFormatting formatting)
        {
            Value = c;

            Braille = c.ToBraille();
            Morse = c.ToMorse();
            BinaryString = c.ToBinaryString();

            Location = new Point(left, top);

            BrailleLayout = new BrailleCharLayout(Braille, formatting.Braille);
            MorseLayout = new MorseCharLayout(Morse, formatting.Morse);
            BinaryLayout = new BinaryCharLayout(BinaryString, formatting.Binary);
        }
    }
}
