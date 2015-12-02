using System;
using System.Drawing;
using System.Windows.Forms;

namespace Braille
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        internal CodePanel CodePanel = new CodePanel { Text = "CODE\r\nHello\r\nWorld", };

        private void Form1_Load(object sender, EventArgs e)
        {
            CodePanel.Parent = this;

            textBox1.Text = CodePanel.Text;
            numericUpDown1.Value = CodePanel.FontSize;
            checkBox1.Checked = CodePanel.ShowBlanks;

            doLayout();
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

            CodePanel.Location = new Point(0, 0);
            CodePanel.Width = this.ClientRectangle.Width;
            CodePanel.Height = textBox1.Top;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            doLayout();
            //CodePanel.UpdateCode();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CodePanel.Text = textBox1.Text;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CodePanel.ShowBlanks = checkBox1.Checked;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            CodePanel.FontSize = Convert.ToInt32(numericUpDown1.Value);
        }
    }
}
