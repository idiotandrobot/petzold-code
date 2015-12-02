using System.Windows.Forms;

namespace Braille
{
    public class CodeBox : PictureBox
    {
        public CodeFormatting Formatting { get; set; }
        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                BackgroundImage = new CodePad(Text, Formatting).ToBitmap();
                Size = BackgroundImage.Size;
            }
        }

        public CodeBox()
        {
            DoubleBuffered = true;
            BackgroundImageLayout = ImageLayout.None;
        }
    }
}
