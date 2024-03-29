﻿using System;
using System.Windows.Forms;

namespace Code
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
                Redraw();
            }
        }

        public CodeBox(CodePad pad)
        {
            Pad = pad ?? throw new ArgumentNullException(nameof(pad));

            DoubleBuffered = true;
            BackgroundImageLayout = ImageLayout.None;
        }

        public void Redraw()
        {
            BackgroundImage = Pad.ToBitmap(Text);
            Size = BackgroundImage.Size;
        }
    }
}
