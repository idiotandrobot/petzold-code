using Petzold.Code;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Code
{
    public class BrailleCharLayout : IEnumerable<Tuple<bool, Rectangle>>
    {
        List<Tuple<bool, Rectangle>> Sequence = new List<Tuple<bool, Rectangle>>();

        public BrailleCharLayout(BrailleChar brailleChar, BrailleFormatting formatting)
        {
            int x = 0;
            int y = -formatting.FontSize;

            foreach (bool code in brailleChar)
            {
                y += formatting.FontSize;
                if (y > (formatting.FontSize * 2))
                {
                    y = 0;
                    x += formatting.FontSize;
                }
                if (code)
                {
                    Sequence.Add(new Tuple<bool, Rectangle>(true, new Rectangle(
                        formatting.GetDotLocation(x, y),
                        formatting.DotSize)));
                }
                else if (formatting.ShowBlanks)
                {
                    Sequence.Add(new Tuple<bool, Rectangle>(false, new Rectangle(
                        formatting.GetBlankLocation(x, y),
                        formatting.BlankSize)));
                }
            }
        }

        public IEnumerator<Tuple<bool, Rectangle>> GetEnumerator()
        {
            return Sequence.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Sequence.GetEnumerator();
        }
    }
}
