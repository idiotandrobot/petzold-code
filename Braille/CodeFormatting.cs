using PropertyChanged;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;

namespace Braille
{
    [ImplementPropertyChanged]
    public class CodeFormatting
    {
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

        public int FontSize { get; set; }
        public Color BackColor { get; private set; }

        public CodeFormatting() {  }

        public CodeFormatting(
            int fontSize,
            Color backColor,
            Color brailleColor,
            Color morseColor,
            Color binaryColor,
            bool showBlanks)
        {
            FontSize = fontSize;
            BackColor = backColor;

            Braille = new BrailleFormatting(fontSize, brailleColor, showBlanks);
            Morse = new MorseFormatting(fontSize, morseColor);
            Binary = new BinaryFormatting("Courier New", fontSize, binaryColor);

#if DEBUG
            var debug = this as INotifyPropertyChanged;
            if (debug != null) debug.PropertyChanged += (s, e) => { Debug.WriteLine("CodeFormatting." + e.PropertyName); };
#endif
        }
    }
}
