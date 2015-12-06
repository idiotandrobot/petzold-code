using PropertyChanged;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;

namespace Braille
{
    [ImplementPropertyChanged]
    public class BinaryFormatting
    {
        public static string DefaultFontName = "Courier New";

        string _FontName = null;
        public string FontName
        {
            get { return _FontName ?? (FontName = DefaultFontName); }
            set { _FontName = value; }
        }

        public static int DefaultFontSize = CodeFormatting.DefaultFontSize;

        int? _FontSize = null;
        public int FontSize
        {
            get { return _FontSize ?? (FontSize = DefaultFontSize); }
            set { _FontSize = value; }
        }

        public static Color DefaultColor = Color.DarkCyan;

        Color? _Color = null;
        public Color Color
        {
            get { return _Color ?? (Color = DefaultColor); }
            set { _Color = value; }
        }

        int? _Padding;
        public int Padding
        {
            get { return _Padding ?? (Padding = (FontSize - 2) / 2); }
            private set { _Padding = value; }
        }

        Font _Font;
        public Font Font
        {
            get { return _Font ?? (Font = new Font(FontName, FontSize / 3)); }
            private set { _Font = value; }
        }

        Brush _Brush;
        public Brush Brush
        {
            get { return _Brush ?? (Brush = new SolidBrush(Color)); }
            private set { _Brush = value; }
        }

        public BinaryFormatting()
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
                            _Font = null;
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
            if (debug != null) debug.PropertyChanged += (s, e) => { Debug.WriteLine("BinaryFormatting." + e.PropertyName); };
#endif
        }

        public Point GetLocation(int x, int y)
        {
            return new Point(
                x + Padding + FontSize / 2,
                y + Padding - FontSize / 4);
        }
    }
}