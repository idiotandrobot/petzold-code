using System;
using System.Drawing;

namespace Braille
{
    public class CodePad
    {
        CodeFormatting Formatting { get; set; }
        CodeLayout Layout { get; set; }
        CodeCharPen Pen { get; set; }

        public CodePad(string text, CodeFormatting formatting)
        {
            Formatting = formatting;
            Layout = new CodeLayout(text, formatting);
            Pen = new CodeCharPen(formatting);
        }

        public Bitmap ToBitmap()
        {            
            try
            {
                var bitmap = new Bitmap(Layout.Width, Layout.Height);

                Graphics g = Graphics.FromImage(bitmap);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                g.Clear(Formatting.BackColor);

                foreach (var charLayout in Layout)
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
