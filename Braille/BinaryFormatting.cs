using PropertyChanged;
using System.Drawing;

namespace Braille
{
    [ImplementPropertyChanged]
    public class BinaryFormatting
    {
        public string FontName { get; set; }
        public int FontSize { get; set; }
        public Color Color { get; set; }

        int? _Padding = null;
        public int Padding
        {
            get { return _Padding ?? (Padding = (FontSize - 2) / 2); }
            private set { _Padding = value; }
        }

        Font _Font = null;
        public Font Font
        {
            get { return _Font ?? (Font = new Font(FontName, FontSize / 3)); }
            private set { _Font = value; }
        }

        Brush _Brush = null;
        public Brush Brush
        {
            get { return _Brush ?? (Brush = new SolidBrush(Color)); }
            private set { _Brush = value; }
        }

        public BinaryFormatting() { }

        public BinaryFormatting(string fontName, int fontSize, Color color)
        {
            FontName = fontName;
            FontSize = fontSize;
            Color = color;
        }

        public Point GetLocation(int x, int y)
        {
            return new Point(
                x + Padding + FontSize / 2,
                y + Padding - FontSize / 4);
        }
    }
}