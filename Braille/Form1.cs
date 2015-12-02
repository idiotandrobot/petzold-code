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

        private void Form1_Load(object sender, EventArgs e)
        {
            codePanel1.Text = "CODE\r\nHello\r\nWorld";
            textBox1.Text = codePanel1.Text;
            numericUpDown1.Value = codePanel1.FontSize;
            checkBox1.Checked = codePanel1.ShowBlanks;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            codePanel1.Text = textBox1.Text;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            codePanel1.ShowBlanks = checkBox1.Checked;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            codePanel1.FontSize = Convert.ToInt32(numericUpDown1.Value);
        }
    }
}
