using PropertyChanged;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Braille
{
    [ImplementPropertyChanged]
    public class CodePanel : Panel
    {
        CodeBox CodeBox = new CodeBox();

        CodeFormatting Formatting;

        public int FontSize
        {
            get { return Formatting.FontSize; }
            set { Formatting.FontSize = value; }
        }

        public Color BrailleColor
        {
            get { return Formatting.Braille.Color; }
            set { Formatting.Braille.Color = value; }
        }

        public Color MorseColor
        {
            get { return Formatting.Morse.Color; }
            set { Formatting.Morse.Color = value; }
        }

        public Color BinaryColor
        {
            get { return Formatting.Binary.Color; }
            set { Formatting.Binary.Color = value; }
        }

        public bool ShowBlanks
        {
            get { return Formatting.Braille.ShowBlanks; }
            set { Formatting.Braille.ShowBlanks = value; }
        }

        public new string Text { get; set; }

        public CodePanel()
        {
            base.AutoScroll = true;
            base.Controls.Add(CodeBox);
            base.Cursor = Cursors.IBeam;
            this.BackColor = Color.White;     

            Formatting = new CodeFormatting(
                42,
                BackColor,
                Color.RoyalBlue,
                Color.DarkGoldenrod,
                Color.DarkCyan,
                true);

            var textChanged = this as INotifyPropertyChanged;
            if (textChanged != null)
                textChanged.PropertyChanged += (s, e) => { if (e.PropertyName == "Text") UpdateCodeBox(); };

#if DEBUG
            var debug = this as INotifyPropertyChanged;
            if (debug != null) debug.PropertyChanged += (s, e) => { Debug.WriteLine("CodePanel." + e.PropertyName); };
#endif
        }

        public void UpdateCodeBox()
        {
            CodeBox.BackgroundImageLayout = ImageLayout.None;
            CodeBox.BackgroundImage = new CodePad(Text, Formatting).ToBitmap();
            CodeBox.Location = new Point(10, 10);
            CodeBox.Size = CodeBox.BackgroundImage.Size;
        }
    }
}
