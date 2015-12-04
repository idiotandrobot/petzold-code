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

        [Category("Appearance")]
        public int FontSize
        {
            get { return Formatting.FontSize; }
            set { Formatting.FontSize = value; }
        }

        //public override Color BackColor
        //{
        //    get { return Formatting.BackColor; }
        //    set { Formatting.BackColor = value; }
        //}

        [Category("Appearance")]
        public Color BrailleColor
        {
            get { return Formatting.Braille.Color; }
            set { Formatting.Braille.Color = value; }
        }

        [Category("Appearance")]
        public Color MorseColor
        {
            get { return Formatting.Morse.Color; }
            set { Formatting.Morse.Color = value; }
        }

        [Category("Appearance")]
        public Color BinaryColor
        {
            get { return Formatting.Binary.Color; }
            set { Formatting.Binary.Color = value; }
        }

        [Category("Appearance")]
        public bool ShowBlanks
        {
            get { return Formatting.Braille.ShowBlanks; }
            set { Formatting.Braille.ShowBlanks = value; }
        }

        public override string Text { get; set; } 

        public CodePanel()
        {
            Controls.Add(CodeBox);

            AutoScroll = true;
            BackColor = Color.White;     

            Formatting = new CodeFormatting(
                42,
                BackColor,
                Color.RoyalBlue,
                Color.DarkGoldenrod,
                Color.DarkCyan,
                true);

            CodeBox.Formatting = Formatting;
            CodeBox.Text = "CODE";

            var notify = this as INotifyPropertyChanged;
            if (notify != null)
                notify.PropertyChanged += (s, e) => 
                {
                    switch (e.PropertyName)
                    {
                        case "FontSize":
                        //case "BackColor":
                        case "BrailleColor":
                        case "MorseColor":
                        case "BinaryColor":
                        case "ShowBlanks":
                        case "Text":
                            CodeBox.Text = Text;
                            break;
                        default:
                            break;
                    }
                };
#if DEBUG
            var debug = this as INotifyPropertyChanged;
            if (debug != null) debug.PropertyChanged += (s, e) => { Debug.WriteLine("CodePanel." + e.PropertyName); };
#endif
        }
    }
}
