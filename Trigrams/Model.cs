using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trigrams
{

    #region 八卦基礎
    /// <summary>
    /// 八卦基礎
    /// </summary>
    class Trigrams8
    {
        /// <summary>
        /// 八卦序號
        /// </summary>
        public int sequence { get; set; }
        /// <summary>
        /// 八卦名(Ex: 震)
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 八卦字(Ex: ☳ )
        /// </summary>
        public string word { get; set; }
        /// <summary>
        /// 八卦碼(Ex: 100)
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 五行代表(金、水、木、火、土)
        /// </summary>
        public string element { get; set; }
        /// <summary>
        /// 自然象徵(Ex: 水、風、火)
        /// </summary>
        public string natural { get; set; }
        /// <summary>
        /// 代表性情
        /// </summary>
        public string temperament { get; set; }
        /// <summary>
        /// 家族關性
        /// </summary>
        public string family { get; set; }
        /// <summary>
        /// 後天八卦方位
        /// </summary>
        public string direction { get; set; }
        /// <summary>
        /// 八卦描述
        /// </summary>
        public string description { get; set; }
    }
    #endregion

    #region 六十四卦
    class Trigrams64
    {
        /// <summary>
        /// 八卦序號
        /// </summary>
        public int sequence { get; set; }
        /// <summary>
        /// 八卦名(Ex: 地天泰)
        /// </summary>
        public string allname { get; set; }
        /// <summary>
        /// 八卦名(Ex: 泰)
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 八卦字(Ex: ☰☷ )
        /// </summary>
        public string word { get; set; }
        /// <summary>
        /// 八卦碼(Ex: 111000)
        /// </summary>
        public string code { get; set; }
    } 
    #endregion
}
