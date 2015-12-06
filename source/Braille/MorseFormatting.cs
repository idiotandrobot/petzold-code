using PropertyChanged;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;

namespace Braille
{
    [ImplementPropertyChanged]
    public class MorseFormatting
    {
        public static int DefaultFontSize = CodeFormatting.DefaultFontSize;
        
        int? _FontSize = null;
        public int FontSize
        {
            get { return _FontSize ?? (FontSize = DefaultFontSize); }
            set { _FontSize = value; }
        }

        public static Color DefaultColor = Color.Goldenrod;

        Color? _Color = null;
        public Color Color
        {
            get { return _Color ?? (Color = DefaultColor); }
            set { _Color = value; }
        }
        
        public MorseFormatting()
        {
            var notify = this as INotifyPropertyChanged;
            if (notify != null)
            {
                notify.PropertyChanged += (s, e) =>
                {
                    switch (e.PropertyName)
                    {
                        case "FontSize":
                            _Padding = null;
                            _MorseScaleSize = null;
                            _DotSize = null;
                            _DashSize = null;
                            _MorseScaleLocation = null;
                            _SpaceWidth = null;
                            break;
                        case "Color":
                            _Brush = null;
                            break;
                        default:
                            break;
                    }

                };
            }
#if DEBUG
            var debug = this as INotifyPropertyChanged;
            if (debug != null) debug.PropertyChanged += (s, e) => { Debug.WriteLine("MorseFormatting." + e.PropertyName); };
#endif
        }

        Brush _Brush;
        public Brush Brush
        {
            get { return _Brush ?? (Brush = new SolidBrush(Color)); }
            private set { _Brush = value; }
        }

        int? _Padding;
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

        Size? _DotSize;
        public Size DotSize
        {
            get { return _DotSize ?? (DotSize = new Size(MorseScaleSize, MorseScaleSize)); }
            private set { _DotSize = value; }
        }

        Size? _DashSize;
        public Size DashSize
        {
            get { return _DashSize ?? (DashSize = new Size(MorseScaleSize * 2, MorseScaleSize / 2)); }
            private set { _DashSize = value; }
        }

        int? _MorseScaleLocation;
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

        int? _SpaceWidth;
        public int SpaceWidth
        {
            get { return _SpaceWidth ?? (SpaceWidth = MorseScaleSize / 2); }
            private set { _SpaceWidth = value; }
        }
    }
}
