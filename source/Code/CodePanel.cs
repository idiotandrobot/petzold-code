using PropertyChanged;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Code
{
    [ImplementPropertyChanged]
    public class CodePanel : Panel
    {
        CodeFormatting Formatting;
        CodeBox CodeBox;        

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

        public override string Text
        {
            get { return CodeBox.Text; }
            set { CodeBox.Text = value; }
        }        

        public CodePanel()
        {
            AutoScroll = true;

            Formatting = new CodeFormatting();
            BackColor = Formatting.BackColor;

            CodeBox = new CodeBox(new CodePad(Formatting));
            Controls.Add(CodeBox);

            (this as INotifyPropertyChanged).PropertyChanged += (s, e) =>
            {
                switch (e.PropertyName)
                {
                    //case "BackColor":
                    case "FontSize":                    
                    case "BrailleColor":
                    case "MorseColor":
                    case "BinaryColor":
                    case "ShowBlanks":
                        CodeBox.Redraw();
                        break;
                    case "Text":
                        CodeBox.Text = Text;
                        break;
                    default:
                        break;
                }
            };
        }
    }
}
