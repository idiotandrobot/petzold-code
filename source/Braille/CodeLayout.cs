﻿using Petzold.Code;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Braille
{
    public class CodeLayout : IEnumerable<CodeCharLayout>
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        List<CodeCharLayout> Layouts = new List<CodeCharLayout>();

        public CodeLayout(
            string text,
            CodeFormatting formatting)
        {            
            int maxWidth = 0;
            int height = 0;
            if (string.IsNullOrEmpty(text))
            {
                maxWidth = 1;
                height = 1;
            }
            else
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
                                left += formatting.Braille.Width;
                                break;

                            case '\r': // carriage return
                                left = 0;
                                break;
                            case '\n': // new line
                                top += formatting.Braille.Height;
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
                        formatting);

                        Layouts.Add(codeLayout);

                        left += formatting.Braille.Width;
                        if (left > maxWidth) maxWidth = left;
                        height = top + formatting.Braille.Height;
                        // TODO: word wrap
                    }
                }
            }
            Width = maxWidth;
            Height = height;
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
