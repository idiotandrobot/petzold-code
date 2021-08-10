using PropertyChanged;
using System.ComponentModel;
using System.Drawing;

namespace Code
{
    [AddINotifyPropertyChangedInterface]
    public class BrailleFormatting
    {
        public readonly static int DefaultFontSize = CodeFormatting.DefaultFontSize;

        private int? _FontSize = null;
        public int FontSize
        {
            get => _FontSize ??= DefaultFontSize; 
            set => _FontSize = value; 
        }

        public readonly static Color DefaultColor = Color.RoyalBlue;

        private Color? _Color = null;
        public Color Color
        {
            get => _Color ??= DefaultColor; 
            set => _Color = value; 
        }

        public readonly static bool DefaultShowBlanks = true;

        private bool? _ShowBlanks = null;
        public bool ShowBlanks
        {
            get => _ShowBlanks ??= DefaultShowBlanks; 
            set => _ShowBlanks = value; 
        }
        
        public BrailleFormatting()
        {
            (this as INotifyPropertyChanged).PropertyChanged += (s, e) =>
            {
                switch (e.PropertyName)
                {
                    case "FontSize":                            
                        _BaseDotSize = null;
                        _Padding = null;                            
                        _Width = null;
                        _Height = null;
                        _ScaledDotSize = null;
                        _DotSize = null;
                        _DotLocation = null;
                        _ScaledBlankSize = null;
                        _BlankSize = null;
                        _BlankLocation = null;
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

        private int? _BaseDotSize;
        private int BaseDotSize
        {
            get => _BaseDotSize ??= FontSize - 2; 
            set => _BaseDotSize = value; 
        }

        private int? _Padding;
        public int Padding
        {
            get => _Padding ??= BaseDotSize / 2; 
            private set => _Padding = value; 
        }

        private int? _Width;
        public int Width
        {
            get => _Width ??= (FontSize * 2) + (Padding * 2); 
            private set => _Width = value; 
        }

        private int? _Height;
        public int Height
        {
            get => _Height ??= (FontSize * 3) + (Padding * 2); 
            private set => _Height = value; 
        }

        private readonly int DotScale = 2;

        private int? _ScaledDotSize;
        private int ScaledDotSize => _ScaledDotSize ??= (BaseDotSize / DotScale);  

        private Size? _DotSize;
        public Size DotSize
        {
            get => _DotSize ??= new Size(ScaledDotSize, ScaledDotSize);
            private set => _DotSize = value; 
        }

        private int? _DotLocation;
        private int DotLocation
        {
            get => _DotLocation ??= (BaseDotSize - ScaledDotSize) / 2; 
            set => _DotLocation = value; 
        }

        public Point GetDotLocation(int x, int y) => new(x + Padding + DotLocation, y + Padding + DotLocation);

        private readonly int BlankScale = 4;

        private int? _ScaledBlankSize;
        private int ScaledBlankSize => _ScaledBlankSize ??= (BaseDotSize / BlankScale); 

        private Size? _BlankSize;
        public Size BlankSize
        {
            get => _BlankSize ??= new Size(ScaledBlankSize, ScaledBlankSize); 
            set => _BlankSize = value; 
        }
        
        private int? _BlankLocation;
        private int BlankLocation
        {
            get => _BlankLocation ??= (BaseDotSize - ScaledBlankSize) / 2; 
            set => _BlankLocation = value; 
        }
        
        public Point GetBlankLocation(int x, int y) => new(x + Padding + BlankLocation, y + Padding + BlankLocation);
    }
}
