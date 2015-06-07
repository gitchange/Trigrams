using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
            textBox2.Text = DateTime.Now.ToString();
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
            List<Trigrams64> list;
            textBox1.Text = string.Empty;
            //textBox2.Text = code;
            list = op.get64Gua(0, code);

            textBox1.Text += list[0].name + list[0].code + list[0].description + "\r\n";
            lblOriginal.Text = list[0].allname;
            lblDown.Text = list[0].word.Substring(0, 1);
            lblUp.Text = list[0].word.Substring(1, 1);

            list = op.get64Gua(1, code);
            textBox1.Text += list[0].name + list[0].code + list[0].description + "\r\n";
            lblChange.Text = list[0].allname;
            lblDownChange.Text = list[0].word.Substring(0, 1);
            lblUpChange.Text = list[0].word.Substring(1, 1);

            lblOriginal.Visible = false;
            lblDown.Visible = false;
            lblUp.Visible= false;
            lblChange.Visible = false;
            lblDownChange.Visible = false;
            lblUpChange.Visible = false;

            TaiwanLunisolarCalendar Tlc = new TaiwanLunisolarCalendar();
            
            DateTime dt = Convert.ToDateTime(textBox2.Text.Trim());
            EcanChineseCalendar c = new EcanChineseCalendar(dt);
            StringBuilder dayInfo = new StringBuilder();
            SolarTerm solar = new SolarTerm();
            
            dayInfo.Append("陽歷：" + c.DateString + "\r\n");//陽歷日期
            dayInfo.Append("農歷：" + c.ChineseDateString + "\r\n");//農歷日期
            dayInfo.Append("星期：" + c.WeekDayStr);//星期
            dayInfo.Append("時辰：" + c.ChineseHour + "\r\n");//時辰
            dayInfo.Append("屬相：" + c.AnimalString + "\r\n");//屬相
            dayInfo.Append("節氣：" + c.ChineseTwentyFourDay + "\r\n");//節氣
            dayInfo.Append("前一個節氣：" + c.ChineseTwentyFourPrevDay + "\r\n");//前一個節氣
            dayInfo.Append("下一個節氣：" + c.ChineseTwentyFourNextDay + "\r\n");//下一個節氣
            dayInfo.Append("目前所在的節氣：" + c.Chinese24DaysCurrentName + "\r\n");
            dayInfo.Append("目前所在的節氣時間：" + c.Chinese24DaysDateTime.ToString() + "\r\n");
            dayInfo.Append("Solar目前所在的節氣：" + solar.Get24DaysCurrentName(dt) + "\r\n");
            dayInfo.Append("Solar目前所在的節氣時間：" + solar.Get24DaysDateTime(dt).ToString() + "\r\n");
            dayInfo.Append("節日：" + c.DateHoliday + "\r\n");//節日
            dayInfo.Append("干支：" + c.GanZhiDateString + "\r\n");//干支
            dayInfo.Append("正確的干支：" + c.GanZhiYearWord + "\r\n");//正確的干支            
            dayInfo.Append("星宿：" + c.ChineseConstellation + "\r\n");//星宿
            dayInfo.Append("星座：" + c.Constellation + "\r\n");//星座
            textBox3.Text = dayInfo.ToString();
        }
    }
}
