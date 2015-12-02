using PropertyChanged;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;

namespace Braille
{
    [ImplementPropertyChanged]
    public class BrailleFormatting
    {
        public int FontSize { get; set; }
        public Color Color { get; set; }
        public bool ShowBlanks { get; set; }

        public BrailleFormatting()
        {

        }
        
        public BrailleFormatting(int fontSize, Color color, bool showBlanks)
        {
            FontSize = fontSize;
            Color = color;
            ShowBlanks = showBlanks;

#if DEBUG
            var debug = this as INotifyPropertyChanged;
            if (debug != null) debug.PropertyChanged += (s, e) => { Debug.WriteLine("BrailleFormatting." + e.PropertyName); };
#endif
        }

        Brush _Brush = null;
        public Brush Brush
        {
            get { return _Brush ?? (Brush = new SolidBrush(Color)); }
            private set { _Brush = value; }
        }

        int? _BaseDotSize;
        int BaseDotSize
        {
            get { return _BaseDotSize ?? (BaseDotSize = FontSize - 2); }
            set { _BaseDotSize = value; }
        }

        int? _Padding = null;
        public int Padding
        {
            get { return _Padding ?? (Padding = BaseDotSize / 2); }
            private set { _Padding = value; }
        }

        int? _Width = null;
        public int Width
        {
            get { return _Width ?? (Width = (FontSize * 2) + (Padding * 2)); }
            private set { _Width = value; }
        }

        int? _Height = null;
        public int Height
        {
            get { return _Height ?? (Height = (FontSize * 3) + (Padding * 2)); }
            private set { _Height = value; }
        }
        
        int DotScale = 2;

        int? _dotSize = null;
        int dotSize
        {
            get { return _dotSize ?? (dotSize = (int)(BaseDotSize / DotScale)); }
            set { _dotSize = value; }
        }

        Size? _DotSize;
        public Size DotSize
        {
            get { return _DotSize ?? (DotSize = new Size(dotSize, dotSize)); }
            private set { _DotSize = value; }
        }

        int? _DotLocation;
        int DotLocation
        {
            get { return _DotLocation ?? (DotLocation = (int)((BaseDotSize - dotSize) / 2)); }
            set { _DotLocation = value; }
        }

        public Point GetDotLocation(int x, int y)
        {
            return new Point(x + Padding + DotLocation, y + Padding + DotLocation);
        }

        int BlankScale = 4;

        int? _blankSize;
        int blankSize
        {
            get { return _blankSize ?? (blankSize = (int)(BaseDotSize / BlankScale)); }
            set { _blankSize = value; }
        }

        Size? _BlankSize;
        public Size BlankSize
        {
            get { return _BlankSize ?? (BlankSize = new Size(blankSize, blankSize)); }
            set { _BlankSize = value; }
        }
        
        int? _BlankLocation;
        int BlankLocation
        {
            get { return _BlankLocation ?? (BlankLocation = (int)((BaseDotSize - blankSize) / 2)); }
            set { _BlankLocation = value; }
        }
        
        public Point GetBlankLocation(int x, int y)
        {
            return new Point(x + Padding + BlankLocation, y + Padding + BlankLocation);
        }
    }
}
