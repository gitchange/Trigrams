//類名:EcanChineseCalendar
//作用:農歷類
//作者：劉典武
//時間：2010-12-01

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trigrams
{
    #region ChineseCalendarException
    /// <summary>
    /// 中國日歷異常處理
    /// </summary>
    public class newCalendarException : System.Exception
    {
        public newCalendarException(string msg)
            : base(msg)
        {
        }
    }
    #endregion
    public class EcanChineseCalendar
    {
        #region 內部結構
        private struct SolarHolidayStruct//陽歷
        {
            public int Month;
            public int Day;
            public int Recess; //假期長度
            public string HolidayName;
            public SolarHolidayStruct(int month, int day, int recess, string name)
            {
                Month = month;
                Day = day;
                Recess = recess;
                HolidayName = name;
            }
        }
        private struct LunarHolidayStruct//農歷
        {
            public int Month;
            public int Day;
            public int Recess;
            public string HolidayName;
            public LunarHolidayStruct(int month, int day, int recess, string name)
            {
                Month = month;
                Day = day;
                Recess = recess;
                HolidayName = name;
            }
        }
        private struct WeekHolidayStruct
        {
            public int Month;
            public int WeekAtMonth;
            public int WeekDay;
            public string HolidayName;
            public WeekHolidayStruct(int month, int weekAtMonth, int weekDay, string name)
            {
                Month = month;
                WeekAtMonth = weekAtMonth;
                WeekDay = weekDay;
                HolidayName = name;
            }
        }
        #endregion
        #region 內部變量
        private DateTime _date;
        private DateTime _datetime;
        private int _cYear;
        private int _cMonth;
        private int _cDay;
        private bool _cIsLeapMonth; //當月是否閏月
        private bool _cIsLeapYear; //當年是否有閏月
        #endregion
        #region 基礎數據
        #region 基本常量
        private const int MinYear = 1900;
        private const int MaxYear = 2050;
        private static DateTime MinDay = new DateTime(1900, 1, 30);
        private static DateTime MaxDay = new DateTime(2049, 12, 31);
        private const int GanZhiStartYear = 1864; //干支計算起始年
        private static DateTime GanZhiStartDay = new DateTime(1899, 12, 22);//起始日
        private const string HZNum = "零一二三四五六七八九";
        private const int AnimalStartYear = 1900; //1900年為鼠年
        private static DateTime ChineseConstellationReferDay = new DateTime(2007, 9, 13);//28星宿參考值,本日為角
        #endregion
        #region 陰歷數據
        /// <summary>
        /// 來源於網上的農歷數據
        /// </summary>
        /// <remarks>
        /// 數據結構如下，共使用17位數據
        /// 第17位：表示閏月天數，0表示29天 1表示30天
        /// 第16位-第5位（共12位）表示12個月，其中第16位表示第一月，如果該月為30天則為1，29天為0
        /// 第4位-第1位（共4位）表示閏月是哪個月，如果當年沒有閏月，則置0
        ///</remarks>
        private static int[] LunarDateArray = new int[]{
0x04BD8,0x04AE0,0x0A570,0x054D5,0x0D260,0x0D950,0x16554,0x056A0,0x09AD0,0x055D2,
0x04AE0,0x0A5B6,0x0A4D0,0x0D250,0x1D255,0x0B540,0x0D6A0,0x0ADA2,0x095B0,0x14977,
0x04970,0x0A4B0,0x0B4B5,0x06A50,0x06D40,0x1AB54,0x02B60,0x09570,0x052F2,0x04970,
0x06566,0x0D4A0,0x0EA50,0x06E95,0x05AD0,0x02B60,0x186E3,0x092E0,0x1C8D7,0x0C950,
0x0D4A0,0x1D8A6,0x0B550,0x056A0,0x1A5B4,0x025D0,0x092D0,0x0D2B2,0x0A950,0x0B557,
0x06CA0,0x0B550,0x15355,0x04DA0,0x0A5B0,0x14573,0x052B0,0x0A9A8,0x0E950,0x06AA0,
0x0AEA6,0x0AB50,0x04B60,0x0AAE4,0x0A570,0x05260,0x0F263,0x0D950,0x05B57,0x056A0,
0x096D0,0x04DD5,0x04AD0,0x0A4D0,0x0D4D4,0x0D250,0x0D558,0x0B540,0x0B6A0,0x195A6,
0x095B0,0x049B0,0x0A974,0x0A4B0,0x0B27A,0x06A50,0x06D40,0x0AF46,0x0AB60,0x09570,
0x04AF5,0x04970,0x064B0,0x074A3,0x0EA50,0x06B58,0x055C0,0x0AB60,0x096D5,0x092E0,
0x0C960,0x0D954,0x0D4A0,0x0DA50,0x07552,0x056A0,0x0ABB7,0x025D0,0x092D0,0x0CAB5,
0x0A950,0x0B4A0,0x0BAA4,0x0AD50,0x055D9,0x04BA0,0x0A5B0,0x15176,0x052B0,0x0A930,
0x07954,0x06AA0,0x0AD50,0x05B52,0x04B60,0x0A6E6,0x0A4E0,0x0D260,0x0EA65,0x0D530,
0x05AA0,0x076A3,0x096D0,0x04BD7,0x04AD0,0x0A4D0,0x1D0B6,0x0D250,0x0D520,0x0DD45,
0x0B5A0,0x056D0,0x055B2,0x049B0,0x0A577,0x0A4B0,0x0AA50,0x1B255,0x06D20,0x0ADA0,
0x14B63
};
        #endregion
        #region 星座名稱
        private static string[] _constellationName =
{
"牡羊座", "金牛座", "雙子座",
"巨蟹座", "獅子座", "處女座",
"天秤座", "天蠍座", "射手座",
"摩羯座", "水瓶座", "雙魚座"
};
        #endregion
        #region 二十四節氣
        private static string[] _lunarHolidayName =
{
"小寒", "大寒", "立春", "雨水",
"驚蟄", "春分", "清明", "穀雨",
"立夏", "小滿", "芒種", "夏至",
"小暑", "大暑", "立秋", "處暑",
"白露", "秋分", "寒露", "霜降",
"立冬", "小雪", "大雪", "冬至"
};
        #endregion
        #region 二十八星宿
        private static string[] _chineseConstellationName =
{
//四 五 六 日 一 二 三
"角木蛟","亢金龍","女土蝠","房日兔","心月狐","尾火虎","箕水豹",
"斗木獬","牛金牛","氐土貉","虛日鼠","危月燕","室火豬","壁水?",
"奎木狼","婁金狗","胃土彘","昴日雞","畢月烏","觜火猴","參水猿",
"井木犴","鬼金羊","柳土獐","星日馬","張月鹿","翼火蛇","軫水蚓"
};
        #endregion
        #region 節氣數據
        private static string[] SolarTerm = new string[] { "小寒", "大寒", "立春", "雨水", "驚蟄", "春分", "清明", "穀雨", "立夏", "小滿", "芒種", "夏至", "小暑", "大暑", "立秋", "處暑", "白露", "秋分", "寒露", "霜降", "立冬", "小雪", "大雪", "冬至" };
        private static int[] sTermInfo = new int[] { 0, 21208, 42467, 63836, 85337, 107014, 128867, 150921, 173149, 195551, 218072, 240693, 263343, 285989, 308563, 331033, 353350, 375494, 397447, 419210, 440795, 462224, 483532, 504758 };
        #endregion
        #region 農歷相關數據
        private static string ganStr = "甲乙丙丁戊己庚辛壬癸";
        private static string zhiStr = "子丑寅卯辰巳午未申酉戌亥";
        private static string animalStr = "鼠牛虎兔龍蛇馬羊猴雞狗豬";
        private static string nStr1 = "日一二三四五六七八九";
        private static string nStr2 = "初十廿卅";
        private static string[] _monthString =
{
"出錯","正月","二月","三月","四月","五月","六月","七月","八月","九月","十月","十一月","臘月"
};
        #endregion
        #region 按公歷計算的節日
        private static SolarHolidayStruct[] sHolidayInfo = new SolarHolidayStruct[]{
new SolarHolidayStruct(1, 1, 1, "元旦"),
new SolarHolidayStruct(2, 2, 0, "世界濕地日"),
new SolarHolidayStruct(2, 10, 0, "國際氣象節"),
new SolarHolidayStruct(2, 14, 0, "情人節"),
new SolarHolidayStruct(3, 1, 0, "國際海豹日"),
new SolarHolidayStruct(3, 5, 0, "學雷鋒紀念日"),
new SolarHolidayStruct(3, 8, 0, "婦女節"),
new SolarHolidayStruct(3, 12, 0, "植樹節 孫中山逝世紀念日"),
new SolarHolidayStruct(3, 14, 0, "國際警察日"),
new SolarHolidayStruct(3, 15, 0, "消費者權益日"),
new SolarHolidayStruct(3, 17, 0, "中國國醫節 國際航海日"),
new SolarHolidayStruct(3, 21, 0, "世界森林日 消除種族歧視國際日 世界兒歌日"),
new SolarHolidayStruct(3, 22, 0, "世界水日"),
new SolarHolidayStruct(3, 24, 0, "世界防治結核病日"),
new SolarHolidayStruct(4, 1, 0, "愚人節"),
new SolarHolidayStruct(4, 7, 0, "世界衛生日"),
new SolarHolidayStruct(4, 22, 0, "世界地球日"),
new SolarHolidayStruct(5, 1, 1, "勞動節"),
new SolarHolidayStruct(5, 2, 1, "勞動節假日"),
new SolarHolidayStruct(5, 3, 1, "勞動節假日"),
new SolarHolidayStruct(5, 4, 0, "青年節"),
new SolarHolidayStruct(5, 8, 0, "世界紅十字日"),
new SolarHolidayStruct(5, 12, 0, "國際護士節"),
new SolarHolidayStruct(5, 31, 0, "世界無煙日"),
new SolarHolidayStruct(6, 1, 0, "國際兒童節"),
new SolarHolidayStruct(6, 5, 0, "世界環境保護日"),
new SolarHolidayStruct(6, 26, 0, "國際禁毒日"),
new SolarHolidayStruct(7, 1, 0, "建黨節 香港回歸紀念 世界建築日"),
new SolarHolidayStruct(7, 11, 0, "世界人口日"),
new SolarHolidayStruct(8, 1, 0, "建軍節"),
new SolarHolidayStruct(8, 8, 0, "中國男子節 父親節"),
new SolarHolidayStruct(8, 15, 0, "抗日戰爭勝利紀念"),
new SolarHolidayStruct(9, 9, 0, "毛主席逝世紀念"),
new SolarHolidayStruct(9, 10, 0, "教師節"),
new SolarHolidayStruct(9, 18, 0, "九·一八事變紀念日"),
new SolarHolidayStruct(9, 20, 0, "國際愛牙日"),
new SolarHolidayStruct(9, 27, 0, "世界旅游日"),
new SolarHolidayStruct(9, 28, 0, "孔子誕辰"),
new SolarHolidayStruct(10, 1, 1, "國慶節 國際音樂日"),
new SolarHolidayStruct(10, 2, 1, "國慶節假日"),
new SolarHolidayStruct(10, 3, 1, "國慶節假日"),
new SolarHolidayStruct(10, 6, 0, "老人節"),
new SolarHolidayStruct(10, 24, 0, "聯合國日"),
new SolarHolidayStruct(11, 10, 0, "世界青年節"),
new SolarHolidayStruct(11, 12, 0, "孫中山誕辰紀念"),
new SolarHolidayStruct(12, 1, 0, "世界艾滋病日"),
new SolarHolidayStruct(12, 3, 0, "世界殘疾人日"),
new SolarHolidayStruct(12, 20, 0, "澳門回歸紀念"),
new SolarHolidayStruct(12, 24, 0, "平安夜"),
new SolarHolidayStruct(12, 25, 0, "聖誕節"),
new SolarHolidayStruct(12, 26, 0, "毛主席誕辰紀念")
};
        #endregion
        #region 按農歷計算的節日
        private static LunarHolidayStruct[] lHolidayInfo = new LunarHolidayStruct[]{
new LunarHolidayStruct(1, 1, 1, "春節"),
new LunarHolidayStruct(1, 15, 0, "元宵節"),
new LunarHolidayStruct(5, 5, 0, "端午節"),
new LunarHolidayStruct(7, 7, 0, "七夕情人節"),
new LunarHolidayStruct(7, 15, 0, "中元節 盂蘭盆節"),
new LunarHolidayStruct(8, 15, 0, "中秋節"),
new LunarHolidayStruct(9, 9, 0, "重陽節"),
new LunarHolidayStruct(12, 8, 0, "臘八節"),
new LunarHolidayStruct(12, 23, 0, "北方小年(掃房)"),
new LunarHolidayStruct(12, 24, 0, "南方小年(撣塵)"),
//new LunarHolidayStruct(12, 30, 0, "除夕") //注意除夕需要其它方法進行計算
};
        #endregion
        #region 按某月第幾個星期幾
        private static WeekHolidayStruct[] wHolidayInfo = new WeekHolidayStruct[]{
new WeekHolidayStruct(5, 2, 1, "母親節"),
new WeekHolidayStruct(5, 3, 1, "全國助殘日"),
new WeekHolidayStruct(6, 3, 1, "父親節"),
new WeekHolidayStruct(9, 3, 3, "國際和平日"),
new WeekHolidayStruct(9, 4, 1, "國際聾人節"),
new WeekHolidayStruct(10, 1, 2, "國際住房日"),
new WeekHolidayStruct(10, 1, 4, "國際減輕自然災害日"),
new WeekHolidayStruct(11, 4, 5, "感恩節")
};
        #endregion
        #endregion
        #region 建構函數 new EcanChineseCalendar(DateTime)
        #region ChinaCalendar <公歷日期初始化>
        /// <summary>
        /// 用一個標準的公歷日期來初使化
        /// </summary>
        /// <param name="dt"></param>
        public EcanChineseCalendar(DateTime dt)
        {
            int i;
            int leap;
            int temp;
            int offset;


            CheckDateLimit(dt);
            _date = dt.Date;
            _datetime = dt;
            //農歷日期計算部分
            leap = 0;
            temp = 0;
            TimeSpan ts = _date - EcanChineseCalendar.MinDay;//計算兩天的基本差距
            offset = ts.Days;
            for (i = MinYear; i <= MaxYear; i++)
            {
                temp = GetChineseYearDays(i); //求當年農歷年天數
                if (offset - temp < 1)
                    break;
                else
                {
                    offset = offset - temp;
                }
            }
            _cYear = i;
            leap = GetChineseLeapMonth(_cYear);//計算該年閏哪個月
            //設定當年是否有閏月
            if (leap > 0)
            {
                _cIsLeapYear = true;
            }
            else
            {
                _cIsLeapYear = false;
            }
            _cIsLeapMonth = false;
            for (i = 1; i <= 12; i++)
            {
                //閏月
                if ((leap > 0) && (i == leap + 1) && (_cIsLeapMonth == false))
                {
                    _cIsLeapMonth = true;
                    i = i - 1;
                    temp = GetChineseLeapMonthDays(_cYear); //計算閏月天數
                }
                else
                {
                    _cIsLeapMonth = false;
                    temp = GetChineseMonthDays(_cYear, i);//計算非閏月天數
                }
                offset = offset - temp;
                if (offset <= 0) break;
            }
            offset = offset + temp;
            _cMonth = i;
            _cDay = offset;
        }
        #endregion
        #region ChinaCalendar <農歷日期初始化>
        /// <summary>
        /// 用農歷的日期來初使化
        /// </summary>
        /// <param name="cy">農歷年</param>
        /// <param name="cm">農歷月</param>
        /// <param name="cd">農歷日</param>
        /// <param name="LeapFlag">閏月標志</param>
        public EcanChineseCalendar(int cy, int cm, int cd, bool leapMonthFlag)
        {
            int i, leap, Temp, offset;
            CheckChineseDateLimit(cy, cm, cd, leapMonthFlag);
            _cYear = cy;
            _cMonth = cm;
            _cDay = cd;
            offset = 0;
            for (i = MinYear; i < cy; i++)
            {
                Temp = GetChineseYearDays(i); //求當年農歷年天數
                offset = offset + Temp;
            }
            leap = GetChineseLeapMonth(cy);// 計算該年應該閏哪個月
            if (leap != 0)
            {
                this._cIsLeapYear = true;
            }
            else
            {
                this._cIsLeapYear = false;
            }
            if (cm != leap)
            {
                _cIsLeapMonth = false; //當前日期並非閏月
            }
            else
            {
                _cIsLeapMonth = leapMonthFlag; //使用用戶輸入的是否閏月月份
            }
            if ((_cIsLeapYear == false) || //當年沒有閏月
            (cm < leap)) //計算月份小於閏月
            {
                #region ...
                for (i = 1; i < cm; i++)
                {
                    Temp = GetChineseMonthDays(cy, i);//計算非閏月天數
                    offset = offset + Temp;
                }
                //檢查日期是否大於最大天
                if (cd > GetChineseMonthDays(cy, cm))
                {
                    throw new newCalendarException("不合法的農歷日期");
                }
                offset = offset + cd; //加上當月的天數
                #endregion
            }
            else //是閏年，且計算月份大於或等於閏月
            {
                #region ...
                for (i = 1; i < cm; i++)
                {
                    Temp = GetChineseMonthDays(cy, i); //計算非閏月天數
                    offset = offset + Temp;
                }
                if (cm > leap) //計算月大於閏月
                {
                    Temp = GetChineseLeapMonthDays(cy); //計算閏月天數
                    offset = offset + Temp; //加上閏月天數
                    if (cd > GetChineseMonthDays(cy, cm))
                    {
                        throw new newCalendarException("不合法的農歷日期");
                    }
                    offset = offset + cd;
                }
                else //計算月等於閏月
                {
                    //如果需要計算的是閏月，則應首先加上與閏月對應的普通月的天數
                    if (this._cIsLeapMonth == true) //計算月為閏月
                    {
                        Temp = GetChineseMonthDays(cy, cm); //計算非閏月天數
                        offset = offset + Temp;
                    }
                    if (cd > GetChineseLeapMonthDays(cy))
                    {
                        throw new newCalendarException("不合法的農歷日期");
                    }
                    offset = offset + cd;
                }
                #endregion
            }
            _date = MinDay.AddDays(offset);
        }
        #endregion
        #endregion
        #region 私有函數
        #region GetChineseMonthDays (傳回農歷 y年m月的總天數)
        //傳回農歷 y年m月的總天數
        private int GetChineseMonthDays(int year, int month)
        {
            if (BitTest32((LunarDateArray[year - MinYear] & 0x0000FFFF), (16 - month)))
            {
                return 30;
            }
            else
            {
                return 29;
            }
        }
        #endregion
        #region GetChineseLeapMonth (傳回農歷 y年閏哪個月 1-12 , 沒閏傳回 0)
        //傳回農歷 y年閏哪個月 1-12 , 沒閏傳回 0
        private int GetChineseLeapMonth(int year)
        {
            return LunarDateArray[year - MinYear] & 0xF;
        }
        #endregion
        #region GetChineseLeapMonthDays (傳回農歷 y年閏月的天數)
        //傳回農歷 y年閏月的天數
        private int GetChineseLeapMonthDays(int year)
        {
            if (GetChineseLeapMonth(year) != 0)
            {
                if ((LunarDateArray[year - MinYear] & 0x10000) != 0)
                {
                    return 30;
                }
                else
                {
                    return 29;
                }
            }
            else
            {
                return 0;
            }
        }
        #endregion
        #region GetChineseYearDays (取農歷年一年的天數)
        /// <summary>
        /// 取農歷年一年的天數
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private int GetChineseYearDays(int year)
        {
            int i, f, sumDay, info;
            sumDay = 348; //29天 X 12個月
            i = 0x8000;
            info = LunarDateArray[year - MinYear] & 0x0FFFF;
            //計算12個月中有多少天為30天
            for (int m = 0; m < 12; m++)
            {
                f = info & i;
                if (f != 0)
                {
                    sumDay++;
                }
                i = i >> 1;
            }
            return sumDay + GetChineseLeapMonthDays(year);
        }
        #endregion
        #region GetChineseHour (獲得當前時間的時辰)
        /// <summary>
        /// 獲得當前時間的時辰
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        ///
        private string GetChineseHour(DateTime dt)
        {
            int _hour, _minute, offset, i;
            int indexGan;
            //string ganHour, zhiHour;
            string tmpGan;
            //計算時辰的地支
            _hour = dt.Hour; //獲得當前時間小時
            _minute = dt.Minute; //獲得當前時間分鐘
            if (_minute != 0) _hour += 1;
            offset = _hour / 2;
            if (offset >= 12) offset = 0;
            //zhiHour = zhiStr[offset].ToString();
            //計算天干
            TimeSpan ts = this._date - GanZhiStartDay;
            i = ts.Days % 60;
            indexGan = ((i % 10 + 1) * 2 - 1) % 10 - 1; //ganStr[i % 10] 為日的天干,(n*2-1) %10得出地支對應,n從1開始
            tmpGan = ganStr.Substring(indexGan) + ganStr.Substring(0, indexGan + 2);//湊齊12位
            //ganHour = ganStr[((i % 10 + 1) * 2 - 1) % 10 - 1].ToString();
            return tmpGan[offset].ToString() + zhiStr[offset].ToString();
        }
        #endregion
        #region CheckDateLimit (檢查公歷日期是否符合要求)
        /// <summary>
        /// 檢查公歷日期是否符合要求
        /// </summary>
        /// <param name="dt"></param>
        private void CheckDateLimit(DateTime dt)
        {
            if ((dt < MinDay) || (dt > MaxDay))
            {
                throw new newCalendarException("超出可轉換的日期");
            }
        }
        #endregion
        #region CheckChineseDateLimit (檢查農歷日期是否合理)
        /// <summary>
        /// 檢查農歷日期是否合理
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="leapMonth"></param>
        private void CheckChineseDateLimit(int year, int month, int day, bool leapMonth)
        {
            if ((year < MinYear) || (year > MaxYear))
            {
                throw new newCalendarException("非法農歷日期");
            }
            if ((month < 1) || (month > 12))
            {
                throw new newCalendarException("非法農歷日期");
            }
            if ((day < 1) || (day > 30)) //中國的月最多30天
            {
                throw new newCalendarException("非法農歷日期");
            }
            int leap = GetChineseLeapMonth(year);// 計算該年應該閏哪個月
            if ((leapMonth == true) && (month != leap))
            {
                throw new newCalendarException("非法農歷日期");
            }
        }
        #endregion
        #region ConvertNumToChineseNum (將0-9轉成漢字形式)
        /// <summary>
        /// 將0-9轉成漢字形式
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private string ConvertNumToChineseNum(char n)
        {
            if ((n < '0') || (n > '9')) return "";
            switch (n)
            {
                case '0':
                    return HZNum[0].ToString();
                case '1':
                    return HZNum[1].ToString();
                case '2':
                    return HZNum[2].ToString();
                case '3':
                    return HZNum[3].ToString();
                case '4':
                    return HZNum[4].ToString();
                case '5':
                    return HZNum[5].ToString();
                case '6':
                    return HZNum[6].ToString();
                case '7':
                    return HZNum[7].ToString();
                case '8':
                    return HZNum[8].ToString();
                case '9':
                    return HZNum[9].ToString();
                default:
                    return "";
            }
        }
        #endregion
        #region BitTest32 (測試某位是否為真)
        /// <summary>
        /// 測試某位是否為真
        /// </summary>
        /// <param name="num"></param>
        /// <param name="bitpostion"></param>
        /// <returns></returns>
        private bool BitTest32(int num, int bitpostion)
        {
            if ((bitpostion > 31) || (bitpostion < 0))
                throw new Exception("Error Param: bitpostion[0-31]:" + bitpostion.ToString());
            int bit = 1 << bitpostion;
            if ((num & bit) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
        #region ConvertDayOfWeek (將星期幾轉成數字表示)
        /// <summary>
        /// 將星期幾轉成數字表示
        /// </summary>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        private int ConvertDayOfWeek(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return 1;
                case DayOfWeek.Monday:
                    return 2;
                case DayOfWeek.Tuesday:
                    return 3;
                case DayOfWeek.Wednesday:
                    return 4;
                case DayOfWeek.Thursday:
                    return 5;
                case DayOfWeek.Friday:
                    return 6;
                case DayOfWeek.Saturday:
                    return 7;
                default:
                    return 0;
            }
        }
        #endregion
        #region CompareWeekDayHoliday (比較當天是不是指定的第週幾)
        /// <summary>
        /// 比較當天是不是指定的第週幾
        /// </summary>
        /// <param name="date"></param>
        /// <param name="month"></param>
        /// <param name="week"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        private bool CompareWeekDayHoliday(DateTime date, int month, int week, int day)
        {
            bool ret = false;
            if (date.Month == month) //月份相同
            {
                if (ConvertDayOfWeek(date.DayOfWeek) == day) //星期幾相同
                {
                    DateTime firstDay = new DateTime(date.Year, date.Month, 1);//生成當月第一天
                    int i = ConvertDayOfWeek(firstDay.DayOfWeek);
                    int firWeekDays = 7 - ConvertDayOfWeek(firstDay.DayOfWeek) + 1; //計算第一周剩餘天數
                    if (i > day)
                    {
                        if ((week - 1) * 7 + day + firWeekDays == date.Day)
                        {
                            ret = true;
                        }
                    }
                    else
                    {
                        if (day + firWeekDays + (week - 2) * 7 == date.Day)
                        {
                            ret = true;
                        }
                    }
                }
            }
            return ret;
        }
        #endregion
        #region Get24DaysDateTime (取得目前所在節氣的時間點)
        /// <summary>
        /// 取得目前所在節氣的時間點
        /// </summary>
        /// <param name="pdatetime">傳入計算日期</param>
        /// <returns></returns>
        public DateTime Get24DaysDateTime(DateTime pdatetime)
        {
            DateTime baseDateAndTime = new DateTime(1900, 1, 6, 2, 5, 0); //#1/6/1900 2:05:00 AM#
            DateTime newDate = DateTime.Now;
            double num;
            int y;
            int c;
            y = pdatetime.Year;
            for (int i = 1; i <= 24; i++)
            {
                num = 525948.76 * (y - 1900) + sTermInfo[i-1];
                newDate = baseDateAndTime.AddMinutes(num);//按分鐘計算
                if (newDate >= pdatetime)
                {
                    if (i - 2 < 0) c = 23; else c = i - 2;
                    num = 525948.76 * (y - 1900) + sTermInfo[c];
                    newDate = baseDateAndTime.AddMinutes(num);//按分鐘計算
                    break;
                }
            }
            return newDate;
        } 
        #endregion
        #region Get24DaysCurrentName (取得目前所在節氣的名稱)
        /// <summary>
        /// 取得目前所在節氣的名稱
        /// </summary>
        /// <param name="pdatetime">傳入計算日期</param>
        /// <returns></returns>
        public string Get24DaysCurrentName(DateTime pdatetime)
        {
            DateTime baseDateAndTime = new DateTime(1900, 1, 6, 2, 5, 0); //#1/6/1900 2:05:00 AM#
            DateTime newDate;
            double num;
            int y;
            int c;
            string tempStr = "";
            y = pdatetime.Year;
            for (int i = 1; i <= 24; i++)
            {
                num = 525948.76 * (y - 1900) + sTermInfo[i-1];
                newDate = baseDateAndTime.AddMinutes(num);//按分鐘計算
                
                if (newDate >= pdatetime)
                {
                    if (i - 2 < 0) c = 23; else c = i - 2;
                    tempStr = SolarTerm[c];
                    break;
                }
            }
            return tempStr;
        } 
        #endregion
        #endregion
        #region 屬性
        #region 節日
        #region newCalendarHoliday (計算中國農歷節日)
        /// <summary>
        /// 計算中國農歷節日
        /// </summary>
        public string newCalendarHoliday
        {
            get
            {
                string tempStr = "";
                if (this._cIsLeapMonth == false) //閏月不計算節日
                {
                    foreach (LunarHolidayStruct lh in lHolidayInfo)
                    {
                        if ((lh.Month == this._cMonth) && (lh.Day == this._cDay))
                        {
                            tempStr = lh.HolidayName;
                            break;
                        }
                    }
                    //對除夕進行特別處理
                    if (this._cMonth == 12)
                    {
                        int i = GetChineseMonthDays(this._cYear, 12); //計算當年農歷12月的總天數
                        if (this._cDay == i) //如果為最後一天
                        {
                            tempStr = "除夕";
                        }
                    }
                }
                return tempStr;
            }
        }
        #endregion
        #region WeekDayHoliday (按某月第幾周第幾日計算的節日)
        /// <summary>
        /// 按某月第幾周第幾日計算的節日
        /// </summary>
        public string WeekDayHoliday
        {
            get
            {
                string tempStr = "";
                foreach (WeekHolidayStruct wh in wHolidayInfo)
                {
                    if (CompareWeekDayHoliday(_date, wh.Month, wh.WeekAtMonth, wh.WeekDay))
                    {
                        tempStr = wh.HolidayName;
                        break;
                    }
                }
                return tempStr;
            }
        }
        #endregion
        #region DateHoliday (按公歷日計算的節日)
        /// <summary>
        /// 按公歷日計算的節日
        /// </summary>
        public string DateHoliday
        {
            get
            {
                string tempStr = "";
                foreach (SolarHolidayStruct sh in sHolidayInfo)
                {
                    if ((sh.Month == _date.Month) && (sh.Day == _date.Day))
                    {
                        tempStr = sh.HolidayName;
                        break;
                    }
                }
                return tempStr;
            }
        }
        #endregion
        #endregion
        #region 公歷日期
        #region Date (取對應的公歷日期)
        /// <summary>
        /// 取對應的公歷日期
        /// </summary>
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        #endregion
        #region WeekDay (取星期幾)
        /// <summary>
        /// 取星期幾
        /// </summary>
        public DayOfWeek WeekDay
        {
            get { return _date.DayOfWeek; }
        }
        #endregion
        #region WeekDayStr (星期幾)
        /// <summary>
        /// 星期幾
        /// </summary>
        public string WeekDayStr
        {
            get
            {
                switch (_date.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        return "星期日";
                    case DayOfWeek.Monday:
                        return "星期一";
                    case DayOfWeek.Tuesday:
                        return "星期二";
                    case DayOfWeek.Wednesday:
                        return "星期三";
                    case DayOfWeek.Thursday:
                        return "星期四";
                    case DayOfWeek.Friday:
                        return "星期五";
                    default:
                        return "星期六";
                }
            }
        }
        #endregion
        #region DateString (公歷日期中文表示法 如一九九七年七月一日)
        /// <summary>
        /// 公歷日期中文表示法 如一九九七年七月一日
        /// </summary>
        public string DateString
        {
            get
            {
                return "公元" + this._date.ToLongDateString();
            }
        }
        #endregion
        #region IsLeapYear (當前是否公歷閏年)
        /// <summary>
        /// 當前是否公歷閏年
        /// </summary>
        public bool IsLeapYear
        {
            get
            {
                return DateTime.IsLeapYear(this._date.Year);
            }
        }
        #endregion
        #region ChineseConstellation (28星宿計算)
        /// <summary>
        /// 28星宿計算
        /// </summary>
        public string ChineseConstellation
        {
            get
            {
                int offset = 0;
                int modStarDay = 0;
                TimeSpan ts = this._date - ChineseConstellationReferDay;
                offset = ts.Days;
                modStarDay = offset % 28;
                return (modStarDay >= 0 ? _chineseConstellationName[modStarDay] : _chineseConstellationName[27 + modStarDay]);
            }
        }
        #endregion
        #region ChineseHour (時辰)
        /// <summary>
        /// 時辰
        /// </summary>
        public string ChineseHour
        {
            get
            {
                return GetChineseHour(_datetime);
            }
        }
        #endregion
        #region ChineseTwentyFourDay (定氣法計算二十四節氣)
        /// <summary>
        /// 定氣法計算二十四節氣,二十四節氣是按地球公轉來計算的，並非是陰歷計算的
        /// </summary>
        /// <remarks>
        /// 節氣的定法有兩種。古代歷法采用的稱為"恆氣"，即按時間把一年等分為24份，
        /// 每一節氣平均得15天有餘，所以又稱"平氣"。現代農歷采用的稱為"定氣"，即
        /// 按地球在軌道上的位置為標準，一周360°，兩節氣之間相隔15°。由於冬至時地
        /// 球位於近日點附近，運動速度較快，因而太陽在黃道上移動15°的時間不到15天。
        /// 夏至前後的情況正好相反，太陽在黃道上移動較慢，一個節氣達16天之多。采用
        /// 定氣時可以保証春、秋兩分必然在晝夜平分的那兩天。
        /// </remarks>
        #region ChineseTwentyFourDay (當前日期所在的節氣名稱)
        public string ChineseTwentyFourDay
        {
            get
            {
                DateTime baseDateAndTime = new DateTime(1900, 1, 6, 2, 5, 0); //#1/6/1900 2:05:00 AM#
                DateTime newDate;
                double num;
                int y;
                string tempStr = "";
                y = this._date.Year;
                for (int i = 1; i <= 24; i++)
                {
                    num = 525948.76 * (y - 1900) + sTermInfo[i - 1];
                    newDate = baseDateAndTime.AddMinutes(num);//按分鐘計算
                    if (newDate.DayOfYear == _date.DayOfYear)
                    {
                        tempStr = SolarTerm[i - 1];
                        break;
                    }
                }
                return tempStr;
            }
        } 
        #endregion
        #region ChineseTwentyFourPrevDay (當前日期前一個最近節氣)
        /// <summary>
        /// 當前日期前一個最近節氣
        /// </summary>
        public string ChineseTwentyFourPrevDay
        {
            get
            {
                DateTime baseDateAndTime = new DateTime(1900, 1, 6, 2, 5, 0); //#1/6/1900 2:05:00 AM#
                DateTime newDate;
                double num;
                int y;
                string tempStr = "";
                y = this._date.Year;
                for (int i = 24; i >= 1; i--)
                {
                    num = 525948.76 * (y - 1900) + sTermInfo[i - 1];
                    newDate = baseDateAndTime.AddMinutes(num);//按分鐘計算
                    if (newDate.DayOfYear < _date.DayOfYear)
                    {
                        tempStr = string.Format("{0}[{1}]", SolarTerm[i - 1], newDate.ToString("yyyy-MM-dd"));
                        break;
                    }
                }
                return tempStr;
            }
        } 
        #endregion
        #region ChineseTwentyFourNextDay (當前日期後一個最近節氣)
        /// <summary>
        /// 當前日期後一個最近節氣
        /// </summary>
        public string ChineseTwentyFourNextDay
        {
            get
            {
                DateTime baseDateAndTime = new DateTime(1900, 1, 6, 2, 5, 0); //#1/6/1900 2:05:00 AM#
                DateTime newDate;
                double num;
                int y;
                string tempStr = "";
                y = this._date.Year;
                for (int i = 1; i <= 24; i++)
                {
                    num = 525948.76 * (y - 1900) + sTermInfo[i - 1];
                    newDate = baseDateAndTime.AddMinutes(num);//按分鐘計算
                    if (newDate.DayOfYear > _date.DayOfYear)
                    {
                        tempStr = string.Format("{0}[{1}]", SolarTerm[i - 1], newDate.ToString("yyyy-MM-dd"));
                        break;
                    }
                }
                return tempStr;
            }
        } 
        #endregion
        #region Chinese24DaysDateTime (當前日期所在的節氣時間)
        /// <summary>
        /// 當前日期所在的節氣時間
        /// </summary>
        public DateTime Chinese24DaysDateTime
        {
            get
            {
                return Get24DaysDateTime(this._datetime);
            }
        } 
        #endregion
        #region Chinese24DaysCurrentName (當前日期所在的節氣名稱)
        /// <summary>
        /// 當前日期所在的節氣名稱
        /// </summary>
        public string Chinese24DaysCurrentName
        {
            get
            {
                return Get24DaysCurrentName(this._datetime);
            }
        } 
        #endregion
        #endregion
        #region IsOverYearSpring (判斷傳入日期是否過了當年的立春日)
        /// <summary>
        /// 判斷傳入日期是否過了當年的立春日
        /// </summary>
        public bool IsOverYearSpring
        {
            get
            {
                bool isoveryearspring = true;
                SolarTerm solar = new SolarTerm();
                //每年的立春約在 2/4 ~ 2/6 期間
                DateTime springDateTime = solar.Get24DaysDateTime(Convert.ToDateTime(this._date.Year.ToString() + "/02/08")); //求當年度立春時間
                if (this._datetime >= springDateTime)
                    isoveryearspring = true;
                else
                    isoveryearspring = false;

                return isoveryearspring;
            }
        }
        #endregion
        #region IsOverMonthBy24 (判斷傳入日期是否過了當月的節氣)
        /// <summary>
        /// 判斷傳入日期是否過了當月的節氣
        /// </summary>
        public bool IsOverMonthBy24
        {
            get
            {
                bool isovermonth = true;
                SolarTerm solar = new SolarTerm();
                DateTime current24DateTime = solar.Get24DaysDateTime(this._datetime); //求目前所在時間的節氣時間
                if (this._datetime.Month == current24DateTime.Month)
                    isovermonth = true;
                else
                    isovermonth = false;

                return isovermonth;
            }
        }
        #endregion
        #endregion
        #region 農歷日期
        #region IsChineseLeapMonth (是否閏月)
        /// <summary>
        /// 是否閏月
        /// </summary>
        public bool IsChineseLeapMonth
        {
            get { return this._cIsLeapMonth; }
        }
        #endregion
        #region IsChineseLeapYear (當年是否有閏月)
        /// <summary>
        /// 當年是否有閏月
        /// </summary>
        public bool IsChineseLeapYear
        {
            get
            {
                return this._cIsLeapYear;
            }
        }
        #endregion
        #region ChineseDay (農歷日)
        /// <summary>
        /// 農歷日
        /// </summary>
        public int ChineseDay
        {
            get { return this._cDay; }
        }
        #endregion
        #region ChineseDayString (農歷日中文表示)
        /// <summary>
        /// 農歷日中文表示
        /// </summary>
        public string ChineseDayString
        {
            get
            {
                switch (this._cDay)
                {
                    case 0:
                        return "";
                    case 10:
                        return "初十";
                    case 20:
                        return "二十";
                    case 30:
                        return "三十";
                    default:
                        return nStr2[(int)(_cDay / 10)].ToString() + nStr1[_cDay % 10].ToString();
                }
            }
        }
        #endregion
        #region ChineseMonth (農歷的月份)
        /// <summary>
        /// 農歷的月份
        /// </summary>
        public int ChineseMonth
        {
            get { return this._cMonth; }
        }
        #endregion
        #region ChineseMonthString (農歷月份字符串)
        /// <summary>
        /// 農歷月份字符串
        /// </summary>
        public string ChineseMonthString
        {
            get
            {
                return _monthString[this._cMonth];
            }
        }
        #endregion
        #region ChineseYear (取農歷年份)
        /// <summary>
        /// 取農歷年份
        /// </summary>
        public int ChineseYear
        {
            get { return this._cYear; }
        }
        #endregion
        #region ChineseYearString (取農歷年字符串如，一九九七年)
        /// <summary>
        /// 取農歷年字符串如，一九九七年
        /// </summary>
        public string ChineseYearString
        {
            get
            {
                string tempStr = "";
                string num = this._cYear.ToString();
                for (int i = 0; i < 4; i++)
                {
                    tempStr += ConvertNumToChineseNum(num[i]);
                }
                return tempStr + "年";
            }
        }
        #endregion
        #region ChineseDateString (取農歷日期表示法：農歷一九九七年正月初五)
        /// <summary>
        /// 取農歷日期表示法：農歷一九九七年正月初五
        /// </summary>
        public string ChineseDateString
        {
            get
            {
                if (this._cIsLeapMonth == true)
                {
                    return "農歷" + ChineseYearString + "閏" + ChineseMonthString + ChineseDayString;
                }
                else
                {
                    return "農歷" + ChineseYearString + ChineseMonthString + ChineseDayString;
                }
            }
        }
        #endregion
        #endregion
        #region 星座
        #region Constellation (計算指定日期的星座序號)
        /// <summary>
        /// 計算指定日期的星座序號
        /// </summary>
        /// <returns></returns>
        public string Constellation
        {
            get
            {
                int index = 0;
                int y, m, d;
                y = _date.Year;
                m = _date.Month;
                d = _date.Day;
                y = m * 100 + d;
                if (((y >= 321) && (y <= 419))) { index = 0; }
                else if ((y >= 420) && (y <= 520)) { index = 1; }
                else if ((y >= 521) && (y <= 620)) { index = 2; }
                else if ((y >= 621) && (y <= 722)) { index = 3; }
                else if ((y >= 723) && (y <= 822)) { index = 4; }
                else if ((y >= 823) && (y <= 922)) { index = 5; }
                else if ((y >= 923) && (y <= 1022)) { index = 6; }
                else if ((y >= 1023) && (y <= 1121)) { index = 7; }
                else if ((y >= 1122) && (y <= 1221)) { index = 8; }
                else if ((y >= 1222) || (y <= 119)) { index = 9; }
                else if ((y >= 120) && (y <= 218)) { index = 10; }
                else if ((y >= 219) && (y <= 320)) { index = 11; }
                else { index = 0; }
                return _constellationName[index];
            }
        }
        #endregion
        #endregion
        #region 生肖
        #region Animal (計算生肖的索引)
        /// <summary>
        /// 計算屬相的索引，注意雖然屬相是以農歷年來區別的，但是目前在實際使用中是按公歷來計算的
        /// 鼠年為1,其它類推
        /// </summary>
        public int Animal
        {
            get
            {
                int offset = _date.Year - AnimalStartYear;
                return (offset % 12) + 1;
            }
        }
        #endregion
        #region AnimalString (取生肖字符串)
        /// <summary>
        /// 取屬相字符串
        /// </summary>
        public string AnimalString
        {
            get
            {
                int offset = _date.Year - AnimalStartYear; //陽歷計算
                //int offset = this._cYear - AnimalStartYear; 農歷計算
                return animalStr[offset % 12].ToString();
            }
        }
        #endregion
        #endregion
        #region 天干地支
        #region GanZhiYearString (取農歷年的干支)
        /// <summary>
        /// 取農歷年的干支表示法如 乙丑年
        /// </summary>
        public string GanZhiYearString
        {
            get
            {
                string tempStr;
                int i = (this._cYear - GanZhiStartYear) % 60; //計算干支
                tempStr = ganStr[i % 10].ToString() + zhiStr[i % 12].ToString() + "年";
                return tempStr;
            }
        }
        #endregion
        #region GanZhiMonthString (取干支的月表示字符串)
        /// <summary>
        /// 取干支的月表示字符串，注意農歷的閏月不記干支
        /// </summary>
        public string GanZhiMonthString
        {
            get
            {
                //每個月的地支總是固定的,而且總是從寅月開始
                int zhiIndex;
                string zhi;
                if (this._cMonth > 10)
                {
                    zhiIndex = this._cMonth - 10;
                }
                else
                {
                    zhiIndex = this._cMonth + 2;
                }
                zhi = zhiStr[zhiIndex - 1].ToString();
                //根據當年的干支年的干來計算月干的第一個
                int ganIndex = 1;
                string gan;
                int i = (this._cYear - GanZhiStartYear) % 60; //計算干支
                switch (i % 10)
                {
                    #region ...
                    case 0: //甲
                        ganIndex = 3;
                        break;
                    case 1: //乙
                        ganIndex = 5;
                        break;
                    case 2: //丙
                        ganIndex = 7;
                        break;
                    case 3: //丁
                        ganIndex = 9;
                        break;
                    case 4: //戊
                        ganIndex = 1;
                        break;
                    case 5: //己
                        ganIndex = 3;
                        break;
                    case 6: //庚
                        ganIndex = 5;
                        break;
                    case 7: //辛
                        ganIndex = 7;
                        break;
                    case 8: //壬
                        ganIndex = 9;
                        break;
                    case 9: //癸
                        ganIndex = 1;
                        break;
                    #endregion
                }
                gan = ganStr[(ganIndex + this._cMonth - 2) % 10].ToString();
                return gan + zhi + "月";
            }
        }
        #endregion
        #region GanZhiDayString (取干支日表示法)
        /// <summary>
        /// 取干支日表示法
        /// </summary>
        public string GanZhiDayString
        {
            get
            {
                int i, offset;
                TimeSpan ts = this._date - GanZhiStartDay;
                offset = ts.Days;
                i = offset % 60;
                return ganStr[i % 10].ToString() + zhiStr[i % 12].ToString() + "日";
            }
        }
        #endregion
        #region GanZhiDateString (取當前日期的干支表示法)
        /// <summary>
        /// 取當前日期的干支表示法如 甲子年乙醜月丙庚日
        /// </summary>
        public string GanZhiDateString
        {
            get
            {
                return GanZhiYearString + GanZhiMonthString + GanZhiDayString;
            }
        }
        #endregion
        #region GanZhiYearBy24 (取年的干支，以二十四節氣看)
        /// <summary>
        /// 取年的干支 (By 二十四節氣看) 表示法如 乙丑年
        /// </summary>
        public string GanZhiYearBy24
        {
            get
            {
                string tempStr;
                int y = this._date.Year;
                if (!this.IsOverYearSpring) y -= 1;

                int i = (y - GanZhiStartYear) % 60; //計算干支
                tempStr = ganStr[i % 10].ToString() + zhiStr[i % 12].ToString();
                return tempStr;
            }
        }
        #endregion
        #region GanZhiMonthBy24 (取年的干支，以二十四節氣看)
        /// <summary>
        /// 取月的干支 (By 二十四節氣看) 表示法如 乙丑月
        /// </summary>
        public string GanZhiMonthBy24
        {
            get
            {
                //每個月的地支總是固定的,而且總是從寅月開始
                int zhiIndex;
                string zhi;
                int i = (this._date.Year - GanZhiStartYear) % 60; //計算干支
                int mon = this._date.Month;
                if (!IsOverMonthBy24)  //判斷是否為當月的節氣
                {
                    mon = this._date.Month - 1;
                    if (mon==0)
                    {
                        mon = 12;
                        i = (this._date.Year - 1 - GanZhiStartYear) % 60; //重新計算干支
                    }                    
                }
                if (mon == 12)
                {
                    zhiIndex = 0;
                }
                else
                {
                    zhiIndex = mon;
                }
                zhi = zhiStr[zhiIndex].ToString();
                //根據當年的干支年的干來計算月干的第一個
                int ganIndex = 1;
                string gan;    
                int yy = (i%10);
                switch (i % 10)
                {
                    #region ...
                    case 0: //甲
                        ganIndex = 3;
                        break;
                    case 1: //乙
                        ganIndex = 5;
                        break;
                    case 2: //丙
                        ganIndex = 7;
                        break;
                    case 3: //丁
                        ganIndex = 9;
                        break;
                    case 4: //戊
                        ganIndex = 1;
                        break;
                    case 5: //己
                        ganIndex = 3;
                        break;
                    case 6: //庚
                        ganIndex = 5;
                        break;
                    case 7: //辛
                        ganIndex = 7;
                        break;
                    case 8: //壬
                        ganIndex = 9;
                        break;
                    case 9: //癸
                        ganIndex = 1;
                        break;
                    #endregion
                }
                gan = ganStr[(ganIndex + mon - 3) % 10].ToString();
                return gan + zhi ;
            }
        }
        #endregion
        #region GanZhiDayBy24 (取日的干支)
        /// <summary>
        /// 取干支日表示法
        /// </summary>
        public string GanZhiDayBy24
        {
            get
            {
                int i, offset;
                TimeSpan ts = this._date - GanZhiStartDay;
                offset = ts.Days;
                i = offset % 60;
                return ganStr[i % 10].ToString() + zhiStr[i % 12].ToString();
            }
        }
        #endregion
        #region GanZhiHourBy24 (取時辰的干支)
        /// <summary>
        /// GanZhiHourBy24 (取時辰的干支)
        /// </summary>
        public string GanZhiHourBy24
        {
            get
            {
                return GetChineseHour(_datetime);
            }                
        }
        #endregion
        #region DiZhiDayPeriodEmptyDie (取當旬空亡)
        /// <summary>
        /// DiZhiDayPeriodEmptyDie (取當旬空亡)
        /// </summary>
        public string DiZhiDayPeriodEmptyDie
        {
            get
            {
                int i, offset , ganNum , zhiNum , first , second;
                TimeSpan ts = this._date - GanZhiStartDay;
                offset = ts.Days;
                i = offset % 60;
                ganNum = (i % 10) +1;
                zhiNum = (i % 12)+ 1;
                second = zhiNum - ganNum;
                if (second <= 0) second += 12;
                first = second - 1;
                return zhiStr[first - 1].ToString() + zhiStr[second - 1].ToString();
            }
        } 
        #endregion
        #endregion
        #endregion
        #region 方法
        #region NextDay (取下一天)
        /// <summary>
        /// 取下一天
        /// </summary>
        /// <returns></returns>
        public EcanChineseCalendar NextDay()
        {
            DateTime nextDay = _date.AddDays(1);
            return new EcanChineseCalendar(nextDay);
        }
        #endregion
        #region PervDay (取前一天)
        /// <summary>
        /// 取前一天
        /// </summary>
        /// <returns></returns>
        public EcanChineseCalendar PervDay()
        {
            DateTime pervDay = _date.AddDays(-1);
            return new EcanChineseCalendar(pervDay);
        }
        #endregion
        #endregion
    }
}