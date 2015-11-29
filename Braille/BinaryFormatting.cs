using System.Drawing;

namespace Braille
{
    public class BinaryFormatting
    {
        public int FontSize { get; private set; }
        public int Padding { get; private set; }

        public Font Font { get; private set; }
        public Brush Brush { get; private set; }

        public BinaryFormatting(string fontName, int fontSize, Color color)
        {
            FontSize = fontSize;
            Padding = (FontSize - 2) / 2;

            Font = new Font(fontName, FontSize / 3);
            Brush = new SolidBrush(color);
        }

        public Point GetLocation(int x, int y)
        {
            return new Point(
                x + Padding + FontSize / 2,
                y + Padding - FontSize / 4);
        }
    }
}