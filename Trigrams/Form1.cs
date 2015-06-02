using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
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
            //Trigrams.Program.InitData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            testing();
        }

        private void testing()
        {
            string filename1 = "Trigrams8Data.json";
            string filename2 = "Trigrams64Data.json";
            string filename3 = "SixLineData.json";
            string fs1 = Trigrams.Program.ReadJsonFile(filename1);
            string fs2 = Trigrams.Program.ReadJsonFile(filename2);
            string fs3 = Trigrams.Program.ReadJsonFile(filename3);
            string t1s = string.Empty;
            string t2s = string.Empty;
            string t3s = string.Empty;
            JavaScriptSerializer json = new JavaScriptSerializer();
            List<Trigrams8> tri8 = json.Deserialize<List<Trigrams8>>(fs1);
            List<Trigrams64> tri64 = json.Deserialize<List<Trigrams64>>(fs2);
            List<SixLine> sl = json.Deserialize<List<SixLine>>(fs3);

            //輸出結果
            textBox1.Text = "=============== 123 開始 ==============\r\n";
            #region 全測
            foreach (var t1 in tri8)
            {
                t1s = string.Format(@"卦名：{0}，方向：{1}，描述：{2}", t1.name, t1.direction, t1.description);
                textBox1.Text += t1s + "\r\n";
                var t2 = from d in tri64
                         where d.house == t1.name
                         select d;
                foreach (var t22 in t2)
                {
                    t2s = string.Format(@"   序號：{0}，六十四卦名：{1}，宮位：{2}，描述：{3}", t22.sequence, t22.allname, t22.house, t22.description);
                    textBox1.Text += t2s + "\r\n";
                    var t3 = from d in sl
                             where d.allname == t22.allname
                             select d;
                    foreach (var t33 in t3)
                    {
                        t3s = string.Format(@"      爻位：{0},世應：{1},象曰：{2}", t33.position, t33.self, t33.description);
                        textBox1.Text += t3s + "\r\n";
                    }
                }
            }
            #endregion
        }

        private void cbxSL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Text = cbxSL1.Text;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string code = string.Join(null, cbxSL1.Text.Substring(0, 1), cbxSL2.Text.Substring(0, 1), cbxSL3.Text.Substring(0, 1), cbxSL4.Text.Substring(0, 1), cbxSL5.Text.Substring(0, 1), cbxSL6.Text.Substring(0, 1));
            Arrange op = new Arrange();
            textBox2.Text = code;
            List<Trigrams64> list = op.get64Gua(code);
            foreach (var tt in list)
            {
                textBox1.Text += tt.name + tt.code + tt.description + "\r\n";
            }

        }
    }
}
