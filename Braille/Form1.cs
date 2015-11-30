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
            BrailleColor = Color.RoyalBlue,
            MorseColor = Color.DarkGoldenrod,
            BinaryColor = Color.DarkCyan,
            FontSize = 42,
            ShowBlanks = true,
            Initializing = false,
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
            BraillePanel1.UpdateCode();
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

        public bool Initializing { get; set; }

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
                UpdateCode();
            }
        }

        int _FontSize;
        public int FontSize
        {
            get { return _FontSize; }
            set
            {
                _FontSize = value;
                UpdateCode();
            }
        }

        private Color _BrailleColor;
        public Color BrailleColor
        {
            get { return _BrailleColor; }
            set
            {
                _BrailleColor = value;
                UpdateCode();
            }
        }

        private Color _MorseColor;
        public Color MorseColor
        {
            get { return _MorseColor; }
            set
            {
                _MorseColor = value;
                UpdateCode();
            }
        }

        private Color _BinaryColor;
        public Color BinaryColor
        {
            get { return _BinaryColor; }
            set
            {
                _BinaryColor = value;
                UpdateCode();
            }
        }

        public BraillePanel()
        {
            Initializing = true;
            TextChanged += Text_Changed;
            base.DoubleBuffered = true;
            base.AutoScroll = true;
            base.Controls.Add(pnlDisplay);
            base.Cursor = Cursors.IBeam;
        }

        public BraillePanel(string text)
        {
            Initializing = true;
            TextChanged += Text_Changed;
            this.Text = text;
            this.AutoScroll = true;
            this.Controls.Add(pnlDisplay);
            this.Cursor = Cursors.IBeam;
        }

        public void Text_Changed(object sender, EventArgs e)
        {
            UpdateCode();
        }

        public void UpdateCode()
        {
            if (Initializing) return;
            pnlDisplay.BackgroundImageLayout = ImageLayout.None;
            pnlDisplay.BackgroundImage = this.ToCode(this.Text, this.FontSize, this.BackColor, this.BrailleColor, this.MorseColor, this.BinaryColor, this.ShowBlanks);
            pnlDisplay.Location = new Point(10, 10);
            pnlDisplay.Size = pnlDisplay.BackgroundImage.Size;
        }

        private Bitmap ToCode(
            string text, 
            int fontSize, 
            Color backColor, 
            Color brailleColor, 
            Color morseColor, 
            Color binaryColor, 
            bool showBlanks)
        {
            var formatting = new CodeFormatting(
                fontSize,
                backColor,
                brailleColor,
                morseColor,
                binaryColor,
                showBlanks);

            var pad = new CodePad(text, formatting);
            return pad.ToBitmap();
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
