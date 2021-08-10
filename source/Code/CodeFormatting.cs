using PropertyChanged;
using System.ComponentModel;
using System.Drawing;

namespace Code
{
    [AddINotifyPropertyChangedInterface]
    public class CodeFormatting
    {
        public readonly static int DefaultFontSize = 42;

        private int? _FontSize = null;
        public int FontSize
        {
            get => _FontSize ??= DefaultFontSize; 
            set => _FontSize = value; 
        }

        public readonly static Color DefaultBackColor = Color.White;

        private Color? _BackColor = null;
        public Color BackColor
        {
            get => _BackColor ??= DefaultBackColor; 
            set => _BackColor = value; 
        }

        private BrailleFormatting _Braille = null;
        public BrailleFormatting Braille
        {
            get => _Braille ??= new BrailleFormatting(); 
            set => _Braille = value; 
        }

        private MorseFormatting _Morse = null;
        public MorseFormatting Morse
        {
            get => _Morse ??= new MorseFormatting(); 
            set => _Morse = value; 
        }

        private BinaryFormatting _Binary = null;
        public BinaryFormatting Binary
        {
            get => _Binary ??= new BinaryFormatting(); 
            set => _Binary = value; 
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
