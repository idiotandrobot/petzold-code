﻿using PropertyChanged;
using System.ComponentModel;
using System.Drawing;

namespace Code
{
    [AddINotifyPropertyChangedInterface]
    public class BrailleFormatting
    {
        public static int DefaultFontSize = CodeFormatting.DefaultFontSize;

        int? _FontSize = null;
        public int FontSize
        {
            get { return _FontSize ?? (FontSize = DefaultFontSize); }
            set { _FontSize = value; }
        }

        public static Color DefaultColor = Color.RoyalBlue;

        Color? _Color = null;
        public Color Color
        {
            get { return _Color ?? (Color = DefaultColor); }
            set { _Color = value; }
        }

        public static bool DefaultShowBlanks = true;

        bool? _ShowBlanks = null;
        public bool ShowBlanks
        {
            get { return _ShowBlanks ?? (ShowBlanks = DefaultShowBlanks); }
            set { _ShowBlanks = value; }
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
                        _dotSize = null;
                        _DotSize = null;
                        _DotLocation = null;
                        _blankSize = null;
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

        Brush _Brush;
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

        int? _Padding;
        public int Padding
        {
            get { return _Padding ?? (Padding = BaseDotSize / 2); }
            private set { _Padding = value; }
        }

        int? _Width;
        public int Width
        {
            get { return _Width ?? (Width = (FontSize * 2) + (Padding * 2)); }
            private set { _Width = value; }
        }

        int? _Height;
        public int Height
        {
            get { return _Height ?? (Height = (FontSize * 3) + (Padding * 2)); }
            private set { _Height = value; }
        }
        
        int DotScale = 2;

        int? _dotSize;
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
