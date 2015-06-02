using System.Collections.Generic;
using System.IO;

namespace Trigrams
{
    static partial class Program
    {
        public static List<Trigrams8> trigrams8 = new List<Trigrams8>();
        public static List<Trigrams64> trigrams64 = new List<Trigrams64>();

        /// <summary>
        /// 讀取 JSON 檔案
        /// </summary>
        /// <returns></returns>
        public static string ReadJsonFile(string filename)
        {
            string json = string.Empty;            
            using (StreamReader r = new StreamReader(filename))
            {
                json = r.ReadToEnd();
            }
            return json;
        }
    }
}
