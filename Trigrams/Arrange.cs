using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
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
            switch (insidecode)
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
            switch (insidecode)
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

    }
}
