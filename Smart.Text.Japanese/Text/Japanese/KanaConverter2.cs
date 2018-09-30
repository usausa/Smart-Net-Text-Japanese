using System.Collections.Generic;

namespace Smart.Text.Japanese
{
    using System;
    using System.Diagnostics;
    using System.Linq;

    using System.Runtime.CompilerServices;

    public static class KanaConverter2
    {
        // ASCII
        // TODO 。、「」 の扱い

        private const string AsciiNarrow =
            "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~｡｢｣､･";

        private const string AsciiWide =
            "！”＃＄％＆’（）＊＋，－．／：；＜＝＞？＠［￥］＾＿｀｛｜｝￣。「」、・";

        //// Kana

        //private const string KanaNarrow =
        //    "ｧｨｩｪｫｬｭｮｯｰｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜｦﾝﾞﾟ";

        //private const string KanaWide =
        //    "ァィゥェォャュョッーアイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワヲン゛゜";

        //private const string HiraganaWide =
        //    "ぁぃぅぇぉゃゅょっーあいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもやゆよらりるれろわをん゛゜";

        //// KanaDakuon

        //private const string KanaDakuonNarrow =
        //    "ｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾊﾋﾌﾍﾎｳﾜｦ";

        //private const string KanaDakuonWide =
        //    "ガギグゲゴザジズゼゾダヂヅデドバビブベボヴ\u30f7\u30fa";

        //private const string HiraganaDakuonWide =
        //    "がぎぐげござじずぜぞだぢづでどばびぶべぼヴ\u30f7\u30fa";

        //// KanaHandakuon

        //private const string KanaHandakuonNarrow =
        //    "ﾊﾋﾌﾍﾎ";

        //private const string KanaHandakuonWide =
        //    "パピプペポ";

        //private const string HiraganaHandakuonWide =
        //    "ぱぴぷぺぽ";

        private class Table
        {
            public char Start { get; }

            public char End { get; }

            public char[] Convert { get; }

            public Table(char start, char end, char[] convert)
            {
                Start = start;
                End = end;
                Convert = convert;
            }
        }

        // TODO T4

        private static readonly Table[] AsciiToNarrowTables;

        private static readonly Table[] AsciiToWideTables;

        static KanaConverter2()
        {
            // TODO
            AsciiToNarrowTables = CreateTables(AsciiWide, AsciiNarrow);
            AsciiToWideTables = CreateTables(AsciiNarrow, AsciiWide);
        }

        private static Table[] CreateTables(string from, string to)
        {
            var chars = from.ToCharArray().OrderBy(x => x).ToArray();
            var list = new List<Table>();
            var start = -1;
            for (var i = 0; i < chars.Length; i++)
            {
                if (start == -1)
                {
                    start = i;
                }

                if ((i == chars.Length - 1) || ((i < chars.Length - 1) && (chars[i] + 1 != chars[i + 1])))
                {
                    var convert = new char[i - start + 1];
                    for (var j = 0; j < convert.Length; j++)
                    {
                        convert[j] = to[from.IndexOf(chars[start + j])];
                    }

                    list.Add(new Table(chars[start], chars[i], convert));
                    start = -1;
                }
            }

            return list.ToArray();
        }

        public static string Convert(string src, KanaOption option)
        {
            if (String.IsNullOrEmpty(src))
            {
                return src;
            }

            var isSpaceToNarrow = (option & KanaOption.SpaceToNarrow) == KanaOption.SpaceToNarrow;
            var isSpaceToWide = (option & KanaOption.SpaceToWide) == KanaOption.SpaceToWide;
            var isNumericToNarrow = (option & KanaOption.NumericToNarrow) == KanaOption.NumericToNarrow;
            var isNumericToWide = (option & KanaOption.NumericToWide) == KanaOption.NumericToWide;
            var isRomanToNarrow = (option & KanaOption.RomanToNarrow) == KanaOption.RomanToNarrow;
            var isRomanToWide = (option & KanaOption.RomanToWide) == KanaOption.RomanToWide;
            var isAsciiToNarrow = (option & KanaOption.AsciiToNarrow) == KanaOption.AsciiToNarrow;
            var isAsciiToWide = (option & KanaOption.AsciiToWide) == KanaOption.AsciiToWide;
            var isKatakanaToHiragana = (option & KanaOption.KatakanaToHiragana) == KanaOption.KatakanaToHiragana;
            var isHiraganaToKatakana = (option & KanaOption.HiraganaToKatakana) == KanaOption.HiraganaToKatakana;

            // TODO オプション定数名見直し
            // TODO パラメータ見直し
            var isKanaToNarrow = (option & KanaOption.KanaToNarrow) == KanaOption.KanaToNarrow;
            var isKanaToWide = (option & KanaOption.KanaToWide) == KanaOption.KanaToWide;
            var isHiraganaToHankana = (option & KanaOption.HiraganaToHankana) == KanaOption.HiraganaToHankana;
            var isHankanaToHiragana = (option & KanaOption.HankanaToHiragana) == KanaOption.HankanaToHiragana;

            // TODO unsafe
            var pos = 0;
            var buffer = new char[src.Length * 2];
            for (var i = 0; i < src.Length; i++)
            {
                var c = src[i];

                // Space
                if (isSpaceToNarrow && c == '　')
                {
                    buffer[pos++] = ' ';
                    continue;
                }

                if (isSpaceToWide && c == ' ')
                {
                    buffer[pos++] = '　';
                    continue;
                }

                // Numeric
                if (isNumericToNarrow && NumericToNarrow(c, buffer, ref pos))
                {
                    continue;
                }

                if (isNumericToWide && NumericToWide(c, buffer, ref pos))
                {
                    continue;
                }

                // Roman
                if (isRomanToNarrow && RomanToNarrow(c, buffer, ref pos))
                {
                    continue;
                }

                if (isRomanToWide && RomanToWide(c, buffer, ref pos))
                {
                    continue;
                }

                // Ascii
                if (isAsciiToNarrow && ConvertFromTables(AsciiToNarrowTables, c, buffer, ref pos))
                {
                    continue;
                }

                if (isAsciiToWide && ConvertFromTables(AsciiToWideTables, c, buffer, ref pos))
                {
                    continue;
                }

                // Katakana/Hiragana
                if (isKatakanaToHiragana && KatakanaToHiragana(c, buffer, ref pos))
                {
                    continue;
                }

                if (isHiraganaToKatakana && HiraganaToKatakana(c, buffer, ref pos))
                {
                    continue;
                }

                buffer[pos++] = c;
            }

            return new string(buffer, 0, pos);
        }

        private static bool ConvertFromTables(Table[] tables, char c, char[] buffer, ref int pos)
        {
            for (var i = 0; i < tables.Length; i++)
            {
                var table = tables[i];
                if (c < table.Start)
                {
                    return false;
                }

                if (c <= table.End)
                {
                    buffer[pos++] = table.Convert[c - table.Start];
                    return true;
                }
            }

            return false;
        }

        // Numeric

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool NumericToNarrow(char c, char[] buffer, ref int pos)
        {
            // ０-９
            if ((c >= 0xFF10) && (c <= 0xFF19))
            {
                buffer[pos++] = (char)(c - 0xFF10 + 0x0030);    // TODO 事前計算
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool NumericToWide(char c, char[] buffer, ref int pos)
        {
            // 0-9
            if ((c >= 0x0030) && (c <= 0x0039))
            {
                buffer[pos++] = (char)(c - 0x0030 + 0xFF10);    // TODO 事前計算
                return true;
            }

            return false;
        }

        // Roman

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool RomanToNarrow(char c, char[] buffer, ref int pos)
        {
            // Ａ-Ｚ
            if ((c >= 0xFF21) && (c <= 0xFF3A))
            {
                buffer[pos++] = (char)(c - 0xFF21 + 0x0041);    // TODO 事前計算
                return true;
            }

            // ａ-ｚ
            if ((c >= 0xFF41) && (c <= 0xFF5A))
            {
                buffer[pos++] = (char)(c - 0xFF41 + 0x0061);    // TODO 事前計算
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool RomanToWide(char c, char[] buffer, ref int pos)
        {
            // A-Z
            if ((c >= 0x0041) && (c <= 0x005A))
            {
                buffer[pos++] = (char)(c - 0x0041 + 0xFF21);    // TODO 事前計算
                return true;
            }

            // a-z
            if ((c >= 0x0061) && (c <= 0x007A))
            {
                buffer[pos++] = (char)(c - 0x0061 + 0xFF41);    // TODO 事前計算
                return true;
            }

            return false;
        }

        // TODO kana narrow, wide

        // Hiragana/Katakana

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool KatakanaToHiragana(char c, char[] buffer, ref int pos)
        {
            // ァ-ヶ
            if ((c >= 0x30A1) && (c <= 0x30F6))
            {
                buffer[pos++] = (char)(c - 0x30A1 + 0x3041);    // TODO 事前計算
                return true;
            }

            // TODO ヷ(0x30F7) ～ ヺ(0x30FA)
            // TODO ヽ(0x30FD) ヾ(0x30FE)

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool HiraganaToKatakana(char c, char[] buffer, ref int pos)
        {
            // ぁ-ゖ
            if ((c >= 0x3041) && (c <= 0x3096))
            {
                buffer[pos++] = (char)(c - 0x3041 + 0x30A1);    // TODO 事前計算
                return true;
            }

            // TODO ヷ(0x30F7) ～ ヺ(0x30FA)
            // TODO ヽ(0x30FD) ヾ(0x30FE)

            return false;
        }
    }
}
