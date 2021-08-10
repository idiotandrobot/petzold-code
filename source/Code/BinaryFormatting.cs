using PropertyChanged;
using System.ComponentModel;
using System.Drawing;

namespace Code
{
    [AddINotifyPropertyChangedInterface]
    public class BinaryFormatting
    {
        public readonly static string DefaultFontName = "Courier New";

        private string _FontName = null;
        public string FontName
        {
            get => _FontName ??= DefaultFontName; 
            set => _FontName = value; 
        }

        public readonly static int DefaultFontSize = CodeFormatting.DefaultFontSize;

        private int? _FontSize = null;
        public int FontSize
        {
            get => _FontSize ??= DefaultFontSize; 
            set => _FontSize = value; 
        }

        public readonly static Color DefaultColor = Color.DarkCyan;

        private Color? _Color = null;
        public Color Color
        {
            get => _Color ??= DefaultColor; 
            set => _Color = value; 
        }

        private int? _Padding;
        public int Padding
        {
            get => _Padding ??= (FontSize - 2) / 2; 
            private set => _Padding = value; 
        }

        private Font _Font;
        public Font Font
        {
            get => _Font ??= new Font(FontName, FontSize / 3); 
            private set => _Font = value; 
        }

        private Brush _Brush;
        public Brush Brush
        {
            get => _Brush ??= new SolidBrush(Color); 
            private set => _Brush = value; 
        }

        public BinaryFormatting()
        { 
            (this as INotifyPropertyChanged).PropertyChanged += (s, e) =>
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

        public Point GetLocation(int x, int y)
        {
            return new Point(
                x + Padding + FontSize / 2,
                y + Padding - FontSize / 4);
        }
    }
}