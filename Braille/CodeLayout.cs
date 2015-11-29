using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Braille
{
    public class CodeLayout : IEnumerable<CodeCharLayout>
    {
        List<CodeCharLayout> Layouts = new List<CodeCharLayout>();

        public CodeLayout(
            string text,
            BrailleFormatting bFormatting,
            MorseFormatting mFormatting,
            BinaryFormatting binFormatting)
        {
            int left = 0;
            int top = 0;
            foreach (char c in text)
            {
                var braille = c.ToBraille();

                if (braille == BrailleChar.Empty)
                {
                    switch (c)
                    {
                        case ' ': // space
                            left += bFormatting.Width;
                            break;

                        case '\r': // carriage return
                            left = 0;
                            break;
                        case '\n': // new line
                            top += bFormatting.Height;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    var codeLayout = new CodeCharLayout(
                    c,
                    left,
                    top,
                    bFormatting,
                    mFormatting,
                    binFormatting);

                    Layouts.Add(codeLayout);

                    left += bFormatting.Width;
                    // TODO: word wrap
                }
            }
        }

        public IEnumerator<CodeCharLayout> GetEnumerator()
        {
            return Layouts.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Layouts.GetEnumerator();
        }
    }
}
