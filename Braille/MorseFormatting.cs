using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Braille
{
    public class MorseFormatting
    {
        int MorseScaleSize;
        int MorseScaleLocation;
        
        public int FontSize { get; private set; }
        public Brush Brush { get; private set; }

        public int Padding { get; private set; }

        public MorseFormatting(int fontSize, Color color)
        {
            FontSize = fontSize;
            Brush = new SolidBrush(color);

            int BaseDotSize = FontSize - 2;
            Padding = BaseDotSize / 2;

            int morseScale = 3;
            MorseScaleSize = (int)((FontSize - 2) / morseScale);

            DotSize = new Size(MorseScaleSize, MorseScaleSize);

            MorseScaleLocation = (int)(((FontSize - 2) - MorseScaleSize) / 2);

            DashSize = new Size(MorseScaleSize * 2, MorseScaleSize / 2);

            SpaceWidth = MorseScaleSize / 2;

        }

        public Point GetDashLocation(int x, int y, int sequenceWidth)
        {
            return new Point(
                x + Padding + MorseScaleLocation + FontSize + MorseScaleSize / 2 - (sequenceWidth / 2),
                y + Padding + MorseScaleLocation + DashSize.Height / 2);
        }

        public Size DashSize { get; private set; }

        public Point GetDotLocation(int x, int y, int sequenceWidth)
        {
            return new Point(
                 x + Padding + MorseScaleLocation + FontSize + MorseScaleSize / 2 - (sequenceWidth / 2),
                 y + Padding + MorseScaleLocation);
        }

        public Size DotSize { get; private set; }

        public int SpaceWidth { get; private set; }
    }
}
