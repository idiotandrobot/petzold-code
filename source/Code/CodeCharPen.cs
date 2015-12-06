using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Code
{
    public class CodeCharPen
    {
        CodeFormatting Formatting { get; set; }

        public CodeCharPen(CodeFormatting formatting)
        {
            Formatting = formatting;
        }

        public Bitmap Draw(CodeCharLayout charLayout)
        {
            Bitmap bm = new Bitmap(Formatting.Braille.Width, Formatting.Braille.Height);
            Graphics g = Graphics.FromImage(bm);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Formatting.BackColor);

            foreach (var layout in charLayout.BrailleLayout)
            {
                g.FillEllipse(Formatting.Braille.Brush, layout.Item2);
            }

            foreach (var layout in charLayout.MorseLayout)
            {
                if (layout.Item1)
                    g.FillRectangle(Formatting.Morse.Brush, layout.Item2);
                else
                    g.FillEllipse(Formatting.Morse.Brush, layout.Item2);
            }

            g.DrawString(charLayout.BinaryLayout.Value,
                    Formatting.Binary.Font,
                    Formatting.Binary.Brush,
                    charLayout.BinaryLayout.Location);

            return bm;
        }
    }
}
