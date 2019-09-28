using PropertyChanged;
using System.ComponentModel;
using System.Drawing;

namespace Code
{
    [AddINotifyPropertyChangedInterface]
    public class CodeFormatting
    {
        public static int DefaultFontSize = 42;

        int? _FontSize = null;
        public int FontSize
        {
            get { return _FontSize ?? (FontSize = DefaultFontSize); }
            set { _FontSize = value; }
        }

        public static Color DefaultBackColor = Color.White;

        Color? _BackColor = null;
        public Color BackColor
        {
            get { return _BackColor ?? (BackColor = DefaultBackColor); }
            set { _BackColor = value; }
        }

        BrailleFormatting _Braille = null;
        public BrailleFormatting Braille
        {
            get { return _Braille ?? (Braille = new BrailleFormatting()); }
            set { _Braille = value; }
        }

        MorseFormatting _Morse = null;
        public MorseFormatting Morse
        {
            get { return _Morse ?? (Morse = new MorseFormatting()); }
            set { _Morse = value; }
        }

        BinaryFormatting _Binary = null;
        public BinaryFormatting Binary
        {
            get { return _Binary ?? (Binary = new BinaryFormatting()); }
            set { _Binary = value; }
        }

        public CodeFormatting()
        {
            (this as INotifyPropertyChanged).PropertyChanged += (s, e) => 
            {
                if (e.PropertyName == "FontSize")
                {
                    Braille.FontSize = FontSize;
                    Morse.FontSize = FontSize;
                    Binary.FontSize = FontSize;
                }
            };
        }
    }
}
