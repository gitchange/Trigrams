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
        /// <summary>
        /// 放置本卦 Code
        /// </summary>
        protected static string originalGuaCode { get; set; }
        /// <summary>
        /// 放置變卦 Code
        /// </summary>
        protected static string changeGuaCode { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Trigrams.Program.InitData();
            initionalData();
            DateTime dtp = dateTimePicker1.Value;
            textBox2.Text = dtp.Year.ToString() + "/" + dtp.Month.ToString() + "/" + dtp.Day.ToString() + " " + cbxHour.Text + ":" + cbxMin.Text + ":00";
            //textBox2.Text = DateTime.Now.ToString();
        }

        private void initionalData()
        {
            for (int i = 0; i < 24; i++)
            {
                cbxHour.Items.Add(i.ToString().PadLeft(2, '0'));
            }
            for (int j = 0; j < 60; j++)
            {
                cbxMin.Items.Add(j.ToString().PadLeft(2, '0'));
            }
            dateTimePicker1.Value = DateTime.Now;
            cbxHour.Text = DateTime.Now.Hour.ToString().PadLeft(2, '0');
            cbxMin.Text = DateTime.Now.Minute.ToString().PadLeft(2, '0');
        }

        #region 秀六十四卦
        /// <summary>
        /// 秀六十四卦
        /// </summary>
        private void show64Gua()
        {
            string code = string.Join(null, cbxSL1.Text.Substring(0, 1), cbxSL2.Text.Substring(0, 1), cbxSL3.Text.Substring(0, 1), cbxSL4.Text.Substring(0, 1), cbxSL5.Text.Substring(0, 1), cbxSL6.Text.Substring(0, 1));
            Arrange op = new Arrange();
            List<Trigrams64> list;

            list = op.get64Gua(0, code);
            originalGuaCode = list[0].code;
            textBox1.Text += list[0].name + originalGuaCode + list[0].description + "\r\n";
            lblOriginal.Text = list[0].allname;
            lblDown.Text = list[0].word.Substring(0, 1);
            lblUp.Text = list[0].word.Substring(1, 1);

            list = op.get64Gua(1, code);
            changeGuaCode = list[0].code;
            textBox1.Text += list[0].name + changeGuaCode + list[0].description + "\r\n";
            lblChange.Text = list[0].allname;
            lblDownChange.Text = list[0].word.Substring(0, 1);
            lblUpChange.Text = list[0].word.Substring(1, 1);

            string showchange = "　　×○";
            lblc91.Text = showchange[Convert.ToInt16(cbxSL1.Text.Substring(0, 1))].ToString();
            lblc92.Text = showchange[Convert.ToInt16(cbxSL2.Text.Substring(0, 1))].ToString();
            lblc93.Text = showchange[Convert.ToInt16(cbxSL3.Text.Substring(0, 1))].ToString();
            lblc94.Text = showchange[Convert.ToInt16(cbxSL4.Text.Substring(0, 1))].ToString();
            lblc95.Text = showchange[Convert.ToInt16(cbxSL5.Text.Substring(0, 1))].ToString();
            lblc96.Text = showchange[Convert.ToInt16(cbxSL6.Text.Substring(0, 1))].ToString();
        }
        #endregion

        #region 秀六十四卦的天干地支
        /// <summary>
        /// 秀六十四卦的天干地支
        /// </summary>
        private void showGuaGanZhi()
        {
            string GuaGanZhi = string.Empty;
            Arrange op = new Arrange();
            GuaGanZhi = op.setupTenGan(originalGuaCode);
            lbl091.Text = GuaGanZhi[0].ToString();
            lbl092.Text = GuaGanZhi[1].ToString();
            lbl093.Text = GuaGanZhi[2].ToString();
            lbl094.Text = GuaGanZhi[3].ToString();
            lbl095.Text = GuaGanZhi[4].ToString();
            lbl096.Text = GuaGanZhi[5].ToString();
            GuaGanZhi = op.setupDiZhi(originalGuaCode);
            lbl091.Text += GuaGanZhi[0].ToString();
            lbl092.Text += GuaGanZhi[1].ToString();
            lbl093.Text += GuaGanZhi[2].ToString();
            lbl094.Text += GuaGanZhi[3].ToString();
            lbl095.Text += GuaGanZhi[4].ToString();
            lbl096.Text += GuaGanZhi[5].ToString();
            GuaGanZhi = op.setupTenGan(changeGuaCode);
            lbl191.Text = GuaGanZhi[0].ToString();
            lbl192.Text = GuaGanZhi[1].ToString();
            lbl193.Text = GuaGanZhi[2].ToString();
            lbl194.Text = GuaGanZhi[3].ToString();
            lbl195.Text = GuaGanZhi[4].ToString();
            lbl196.Text = GuaGanZhi[5].ToString();
            GuaGanZhi = op.setupDiZhi(changeGuaCode);
            lbl191.Text += GuaGanZhi[0].ToString();
            lbl192.Text += GuaGanZhi[1].ToString();
            lbl193.Text += GuaGanZhi[2].ToString();
            lbl194.Text += GuaGanZhi[3].ToString();
            lbl195.Text += GuaGanZhi[4].ToString();
            lbl196.Text += GuaGanZhi[5].ToString();

        }
        #endregion

        private void showSelf()
        {
            Arrange op = new Arrange();
            string self = op.setupGuaSelf(originalGuaCode);
            lbls091.Text = (self[0].ToString() == "0")? "": (self[0].ToString() == "1") ? "世" : "應";
            lbls092.Text = (self[1].ToString() == "0") ? "" : (self[1].ToString() == "1") ? "世" : "應";
            lbls093.Text = (self[2].ToString() == "0") ? "" : (self[2].ToString() == "1") ? "世" : "應";
            lbls094.Text = (self[3].ToString() == "0") ? "" : (self[3].ToString() == "1") ? "世" : "應";
            lbls095.Text = (self[4].ToString() == "0") ? "" : (self[4].ToString() == "1") ? "世" : "應";
            lbls096.Text = (self[5].ToString() == "0") ? "" : (self[5].ToString() == "1") ? "世" : "應"; 
        }

        private void showSixFamily()
        {
            Arrange op = new Arrange();
            string[] sixfamily = op.setupGuaFamily(originalGuaCode);
            lblr091.Text = sixfamily[0].ToString();
            lblr092.Text = sixfamily[1].ToString();
            lblr093.Text = sixfamily[2].ToString();
            lblr094.Text = sixfamily[3].ToString();
            lblr095.Text = sixfamily[4].ToString();
            lblr096.Text = sixfamily[5].ToString();
            string[] vfamily = op.setupGuaConceal(originalGuaCode);
            lblv091.Text = vfamily[0].ToString();
            lblv092.Text = vfamily[1].ToString();
            lblv093.Text = vfamily[2].ToString();
            lblv094.Text = vfamily[3].ToString();
            lblv095.Text = vfamily[4].ToString();
            lblv096.Text = vfamily[5].ToString();
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
            show64Gua();
            showGuaGanZhi();
            showSelf();
            showSixFamily();
            panel1.Visible = false;
            //lblOriginal.Visible = false;
            //lblDown.Visible = false;
            //lblUp.Visible= false;
            //lblChange.Visible = false;
            //lblDownChange.Visible = false;
            //lblUpChange.Visible = false;

            TaiwanLunisolarCalendar Tlc = new TaiwanLunisolarCalendar();

            DateTime dtp = dateTimePicker1.Value;
            textBox2.Text = dtp.Year.ToString() + "/" + dtp.Month.ToString() + "/" + dtp.Day.ToString() + " " + cbxHour.Text + ":" + cbxMin.Text + ":00";
            DateTime dt = Convert.ToDateTime(textBox2.Text.Trim());
            //DateTime dt = Convert.ToDateTime(dateTimePicker1.Value.ToString()+" "+cbxHour.Text+":"+cbxMin.Text+":00");
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
            dayInfo.Append("正確的年干支：" + c.GanZhiYearBy24 + "\r\n");//正確的年干支干支  
            dayInfo.Append("正確的月干支：" + c.GanZhiMonthBy24 + "\r\n");//正確的月干支  
            dayInfo.Append("正確的日干支：" + c.GanZhiDayBy24 + "\r\n");//正確的日干支 
            dayInfo.Append("正確的時干支：" + c.GanZhiHourBy24 + "\r\n");//正確的時干支             
            dayInfo.Append("當旬空亡：" + c.DiZhiDayPeriodEmptyDie + "\r\n");//當旬空亡             
            dayInfo.Append("星宿：" + c.ChineseConstellation + "\r\n");//星宿
            dayInfo.Append("星座：" + c.Constellation + "\r\n");//星座
            textBox3.Text = dayInfo.ToString();
            lblYear.Text = "年：" + c.GanZhiYearBy24;
            lblMonth.Text = "月：" + c.GanZhiMonthBy24;
            lblDay.Text = "日：" + c.GanZhiDayBy24;
            lblHour.Text = "時：" + c.GanZhiHourBy24;
            lblEmptyDie.Text = "空亡：" + c.DiZhiDayPeriodEmptyDie;
        }
    }
}
