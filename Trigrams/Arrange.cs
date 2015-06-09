using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace Trigrams
{
    /// <summary>
    /// 裝卦類別
    /// </summary>
    class Arrange
    {
        protected List<Trigrams8> trigrams8 = new List<Trigrams8>();
        protected List<Trigrams64> trigrams64 = new List<Trigrams64>();
        protected List<SixLine> sixline = new List<SixLine>();
        protected string[] FiveElement = { "金", "水", "木", "火", "土" };
        protected string[] StrongWeak = { "旺", "相", "死", "囚", "休" };
        protected string[] GuaFamily = { "兄弟", "子孫", "妻財", "官鬼", "父母" };
        protected void readdata()
        {
            string filename1 = "Trigrams8Data.json";
            string filename2 = "Trigrams64Data.json";
            string filename3 = "SixLineData.json";
            string fs1 = Trigrams.Program.ReadJsonFile(filename1);
            string fs2 = Trigrams.Program.ReadJsonFile(filename2);
            string fs3 = Trigrams.Program.ReadJsonFile(filename3);
            JavaScriptSerializer json = new JavaScriptSerializer();
            trigrams8 = json.Deserialize<List<Trigrams8>>(fs1);
            trigrams64 = json.Deserialize<List<Trigrams64>>(fs2);
            sixline = json.Deserialize<List<SixLine>>(fs3);
        }

        #region 取得八卦資訊
        /// <summary>
        /// 取得八卦資訊
        /// </summary>
        /// <param name="pcode">0:少陰;1:少陽</param>
        /// <returns>回傳: List<<Trigrams8>> </returns>
        public List<Trigrams8> get8Gua(string pcode)
        {
            string newcode = pcode;
            readdata();
            var find = from gua in trigrams8
                       where gua.code == newcode
                       select gua;
            return find.ToList();
        }
        #endregion

        #region 取得六十四卦資訊
        /// <summary>
        /// 取得六十四卦資訊
        /// </summary>
        /// <param name="pgua">本卦(0)或變卦(1)</param>
        /// <param name="pcode">0:少陰;1:少陽;2:老陰;3:老陽</param>
        /// <returns>回傳: List<<Trigrams64>> </returns>
        public List<Trigrams64> get64Gua(int pgua, string pcode)
        {
            string newcode = string.Empty;
            for (int i = 0; i < pcode.Length; i++)
            {
                switch (pcode.Substring(i, 1))
                {
                    case "0":
                        newcode += "0";
                        break;
                    case "1":
                        newcode += "1";
                        break;
                    case "2":
                        if (pgua == 0)
                            newcode += "0";
                        else
                            newcode += "1";
                        break;
                    case "3":
                        if (pgua == 0)
                            newcode += "1";
                        else
                            newcode += "0";
                        break;
                }
            }
            readdata();
            var find = from gua in trigrams64
                       where gua.code == newcode
                       select gua;
            return find.ToList();
        }
        #endregion

        #region 為六十四卦裝天干
        /// <summary>
        /// 為六十四卦裝天干 
        /// 乾納甲壬坎納戊
        /// 離納己土震納庚
        /// 坤納乙癸巽納辛
        /// 艮納丙火兌納丁
        /// </summary>
        /// <param name="pcode">傳入六十四卦 Code</param>
        /// <returns></returns>
        public string setupTenGan(string pcode)
        {
            string inside = string.Empty, outside = string.Empty, insidecode, outsidecode;
            insidecode = pcode.Substring(0, 3);
            outsidecode = pcode.Substring(3, 3);
            //先判斷內卦
            switch (insidecode)
            {
                case "111": //乾
                    inside = "甲甲甲";
                    break;
                case "010": //坎
                    inside = "戊戊戊";
                    break;
                case "101": //離
                    inside = "己己己";
                    break;
                case "100": //震
                    inside = "庚庚庚";
                    break;
                case "000": //坤
                    inside = "乙乙乙";
                    break;
                case "011": //巽
                    inside = "辛辛辛";
                    break;
                case "001": //艮
                    inside = "丙丙丙";
                    break;
                case "110": //兌
                    inside = "丁丁丁";
                    break;
            }
            //後判斷外卦
            switch (outsidecode)
            {
                case "111": //乾
                    outside = "壬壬壬";
                    break;
                case "010": //坎
                    outside = "戊戊戊";
                    break;
                case "101": //離
                    outside = "己己己";
                    break;
                case "100": //震
                    outside = "庚庚庚";
                    break;
                case "000": //坤
                    outside = "癸癸癸";
                    break;
                case "011": //巽
                    outside = "辛辛辛";
                    break;
                case "001": //艮
                    outside = "丙丙丙";
                    break;
                case "110": //兌
                    outside = "丁丁丁";
                    break;
            }
            return string.Join(null, inside, outside);
        }
        #endregion

        #region 為六十四卦裝地支
        /// <summary>
        /// 為六十四卦裝地支 
        /// 乾、震：子寅辰午申戌（記住初爻納子就行了，其餘的隔一順推）
        /// 坎：寅辰午申戌子（記住初爻納辰就行了，其餘的隔一順推）
        /// 艮：辰午申戌子寅（記住初爻納辰就行了，其餘的隔一順推）
        /// 坤：未巳卯丑亥酉（記住初爻納未就行了，其餘的隔一逆推）
        /// 兌：巳卯丑亥酉未（記住初爻納巳就行了，其餘的隔一逆推）
        /// 離：卯丑亥酉未巳（記住初爻納卯就行了，其餘的隔一逆推）
        /// 巽：丑亥酉未巳卯（記住初爻納未就行了，其餘的隔一逆推）
        /// 規律：四陽卦納陽 ​​支，四陰卦納陰支。陽支順行，陰支逆佈。
        /// </summary>
        /// <param name="pcode">傳入六十四卦 Code</param>
        /// <returns></returns>
        public string setupDiZhi(string pcode)
        {
            string inside = string.Empty, outside = string.Empty, insidecode, outsidecode;
            insidecode = pcode.Substring(0, 3);
            outsidecode = pcode.Substring(3, 3);
            //先判斷內卦
            switch (insidecode)
            {
                case "111": //乾
                    inside = "子寅辰";
                    break;
                case "010": //坎
                    inside = "寅辰午";
                    break;
                case "101": //離
                    inside = "卯丑亥";
                    break;
                case "100": //震
                    inside = "子寅辰";
                    break;
                case "000": //坤
                    inside = "未巳卯";
                    break;
                case "011": //巽
                    inside = "丑亥酉";
                    break;
                case "001": //艮
                    inside = "辰午申";
                    break;
                case "110": //兌
                    inside = "巳卯丑";
                    break;
            }
            //後判斷外卦
            switch (outsidecode)
            {
                case "111": //乾
                    outside = "午申戌";
                    break;
                case "010": //坎
                    outside = "申戌子";
                    break;
                case "101": //離
                    outside = "酉未巳";
                    break;
                case "100": //震
                    outside = "午申戌";
                    break;
                case "000": //坤
                    outside = "丑亥酉";
                    break;
                case "011": //巽
                    outside = "未巳卯";
                    break;
                case "001": //艮
                    outside = "戌子寅";
                    break;
                case "110": //兌
                    outside = "亥酉未";
                    break;
            }
            return string.Join(null, inside, outside);
        }
        #endregion

        #region 為六十四卦裝世應
        /// <summary>
        /// 為六十四卦裝世應
        /// 天同二世天變五, 
        /// 地同四世地變初, 
        /// 本宮六世三世異, 
        /// 人同遊魂人變歸.
        /// </summary>
        /// <param name="pcode"></param>
        /// <returns></returns>
        public string setupGuaSelf(string pcode)
        {
            string result = string.Empty;
            //天同二世
            if (pcode[3 - 1] == pcode[6 - 1] && pcode[2 - 1] != pcode[5 - 1] && pcode[1 - 1] != pcode[4 - 1]) result = "010020";    //[　世　　應　]
            //天變五
            if (pcode[3 - 1] != pcode[6 - 1] && pcode[2 - 1] == pcode[5 - 1] && pcode[1 - 1] == pcode[4 - 1]) result = "020010";    //[　應　　世　]
            //地同四世
            if (pcode[3 - 1] != pcode[6 - 1] && pcode[2 - 1] != pcode[5 - 1] && pcode[1 - 1] == pcode[4 - 1]) result = "200100";    //[應　　世　　]
            //地變初
            if (pcode[3 - 1] == pcode[6 - 1] && pcode[2 - 1] == pcode[5 - 1] && pcode[1 - 1] != pcode[4 - 1]) result = "100200";    //[世　　應　　]
            //本宮六世
            if (pcode[3 - 1] == pcode[6 - 1] && pcode[2 - 1] == pcode[5 - 1] && pcode[1 - 1] == pcode[4 - 1]) result = "002001";    //[　　應　　世]
            //三世異
            if (pcode[3 - 1] != pcode[6 - 1] && pcode[2 - 1] != pcode[5 - 1] && pcode[1 - 1] != pcode[4 - 1]) result = "001002";    //[　　世　　應]
            //人同遊魂
            if (pcode[3 - 1] != pcode[6 - 1] && pcode[2 - 1] == pcode[5 - 1] && pcode[1 - 1] != pcode[4 - 1]) result = "200100";    //[應　　世　　]
            //人變歸
            if (pcode[3 - 1] == pcode[6 - 1] && pcode[2 - 1] != pcode[5 - 1] && pcode[1 - 1] == pcode[4 - 1]) result = "100200";    //[世　　應　　]
            return result;
        }
        #endregion

        #region 為六十四卦裝六親
        /// <summary>
        /// 為六十四卦裝六親
        /// 一二三六外卦宮
        /// </summary>
        /// <param name="pcode"></param>
        /// <returns></returns>
        public string[] setupGuaFamily(string pcode)
        {
            string guacode8 = string.Empty;            
            //若世爻在一二三六，則取外卦宮，反之則為內卦宮
            if (isSelfonInside(pcode))
            {
                guacode8 = pcode.Substring(3, 3);
            }
            else
            {
                guacode8 = pcode.Substring(0, 3);
            }
            //取得八卦的屬性
            List<Trigrams8> get8gua = get8Gua(guacode8);
            string gua8_5element = get8gua[0].element;
            //取得六十四卦的六爻地支
            string str64guadizhi = setupDiZhi(pcode);
            //定義變數
            List<string> list64guadizhi = new List<string>();
            List<string> list64guaelement = new List<string>();
            List<string> list64guafamily = new List<string>();
            GanZhiConvertTo dict = new GanZhiConvertTo();
            Dictionary<string, string> dict5 = dict.DiZhitoFiveAttribute();

            for (int i = 0; i < str64guadizhi.Length; i++)
            {
                list64guadizhi.Add(str64guadizhi[i].ToString());
            }
            foreach (string dizhi in list64guadizhi)
            {
                list64guaelement.Add(dict5[dizhi]);
            }
            
            string[] sixfamily = new string[6];
            //重新編排五行的位置，使宮卦的五行排在第一個位置，並能對應到六親的兄弟
            string[] re5element = offsetList(FiveElement.ToList(), Array.IndexOf(FiveElement, gua8_5element)).ToArray();
            int pos = 0;
            for (int j = 0; j < list64guaelement.Count(); j++)
            {
                pos = Array.IndexOf(re5element, list64guaelement[j].ToString());
                sixfamily[j] = GuaFamily[pos];
            }
            return sixfamily; 
        }
        #endregion

        #region 為六十四卦裝六獸
        /// <summary>
        /// 為六十四卦裝六獸
        /// 卦中裝六獸主要依據測日的天干，訣曰：
        /// 甲乙起青龍，
        /// 丙丁起朱雀；
        /// 戊日起勾陳，
        /// 己日起騰蛇；
        /// 庚辛起白虎，
        /// 壬癸起玄武
        /// </summary>
        /// <param name="ptenganday">傳入測日的天干</param>
        /// <returns></returns>
        public string[] setupGuaSixBeast(string ptenganday)
        {
            string[] beastlist = { "青龍", "朱雀", "勾陳", "騰蛇", "白虎", "玄武" };
            int offsetvalue = 0;

            switch (ptenganday)
            {
                case "甲":
                case "乙":
                    offsetvalue = 0;
                    break;
                case "丙":
                case "丁":
                    offsetvalue = 1;
                    break;
                case "戊":
                    offsetvalue = 2;
                    break;
                case "己":
                    offsetvalue = 3;
                    break;
                case "庚":
                case "辛":
                    offsetvalue = 4;
                    break;
                case "壬":
                case "癸":
                    offsetvalue = 5;
                    break;
            }
            beastlist = offsetList(beastlist.ToList(), offsetvalue).ToArray();
            return beastlist;
        }
        #endregion

        public string[] setupGuaConceal(string pcode)
        {
            string[] get64gua6family = setupGuaFamily(pcode);
            List<string> conceal = new List<string>();
            for (int i = 0; i < GuaFamily.Count(); i++)
            {
                //檢查(收集)卦中無六親的類型
                if (Array.IndexOf(get64gua6family,GuaFamily[i].ToString())<0)
                {
                    conceal.Add(GuaFamily[i].ToString());
                }
            }
            //取得六十四卦的主宮
            string get64guahouse = get64GuaHouse(pcode);
            //取得六十四卦的主宮的 Code
            var find = from gua in trigrams8
                       where gua.name == get64guahouse
                       select gua;
            List<Trigrams8> house = find.ToList();
            //取得主宮的六親
            string[] housefamily = setupGuaFamily(house[0].code + house[0].code);
            //宣告缺爻位的陣列
            string[] less = { "", "", "", "", "", "" };

            int lesspos;
            for (int j = 0 ; j < conceal.Count() ; j++)            
            {
                lesspos = Array.IndexOf(housefamily, conceal[j].ToString());
                //檢查缺少的六親在主宮的爻位，並塞入缺爻位陣列中
                if (lesspos>= 0)
                {
                    less[lesspos] = conceal[j].ToString();
                }                
            }
            return less;
        }

        #region 取得六十四卦的主宮
        /// <summary>
        /// 取得六十四卦的主宮
        /// </summary>
        /// <param name="pcode">傳入六十四卦 Code</param>        
        /// <returns>回傳：六十四卦的主宮</returns>
        protected string get64GuaHouse(string pcode)
        {
            List<Trigrams64> get64gua = get64Gua(0,pcode);
            return get64gua[0].house;
        } 
        #endregion

        #region 判斷"世"爻是否在內卦
        /// <summary>
        /// 判斷"世"爻是否在內卦
        /// </summary>
        /// <param name="pcode">傳入六十四卦 Code</param>
        /// <returns>回傳 true 為是在內卦 ; false 為外卦 </returns>
        protected bool isSelfonInside(string pcode)
        {
            //取得"世"在六十四卦上的爻位
            int self_position = setupGuaSelf(pcode).IndexOf("1") + 1;
            //若世爻在一二三六，則取外卦宮，反之則為內卦宮
            if (Regex.IsMatch(self_position.ToString(), "[1236]"))
            {
                return true;
            }
            else
            {
                return false;
            }
        } 
        #endregion

        #region 物件的位移
        /// <summary>
        /// List(string) 物件的位移
        /// </summary>
        /// <param name="list"> list 物件</param>
        /// <param name="offset"> 位移量 </param>
        /// <returns></returns>
        protected List<string> offsetList(List<string> list, int offset)
        {
            if (offset > list.Count())
            {
                offset = (offset / (offset / list.Count())) - list.Count();
            }
            List<string> result = new List<string>();
            for (int i = offset; i < list.Count(); i++)
            {
                result.Add(list[i]);
            }
            for (int j = 0; j < offset; j++)
            {
                result.Add(list[j]);
            }
            return result;
        } 
        #endregion
    }

    #region 干支字元轉換為數字及五行字典類別
    /// <summary>
    /// 干支字元轉換為數字及五行字典類別
    /// </summary>
    class GanZhiConvertTo
    {
        Dictionary<string, int> dictTenGantoNum;
        Dictionary<string, int> dictDiZhitoNum;
        Dictionary<string, string> dictTenGantoFiveAttribute;
        Dictionary<string, string> dictDiZhitoFiveAttribute;
        public GanZhiConvertTo()
        {
            dictTenGantoNum = new Dictionary<string, int>();
            dictTenGantoNum.Add("甲", 1);
            dictTenGantoNum.Add("乙", 2);
            dictTenGantoNum.Add("丙", 3);
            dictTenGantoNum.Add("丁", 4);
            dictTenGantoNum.Add("戊", 5);
            dictTenGantoNum.Add("己", 6);
            dictTenGantoNum.Add("庚", 7);
            dictTenGantoNum.Add("辛", 8);
            dictTenGantoNum.Add("壬", 9);
            dictTenGantoNum.Add("癸", 10);
            dictDiZhitoNum = new Dictionary<string, int>();
            dictDiZhitoNum.Add("子", 1);
            dictDiZhitoNum.Add("丑", 2);
            dictDiZhitoNum.Add("寅", 3);
            dictDiZhitoNum.Add("卯", 4);
            dictDiZhitoNum.Add("辰", 5);
            dictDiZhitoNum.Add("巳", 6);
            dictDiZhitoNum.Add("午", 7);
            dictDiZhitoNum.Add("未", 8);
            dictDiZhitoNum.Add("申", 9);
            dictDiZhitoNum.Add("酉", 10);
            dictDiZhitoNum.Add("戌", 11);
            dictDiZhitoNum.Add("亥", 12);
            dictTenGantoFiveAttribute = new Dictionary<string, string>();
            dictTenGantoFiveAttribute.Add("甲", "木");
            dictTenGantoFiveAttribute.Add("乙", "木");
            dictTenGantoFiveAttribute.Add("丙", "火");
            dictTenGantoFiveAttribute.Add("丁", "火");
            dictTenGantoFiveAttribute.Add("戊", "土");
            dictTenGantoFiveAttribute.Add("己", "土");
            dictTenGantoFiveAttribute.Add("庚", "金");
            dictTenGantoFiveAttribute.Add("辛", "金");
            dictTenGantoFiveAttribute.Add("壬", "水");
            dictTenGantoFiveAttribute.Add("癸", "水");
            dictDiZhitoFiveAttribute = new Dictionary<string, string>();
            dictDiZhitoFiveAttribute.Add("子", "水");
            dictDiZhitoFiveAttribute.Add("丑", "土");
            dictDiZhitoFiveAttribute.Add("寅", "木");
            dictDiZhitoFiveAttribute.Add("卯", "木");
            dictDiZhitoFiveAttribute.Add("辰", "土");
            dictDiZhitoFiveAttribute.Add("巳", "火");
            dictDiZhitoFiveAttribute.Add("午", "火");
            dictDiZhitoFiveAttribute.Add("未", "土");
            dictDiZhitoFiveAttribute.Add("申", "金");
            dictDiZhitoFiveAttribute.Add("酉", "金");
            dictDiZhitoFiveAttribute.Add("戌", "土");
            dictDiZhitoFiveAttribute.Add("亥", "水");

        }

        public Dictionary<string, int> TenGantoNum()
        {
            return this.dictTenGantoNum;
        }
        public Dictionary<string, int> DiZhitoNum()
        {
            return this.dictDiZhitoNum;
        }
        public Dictionary<string, string> TenGantoFiveAttribute()
        {
            return this.dictTenGantoFiveAttribute;
        }
        public Dictionary<string, string> DiZhitoFiveAttribute()
        {
            return this.dictDiZhitoFiveAttribute;
        }
    }
    #endregion
}
