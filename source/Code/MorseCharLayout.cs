using Petzold.Code;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Code
{
    public class MorseCharLayout : IEnumerable<Tuple<bool, Rectangle>>
    {
        List<Tuple<bool, Rectangle>> Sequence = new List<Tuple<bool, Rectangle>>();

        public MorseCharLayout(MorseChar morseChar, MorseFormatting formatting)
        {
            var sequenceWidth = 0;
            foreach (bool code in morseChar)
            {
                if (code)
                    sequenceWidth += formatting.DashSize.Width;
                else
                    sequenceWidth += formatting.DotSize.Width;

                sequenceWidth += formatting.SpaceWidth;
            }
            sequenceWidth -= formatting.SpaceWidth;

            int x = -formatting.FontSize / 2;
            int y = formatting.FontSize / 2;

            foreach (var code in morseChar)
            {
                if (code)
                {
                    Sequence.Add(new Tuple<bool, Rectangle>(true, new Rectangle(
                        formatting.GetDashLocation(x, y, sequenceWidth),
                        formatting.DashSize)));

                    x += formatting.DashSize.Width;
                }
                else
                {
                    Sequence.Add(new Tuple<bool, Rectangle>(false, new Rectangle(
                        formatting.GetDotLocation(x, y, sequenceWidth),
                        formatting.DotSize)));

                    x += formatting.DotSize.Width;
                }
                x += formatting.SpaceWidth;
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
