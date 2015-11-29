using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Braille
{
    public class BrailleFormatting
    {
        int DotScale = 2;
        int DotLocation;

        int BlankScale = 4;
        int BlankLocation;

        public int FontSize { get; private set; }
        public Brush Brush { get; private set; }

        public int Padding { get; private set; }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public bool ShowBlanks { get; private set; }

        public BrailleFormatting(int fontSize, Color color, bool showBlanks)
        {
            FontSize = fontSize;
            Brush = new SolidBrush(color);

            ShowBlanks = showBlanks;

            int BaseDotSize = FontSize - 2;
            Padding = BaseDotSize / 2;

            Width = (FontSize * 2) + (Padding * 2);
            Height = (FontSize * 3) + (Padding * 2);

            int dotSize = (int)(BaseDotSize / DotScale);
            DotSize = new Size(dotSize, dotSize);
            DotLocation = (int)((BaseDotSize - dotSize) / 2);

            int blankSize = (int)(BaseDotSize / BlankScale);
            BlankSize = new Size(blankSize, blankSize);
            BlankLocation = (int)((BaseDotSize - blankSize) / 2);
        }

        public Point GetDotLocation(int x, int y)
        {
            return new Point(x + Padding + DotLocation, y + Padding + DotLocation);
        }

        public Size DotSize { get; private set;}

        public Point GetBlankLocation(int x, int y)
        {
            return new Point(x + Padding + BlankLocation, y + Padding + BlankLocation);
        }

        public Size BlankSize { get; private set; }
    }
}
