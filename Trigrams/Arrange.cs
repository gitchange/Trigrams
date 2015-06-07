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

    }
}
