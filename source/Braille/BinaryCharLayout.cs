using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Braille
{
    public class BinaryCharLayout
    {
        public string Value { get; private set; }
        public Point Location { get; private set; }

        public BinaryCharLayout(string binaryString, BinaryFormatting formatting)
        {
            Value = binaryString;

            int x = -formatting.FontSize / 2;
            int y = formatting.FontSize * 2;

            Location = formatting.GetLocation(x, y);
        }
    }
}
