using PropertyChanged;
using System.ComponentModel;
using System.Drawing;

namespace Code
{
    [AddINotifyPropertyChangedInterface]
    public class MorseFormatting
    {
        public readonly static int DefaultFontSize = CodeFormatting.DefaultFontSize;

        private int? _FontSize = null;
        public int FontSize
        {
            get => _FontSize ??= DefaultFontSize; 
            set => _FontSize = value; 
        }

        public readonly static Color DefaultColor = Color.Goldenrod;

        private Color? _Color = null;
        public Color Color
        {
            get => _Color ??= DefaultColor; 
            set => _Color = value; 
        }
        
        public MorseFormatting()
        {
            (this as INotifyPropertyChanged).PropertyChanged += (s, e) =>
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

        private Brush _Brush;
        public Brush Brush
        {
            get => _Brush ??= new SolidBrush(Color); 
            private set => _Brush = value; 
        }

        private int? _Padding;
        public int Padding => _Padding ??= (FontSize - 2) / 2; 

        private readonly int MorseScale = 3;

        private int? _MorseScaleSize;
        private int MorseScaleSize
        {
            get => _MorseScaleSize ??= (FontSize - 2) / MorseScale; 
            set => _MorseScaleSize = value; 
        }

        private Size? _DotSize;
        public Size DotSize => _DotSize ??= new(MorseScaleSize, MorseScaleSize); 
        
        private Size? _DashSize;
        public Size DashSize => _DashSize ??= new Size(MorseScaleSize * 2, MorseScaleSize / 2); 

        private int? _MorseScaleLocation;
        int MorseScaleLocation
        {
            get => _MorseScaleLocation ??= ((FontSize - 2) - MorseScaleSize) / 2; 
            set => _MorseScaleLocation = value; 
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

        private int? _SpaceWidth;
        public int SpaceWidth => _SpaceWidth ??= MorseScaleSize / 2; 
    }
}
