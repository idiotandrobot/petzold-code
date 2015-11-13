using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace Braille
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        internal BraillePanel BraillePanel1 = new BraillePanel
        {
            BackColor = Color.White,
            ForeColor = Color.RoyalBlue,
            BrailleColor =  Color.RoyalBlue,
            MorseColor = Color.DarkGoldenrod,
            BinaryColor = Color.DarkCyan,
            FontSize = 48,
            DrawBlanks = true,
        };
        private void Form1_Load(object sender, EventArgs e)
        {
            BraillePanel1.Parent = this;
            doLayout();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            BraillePanel1.Text = textBox1.Text;
        }
        public void doLayout()
        {
            numericUpDown1.Top = this.ClientRectangle.Height - numericUpDown1.Height;
            numericUpDown1.Left = 0;
            numericUpDown1.Width = this.ClientRectangle.Width / 2;
            numericUpDown1.Value = BraillePanel1.FontSize;
            checkBox1.Left = numericUpDown1.Left + numericUpDown1.Width + 5;
            checkBox1.Width = numericUpDown1.Width;
            checkBox1.Top = numericUpDown1.Top;
            checkBox1.Checked = BraillePanel1.DrawBlanks;
            textBox1.Left = 0;
            textBox1.Width = this.ClientRectangle.Width;
            textBox1.Top = numericUpDown1.Top - textBox1.Height;
            BraillePanel1.Location = new Point(0, 0);
            BraillePanel1.Width = this.ClientRectangle.Width;
            BraillePanel1.Height = textBox1.Top;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            doLayout();
            BraillePanel1.UpdateBraille();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            BraillePanel1.DrawBlanks = checkBox1.Checked;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            BraillePanel1.FontSize = Convert.ToInt32(numericUpDown1.Value);
        }
    }

    public class BraillePanel : Panel
    {
        private string _text { get; set; }
        private int _fontSize { get; set; }
        private bool _drawBlanks { get; set; }
        internal dbPictureBox pnlDisplay = new dbPictureBox();
        public new event TextChangedEventHandler TextChanged;
        public delegate void TextChangedEventHandler(object sender, EventArgs e);
        public new string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                if (TextChanged != null)
                {
                    TextChanged(this, new EventArgs());
                }
            }
        }
        public bool DrawBlanks
        {
            get { return _drawBlanks; }
            set
            {
                _drawBlanks = value;
                UpdateBraille();
            }
        }
        public int FontSize
        {
            get { return _fontSize; }
            set
            {
                _fontSize = value;
                UpdateBraille();
            }
        }

        private Color _BrailleColor;
        public Color BrailleColor
        {
            get { return _BrailleColor; }
            set
            {
                _BrailleColor = value;
                UpdateBraille();
            }
        }

        private Color _MorseColor;
        public Color MorseColor
        {
            get { return _MorseColor; }
            set
            {
                _MorseColor = value;
                UpdateBraille();
            }
        }

        private Color _BinaryColor;
        public Color BinaryColor
        {
            get { return _BinaryColor; }
            set
            {
                _BinaryColor = value;
                UpdateBraille();
            }
        }

        public BraillePanel()
        {
            TextChanged += Text_Changed;
            base.DoubleBuffered = true;
            base.AutoScroll = true;
            base.Controls.Add(pnlDisplay);
            base.Cursor = Cursors.IBeam;
        }

        public BraillePanel(string text)
        {
            TextChanged += Text_Changed;
            this.Text = text;
            this.AutoScroll = true;
            this.Controls.Add(pnlDisplay);
            this.Cursor = Cursors.IBeam;
        }

        public void Text_Changed(object sender, EventArgs e)
        {
            UpdateBraille();
        }

        public void UpdateBraille()
        {
            pnlDisplay.BackgroundImageLayout = ImageLayout.None;
            pnlDisplay.BackgroundImage = this.ToBraille(this.Text, this.FontSize, this.BackColor, this.BrailleColor, this.MorseColor, this.BinaryColor, this.DrawBlanks);
            pnlDisplay.Location = new Point(10, 10);
            pnlDisplay.Size = pnlDisplay.BackgroundImage.Size;
        }

        private Bitmap ToBraille(string text, int fontSize, Color backColor, Color brailleColor, Color morseColor, Color binaryColor, bool drawBlanks)
        {
            string[] lines = { };
            int dotSize = fontSize - 2;
            int pad = dotSize / 2;
            int characterWidth = (fontSize * 2) + (pad * 2);
            int characterHeight = (fontSize * 3) + (pad * 2);
            Color crColor = Color.FromArgb(255, 255, 255, 0);
            Color lfColor = Color.FromArgb(255, 0, 255, 255);
            Color unknownColor = Color.FromArgb(255, 255, 0, 255);
            try
            {
                lines = text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            }
            catch
            {
                Bitmap bm = new Bitmap(1, 1);
                Graphics g = Graphics.FromImage(bm);
                g.Clear(base.BackColor);
                return bm;
            }
            Size maxSize = new Size(0, lines.Count() * characterHeight);
            foreach (string line in lines)
            {
                if ((line.Length * characterWidth) > maxSize.Width)
                    maxSize.Width = (line.Length * characterWidth);
            }
            List<Image> images = new List<Image>();
            Bitmap finalBM = null;
            try
            {
                finalBM = new Bitmap(maxSize.Width, maxSize.Height);
            }
            catch
            {
                Bitmap bm = new Bitmap(1, 1);
                Graphics g = Graphics.FromImage(bm);
                g.Clear(base.BackColor);
                return bm;
            }
            Graphics finalG = Graphics.FromImage(finalBM);
            finalG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            finalG.Clear(backColor);
            if (string.IsNullOrEmpty(text))
                return finalBM;
            foreach (char c in text)
            {
                BrailleChar brailleChar = c.ToBraille();
                Bitmap bm = new Bitmap(characterWidth, characterHeight);
                Graphics g = Graphics.FromImage(bm);
                int x = 0;
                int y = -fontSize;

                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(backColor);

                if (brailleChar == BrailleChar.Empty)
                {
                    switch (c)
                    {
                        case ' ':
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                            g.Clear(backColor);
                            images.Add(bm);
                            continue;
                        //'\r' is carriage return.
                        //'\n' is new line.
                        case '\r':
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                            g.Clear(backColor);
                            bm.SetPixel(0, 0, crColor);
                            images.Add(bm);
                            continue;
                        case '\n':
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                            g.Clear(backColor);
                            bm.SetPixel(0, 0, lfColor);
                            images.Add(bm);
                            continue;
                        default:
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                            g.Clear(backColor);
                            bm.SetPixel(0, 0, unknownColor);
                            images.Add(bm);
                            continue;
                    }
                }
                int actualScale = 2;
                int actualDotSize = (int)(dotSize / actualScale);
                int actualDotLocation = (int)((dotSize - actualDotSize) / 2);
                int blankScale = 4;
                int blankDotSize = (int)(dotSize / blankScale);
                int blankDotLocation = (int)((dotSize - blankDotSize) / 2);

                int morsex = x - fontSize / 2;
                int morsey = y + fontSize + fontSize / 2;

                int binaryx = x - fontSize / 2;
                int binaryy = y + (fontSize * 3);

                foreach (bool code in brailleChar)
                {
                    y += fontSize;
                    if (y > (fontSize * 2))
                    {
                        y = 0;
                        x += fontSize;
                    }
                    if (code)
                    {
                        g.FillEllipse(new SolidBrush(brailleColor), new Rectangle(
                            new Point(x + pad + actualDotLocation, y + pad + actualDotLocation),
                            new Size(actualDotSize, actualDotSize)));
                    }
                    else
                    {
                        if (drawBlanks)
                        {
                            //g.DrawEllipse(new Pen(new SolidBrush(foreColor)), new Rectangle(new Point(x + pad, y + pad), new Size(dotSize, dotSize)));
                            g.FillEllipse(new SolidBrush(brailleColor), new Rectangle(
                                new Point(x + pad + blankDotLocation, y + pad + blankDotLocation),
                                new Size(blankDotSize, blankDotSize)));
                        }
                    }

                }

                int morseScale = 3;
                int morseScaleSize = (int)(dotSize / morseScale);
                int morseScaleLocation = (int)((dotSize - morseScaleSize) / 2);
                int morseDashWidth = morseScaleSize * 2;
                int morseDashHeight = morseScaleSize / 2;

                var morseDashSize = new Size(morseDashWidth, morseDashHeight);
                var morseDotSize = new Size(morseScaleSize, morseScaleSize);

                var morseSequenceWidth = 0;
                foreach (bool code in c.ToMorse())
                {
                    if (code)
                        morseSequenceWidth += morseDashSize.Width;
                    else
                        morseSequenceWidth += morseDotSize.Width;

                    morseSequenceWidth += morseScaleSize / 2;
                }

                foreach (bool code in c.ToMorse())
                {                    
                    if (code)
                    {
                        g.FillRectangle(new SolidBrush(morseColor), new Rectangle(
                            new Point(morsex + pad + morseScaleLocation, morsey + pad + morseScaleLocation + morseDashHeight / 2),
                            morseDashSize));

                        morsex += morseDashSize.Width;
                    }
                    else
                    {
                        g.FillEllipse(new SolidBrush(morseColor), new Rectangle(
                            new Point(morsex + pad + morseScaleLocation, morsey + pad + morseScaleLocation),
                            morseDotSize));

                        morsex += morseDotSize.Width;
                    }

                    morsex += morseScaleSize / 2;
                }

                var binaryString = c.ToBinaryString();
                g.DrawString(binaryString,
                    new Font("Courier New", fontSize / 3),
                    new SolidBrush(binaryColor),
                    new Point(binaryx + pad, binaryy + pad - fontSize / 4));

                images.Add(bm);
            }
            int left = -images[0].Width;
            int top = 0;
            foreach (Bitmap Image in images)
            {
                Color color = Image.GetPixel(0, 0);
                if (color == crColor)
                {
                    left = -Image.Width;
                    top += Image.Height;
                    continue;
                }
                else if (color == lfColor)
                {
                    continue;
                }
                else if (color == unknownColor)
                {
                    //    MsgBox("unknown!")
                    continue;
                }
                left += Image.Width;
                if ((left + Image.Width) > maxSize.Width)
                {
                    left = 0;
                    top += Image.Height;
                }
                Point location = new Point(left, top);
                finalG.DrawImage(Image, location);
            }
            return finalBM;
        }
        //This class the db Picturebox, is Double Buffered, for less flickering
        public class dbPictureBox : PictureBox
        {
            public dbPictureBox()
            {
                this.DoubleBuffered = true;
            }
        }
    }
}
