using System;
using System.Drawing;
using System.Windows.Forms;

namespace Code
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

            toolStripLabel1.Text = codePanel1.FontSize.ToString();

            for (KnownColor i = KnownColor.AliceBlue; i < KnownColor.YellowGreen; i++)
            {
                toolStripComboBox2.Items.Add(i);
                toolStripComboBox3.Items.Add(i);
                toolStripComboBox4.Items.Add(i);
            }

            toolStripComboBox2.SelectedItem = codePanel1.BrailleColor.ToKnownColor();
            toolStripComboBox3.SelectedItem = codePanel1.MorseColor.ToKnownColor();
            toolStripComboBox4.SelectedItem = codePanel1.BinaryColor.ToKnownColor();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            codePanel1.Text = textBox1.Text;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            codePanel1.FontSize = 42;
            toolStripLabel1.Text = codePanel1.FontSize.ToString();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            codePanel1.FontSize += 1;
            toolStripLabel1.Text = codePanel1.FontSize.ToString();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            codePanel1.FontSize -= 1;
            toolStripLabel1.Text = codePanel1.FontSize.ToString();
        }

        private void toolStripComboBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                codePanel1.BrailleColor = Color.FromKnownColor((KnownColor)toolStripComboBox2.SelectedItem);
            }
            catch (Exception) { }
        }

        private void toolStripComboBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                codePanel1.MorseColor = Color.FromKnownColor((KnownColor)toolStripComboBox3.SelectedItem);
            }
            catch (Exception) { }
        }

        private void toolStripComboBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                codePanel1.BinaryColor = Color.FromKnownColor((KnownColor)toolStripComboBox4.SelectedItem);
            }
            catch (Exception) { }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            codePanel1.BrailleColor = Color.RoyalBlue;
            codePanel1.MorseColor = Color.Goldenrod;
            codePanel1.BinaryColor = Color.DarkCyan;

            toolStripComboBox2.SelectedItem = codePanel1.BrailleColor.ToKnownColor();
            toolStripComboBox3.SelectedItem = codePanel1.MorseColor.ToKnownColor();
            toolStripComboBox4.SelectedItem = codePanel1.BinaryColor.ToKnownColor();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                var result = saveFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    codePanel1.Save(saveFileDialog1.FileName);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }            
        }
    }
}
