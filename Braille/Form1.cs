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
            FontSize = 42,
            ShowBlanks = true,
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
            checkBox1.Checked = BraillePanel1.ShowBlanks;
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
            BraillePanel1.ShowBlanks = checkBox1.Checked;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            BraillePanel1.FontSize = Convert.ToInt32(numericUpDown1.Value);
        }
    }

    public class BraillePanel : Panel
    {
        internal dbPictureBox pnlDisplay = new dbPictureBox();
        public new event TextChangedEventHandler TextChanged;
        public delegate void TextChangedEventHandler(object sender, EventArgs e);

        string _Text;
        public new string Text
        {
            get { return _Text; }
            set
            {
                _Text = value;
                if (TextChanged != null)
                {
                    TextChanged(this, new EventArgs());
                }
            }
        }

        bool _ShowBlanks;
        public bool ShowBlanks
        {
            get { return _ShowBlanks; }
            set
            {
                _ShowBlanks = value;
                UpdateBraille();
            }
        }

        int _FontSize;
        public int FontSize
        {
            get { return _FontSize; }
            set
            {
                _FontSize = value;
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
            pnlDisplay.BackgroundImage = this.ToBraille(this.Text, this.FontSize, this.BackColor, this.BrailleColor, this.MorseColor, this.BinaryColor, this.ShowBlanks);
            pnlDisplay.Location = new Point(10, 10);
            pnlDisplay.Size = pnlDisplay.BackgroundImage.Size;
        }

        private Bitmap ToBraille(
            string text, 
            int fontSize, 
            Color backColor, 
            Color brailleColor, 
            Color morseColor, 
            Color binaryColor, 
            bool showBlanks)
        {
            string[] lines = { };
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

            var formatting = new CodeFormatting(
                fontSize, 
                brailleColor, 
                morseColor, 
                binaryColor, 
                showBlanks);
            
            var maxSize = new Size(0, lines.Count() * formatting.Braille.Height);
            foreach (string line in lines)
            {
                if ((line.Length * formatting.Braille.Width) > maxSize.Width)
                    maxSize.Width = (line.Length * formatting.Braille.Width);
            }
            
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

            var codeLayout = new CodeLayout(text, formatting);
            
            foreach (var charLayout in codeLayout)
            {
                Bitmap bm = new Bitmap(formatting.Braille.Width, formatting.Braille.Height);
                Graphics g = Graphics.FromImage(bm);

                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(backColor);

                foreach (var layout in charLayout.BrailleLayout)
                {
                    g.FillEllipse(formatting.Braille.Brush, layout.Item2);
                }

                foreach (var layout in charLayout.MorseLayout)
                {
                    if (layout.Item1)
                        g.FillRectangle(formatting.Morse.Brush, layout.Item2);
                    else
                        g.FillEllipse(formatting.Morse.Brush, layout.Item2);
                }

                g.DrawString(charLayout.BinaryLayout.Value,
                    formatting.Binary.Font,
                    formatting.Binary.Brush,
                    charLayout.BinaryLayout.Location);

                finalG.DrawImage(bm, charLayout.Location);
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
