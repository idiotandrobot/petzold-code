using System;
using System.Drawing;

namespace Braille
{
    public class CodePad
    {
        CodeFormatting Formatting { get; set; }
        CodeCharPen Pen { get; set; }

        public CodePad(CodeFormatting formatting)
        {
            if (formatting == null) throw new ArgumentNullException("formatting");
            Formatting = formatting;            
            Pen = new CodeCharPen(formatting);
        }

        public Bitmap ToBitmap(string text)
        {            
            try
            {
                var layout = new CodeLayout(text, Formatting);

                var bitmap = new Bitmap(layout.Width, layout.Height);

                Graphics g = Graphics.FromImage(bitmap);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                g.Clear(Formatting.BackColor);

                foreach (var charLayout in layout)
                {
                    g.DrawImage(Pen.Draw(charLayout), charLayout.Location);
                }
                return bitmap;
            }
            catch (Exception)
            {
                var b = new Bitmap(1, 1);
                Graphics g = Graphics.FromImage(b);
                g.Clear(Formatting.BackColor);
                return b;
            }
        }
    }
}
