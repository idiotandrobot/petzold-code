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
namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        internal BraillePanel BraillePanel1 = new BraillePanel
        {
            BackColor = Color.Black,
            ForeColor = Color.White,
            FontSize = 10
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
            checkBox1.Left = numericUpDown1.Left + numericUpDown1.Width + 5;
            checkBox1.Width = numericUpDown1.Width;
            checkBox1.Top = numericUpDown1.Top;
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
        pnlDisplay.BackgroundImage = this.ToBraille(this.Text, this.FontSize, this.BackColor, this.ForeColor, this.DrawBlanks);
        pnlDisplay.Location = new Point(10, 10);
        pnlDisplay.Size = pnlDisplay.BackgroundImage.Size;
    }
    private Bitmap ToBraille(string text, int fontSize, Color backColor, Color foreColor, bool drawBlanks)
    {
        string[] lines = {
			
		};
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
        foreach (char s in text.ToLower())
        {
            string keyCode = string.Empty;
            Bitmap bm = new Bitmap(characterWidth, characterHeight);
            Graphics g = Graphics.FromImage(bm);
            int x = 0;
            int y = -fontSize;
            string s3 = s.ToString();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(backColor);
            switch (s3)
            {
                case "a":
                    keyCode = "100000";
                    break;
                case "b":
                    keyCode = "110000";
                    break;
                case "c":
                    keyCode = "100100";
                    break;
                case "d":
                    keyCode = "100110";
                    break;
                case "e":
                    keyCode = "100010";
                    break;
                case "f":
                    keyCode = "110100";
                    break;
                case "g":
                    keyCode = "110110";
                    break;
                case "h":
                    keyCode = "110010";
                    break;
                case "i":
                    keyCode = "010100";
                    break;
                case "j":
                    keyCode = "010110";
                    break;
                case "k":
                    keyCode = "101000";
                    break;
                case "l":
                    keyCode = "111000";
                    break;
                case "m":
                    keyCode = "101100";
                    break;
                case "n":
                    keyCode = "101110";
                    break;
                case "o":
                    keyCode = "101010";
                    break;
                case "p":
                    keyCode = "111100";
                    break;
                case "q":
                    keyCode = "111110";
                    break;
                case "r":
                    keyCode = "111010";
                    break;
                case "s":
                    keyCode = "011100";
                    break;
                case "t":
                    keyCode = "011110";
                    break;
                case "u":
                    keyCode = "101001";
                    break;
                case "v":
                    keyCode = "111001";
                    break;
                case "w":
                    keyCode = "010111";
                    break;
                case "x":
                    keyCode = "101101";
                    break;
                case "y":
                    keyCode = "101111";
                    break;
                case "z":
                    keyCode = "101011";
                    break;
                case "1":
                    keyCode = "010000";
                    break;
                case "2":
                    keyCode = "011000";
                    break;
                case "3":
                    keyCode = "010010";
                    break;
                case "4":
                    keyCode = "010011";
                    break;
                case "5":
                    keyCode = "010001";
                    break;
                case "6":
                    keyCode = "011010";
                    break;
                case "7":
                    keyCode = "011011";
                    break;
                case "8":
                    keyCode = "011001";
                    break;
                case "9":
                    keyCode = "001010";
                    break;
                case "0":
                    keyCode = "001011";
                    break;
                case "&":
                    keyCode = "111101";
                    break;
                case "=":
                    keyCode = "111111";
                    break;
                case "(":
                    keyCode = "111011";
                    break;
                case "!":
                    keyCode = "011101";
                    break;
                case ")":
                    keyCode = "011111";
                    break;
                case "*":
                    keyCode = "100001";
                    break;
                case "<":
                    keyCode = "110001";
                    break;
                case "%":
                    keyCode = "100101";
                    break;
                case "?":
                    keyCode = "100111";
                    break;
                case ":":
                    keyCode = "100011";
                    break;
                case "$":
                    keyCode = "110101";
                    break;
                case "]":
                    keyCode = "110111";
                    break;
                case "\\":
                    keyCode = "110011";
                    break;
                case "[":
                    keyCode = "010101";
                    break;
                case "/":
                    keyCode = "001100";
                    break;
                case "+":
                    keyCode = "001101";
                    break;
                case "#":
                    keyCode = "001111";
                    break;
                case ">":
                    keyCode = "001110";
                    break;
                case "'":
                    keyCode = "001000";
                    break;
                case "-":
                    keyCode = "001001";
                    break;
                case "@":
                    keyCode = "000100";
                    break;
                case "^":
                    keyCode = "000110";
                    break;
                case "_":
                    keyCode = "000111";
                    break;
                case "\"":
                    keyCode = "000010";
                    break;
                case ".":
                    keyCode = "000101";
                    break;
                case ";":
                    keyCode = "000011";
                    break;
                case ",":
                    keyCode = "000001";
                    break;
                case " ":
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                    g.Clear(backColor);
                    images.Add(bm);
                    continue;
                //'\r' is carriage return.
                //'\n' is new line.
                case "\r":
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                    g.Clear(Color.White);
                    bm.SetPixel(0, 0, crColor);
                    images.Add(bm);
                    continue;
                case "\n":
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                    g.Clear(Color.White);
                    bm.SetPixel(0, 0, lfColor);
                    images.Add(bm);
                    continue;
                default:
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                    g.Clear(Color.White);
                    bm.SetPixel(0, 0, unknownColor);
                    images.Add(bm);
                    continue;
            }
            foreach (char s2 in keyCode.ToCharArray())
            {
                y += fontSize;
                if (y > (fontSize * 2))
                {
                    y = 0;
                    x += fontSize;
                }
                if (s2 == "1".ToCharArray()[0])
                {
                    g.FillEllipse(new SolidBrush(foreColor), new Rectangle(new Point(x + pad, y + pad), new Size(dotSize, dotSize)));
                }
                else
                {
                    if (drawBlanks)
                    {
                        g.DrawEllipse(new Pen(new SolidBrush(foreColor)), new Rectangle(new Point(x + pad, y + pad), new Size(dotSize, dotSize)));
                    }
                }

            }
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