using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trigrams
{
    static partial class Program
    {
        public static List<Trigrams8> trigrams8 = new List<Trigrams8>();

        public static void InitData()
        {
            trigrams8.Add(new Trigrams8() { sequence = 1, name = "乾", word = "☰", code = "111", element = "金", natural = "天", temperament = "健", family = "父親", direction = "西北", description = "元亨，利貞。" });
            trigrams8.Add(new Trigrams8() { sequence = 2, name = "兌", word = "☱", code = "110", element = "金", natural = "澤", temperament = "悅", family = "少女", direction = "西", description = "亨，利貞。" });
            trigrams8.Add(new Trigrams8() { sequence = 3, name = "離", word = "☲", code = "101", element = "火", natural = "火", temperament = "麗", family = "中女", direction = "南", description = "利貞，亨。畜牝牛，吉。" });
            trigrams8.Add(new Trigrams8() { sequence = 4, name = "震", word = "☳", code = "100", element = "木", natural = "雷", temperament = "動", family = "長男", direction = "東", description = "亨。震來虩虩，笑言啞啞。震驚百里，不喪匕鬯。" });
            trigrams8.Add(new Trigrams8() { sequence = 5, name = "巽", word = "☴", code = "011", element = "木", natural = "風", temperament = "入", family = "長女", direction = "東南", description = "小亨，利攸往，利見大人。" });
            trigrams8.Add(new Trigrams8() { sequence = 6, name = "坎", word = "☵", code = "010", element = "水", natural = "水", temperament = "陷", family = "中男", direction = "北", description = "習坎，有孚，維心亨，行有尚。" });
            trigrams8.Add(new Trigrams8() { sequence = 7, name = "艮", word = "☶", code = "001", element = "土", natural = "山", temperament = "止", family = "少男", direction = "東北", description = "艮其背，不獲其身，行其庭，不見其人，無咎。" });
            trigrams8.Add(new Trigrams8() { sequence = 8, name = "坤", word = "☷", code = "000", element = "土", natural = "地", temperament = "順", family = "母親", direction = "西南", description = "元亨，利牝馬之貞。君子有攸往，先迷後得主，利西南得朋，東北喪朋。安貞，吉。" });
        }
    }
}
