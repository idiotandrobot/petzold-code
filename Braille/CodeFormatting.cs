using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Braille
{
    public class CodeFormatting
    {
        public BrailleFormatting Braille { get; private set; }
        public MorseFormatting Morse { get; private set; }
        public BinaryFormatting Binary { get; private set; }

        public Color BackColor { get; private set; }

        public CodeFormatting(
            int fontSize,
            Color backColor,
            Color brailleColor,
            Color morseColor,
            Color binaryColor,
            bool showBlanks)
        {
            BackColor = backColor;

            Braille = new BrailleFormatting(fontSize, brailleColor, showBlanks);
            Morse = new MorseFormatting(fontSize, morseColor);
            Binary = new BinaryFormatting("Courier New", fontSize, binaryColor);
        }
    }
}
