using PropertyChanged;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;

namespace Braille
{
    [ImplementPropertyChanged]
    public class MorseFormatting
    {
        public int FontSize { get; set; }
        public Color Color { get; set; }

        public MorseFormatting() { }

        public MorseFormatting(int fontSize, Color color)
        {
            FontSize = fontSize;
            Color = color;
#if DEBUG
            var debug = this as INotifyPropertyChanged;
            if (debug != null) debug.PropertyChanged += (s, e) => { Debug.WriteLine("MorseFormatting." + e.PropertyName); };
#endif
        }

        Brush _Brush = null;
        public Brush Brush
        {
            get { return _Brush ?? (Brush = new SolidBrush(Color)); }
            private set { _Brush = value; }
        }

        int? _Padding = null;
        public int Padding
        {
            get { return _Padding ?? ((FontSize - 2) / 2); }
            private set { _Padding = value; }
        }

        int MorseScale = 3;

        int? _MorseScaleSize;
        int MorseScaleSize
        {
            get { return _MorseScaleSize ?? (MorseScaleSize = (int)((FontSize - 2) / MorseScale)); }
            set { _MorseScaleSize = value; }
        }

        Size? _DotSize = null;
        public Size DotSize
        {
            get { return _DotSize ?? (DotSize = new Size(MorseScaleSize, MorseScaleSize)); }
            private set { _DotSize = value; }
        }

        Size? _DashSize = null;
        public Size DashSize
        {
            get { return _DashSize ?? (DashSize = new Size(MorseScaleSize * 2, MorseScaleSize / 2)); }
            private set { _DashSize = value; }
        }

        int? _MorseScaleLocation = null;
        int MorseScaleLocation
        {
            get { return _MorseScaleLocation ?? (MorseScaleLocation = (int)(((FontSize - 2) - MorseScaleSize) / 2)); }
            set { _MorseScaleLocation = value; }
        }           
        
        public Point GetDashLocation(int x, int y, int sequenceWidth)
        {
            return new Point(
                x + Padding + MorseScaleLocation + FontSize + MorseScaleSize / 2 - (sequenceWidth / 2),
                y + Padding + MorseScaleLocation + DashSize.Height / 2);
        }

        public Point GetDotLocation(int x, int y, int sequenceWidth)
        {
            return new Point(
                 x + Padding + MorseScaleLocation + FontSize + MorseScaleSize / 2 - (sequenceWidth / 2),
                 y + Padding + MorseScaleLocation);
        }

        int? _SpaceWidth = null;
        public int SpaceWidth
        {
            get { return _SpaceWidth ?? (SpaceWidth = MorseScaleSize / 2); }
            private set { _SpaceWidth = value; }
        }
    }
}
