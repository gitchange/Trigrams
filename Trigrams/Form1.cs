using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Trigrams
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Trigrams.Program.InitData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "123 開始\r\n";
            foreach (Trigrams64 t in Trigrams.Program.trigrams64)
            {
                textBox1.Text += t.allname + "\r\n";
                foreach (SixLine sl in t.sixline)
                {
                    textBox1.Text += sl.position + "\r\n";
                }
            }
        }
    }
}
