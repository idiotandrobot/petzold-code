using System;
using System.Windows.Forms;

namespace Braille
{
    public class CodeBox : PictureBox
    {
        CodePad Pad { get; set; }

        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                BackgroundImage = Pad.ToBitmap(Text);
                Size = BackgroundImage.Size;
            }
        }

        public CodeBox(CodePad pad)
        {
            if (pad == null) throw new ArgumentNullException("pad");
            Pad = pad;

            DoubleBuffered = true;
            BackgroundImageLayout = ImageLayout.None;
        }
    }
}
