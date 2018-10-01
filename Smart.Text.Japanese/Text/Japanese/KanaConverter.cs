namespace Smart.Text.Japanese
{
    using System;
    using System.Runtime.CompilerServices;

    public static class KanaConverter
    {
        // To KanaNarrow

        private const string ToHankana =
            "ｧｱｨｲｩｳｪｴｫｵｶｶｷｷｸｸｹｹｺｺｻｻｼｼｽｽｾｾｿｿﾀﾀﾁﾁｯﾂﾂﾃﾃﾄﾄﾅﾆﾇﾈﾉﾊﾊﾊﾋﾋﾋﾌﾌﾌﾍﾍﾍﾎﾎﾎﾏﾐﾑﾒﾓｬﾔｭﾕｮﾖﾗﾘﾙﾚﾛ ﾜ  ｦﾝｳ  ";

        private const string ToHankanaType =
            "           ﾞ ﾞ ﾞ ﾞ ﾞ ﾞ ﾞ ﾞ ﾞ ﾞ ﾞ ﾞ  ﾞ ﾞ ﾞ      ﾞﾟ ﾞﾟ ﾞﾟ ﾞﾟ ﾞﾟ                      ﾞ  ";

        // To KanaWide

        private const string ToKatakana =
            "ヲァィゥェォャュョッーアイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワン゛゜";

        private const string ToHiragana =
            "をぁぃぅぇぉゃゅょっーあいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもやゆよらりるれろわん゛゜";

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
            var isKatakanaToHankana = (option & KanaOption.KatakanaToHankana) == KanaOption.KatakanaToHankana;
            var isHankanaToKatakana = (option & KanaOption.HankanaToKatakana) == KanaOption.HankanaToKatakana;
            var isHiraganaToHankana = (option & KanaOption.HiraganaToHankana) == KanaOption.HiraganaToHankana;
            var isHankanaToHiragana = (option & KanaOption.HankanaToHiragana) == KanaOption.HankanaToHiragana;

            // TODO pool
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
                if (isAsciiToNarrow && AsciiToNarrow(c, buffer, ref pos))
                {
                    continue;
                }

                if (isAsciiToWide && AsciiToWide(c, buffer, ref pos))
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

                // Hankana/Katakana
                if (isKatakanaToHankana && KatakanaToHankana(c, buffer, ref pos))
                {
                    continue;
                }

                if (isHankanaToKatakana)
                {
                    var next = i < src.Length - 1 ? src[i + 1] : (char)0;
                    if (HankanaToKatakana(c, next, ref i, buffer, ref pos))
                    {
                        continue;
                    }
                }

                // Hankana/Hiragana
                if (isHiraganaToHankana && HiraganaToHankana(c, buffer, ref pos))
                {
                    continue;
                }

                if (isHankanaToHiragana)
                {
                    var next = i < src.Length - 1 ? src[i + 1] : (char)0;
                    if (HankanaToHiragana(c, next, ref i, buffer, ref pos))
                    {
                        continue;
                    }
                }

                buffer[pos++] = c;
            }

            return new string(buffer, 0, pos);
        }

        // ASCII

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool AsciiToNarrow(char c, char[] buffer, ref int pos)
        {
            // ’
            if (c == 0x2019)
            {
                buffer[pos++] = '\'';
                return true;
            }

            // ”
            if (c == 0x201D)
            {
                buffer[pos++] = '"';
                return true;
            }

            // ！
            if (c == 0xFF01)
            {
                buffer[pos++] = '!';
                return true;
            }

            // ＃＄％＆
            if (c < 0xFF03)
            {
                return false;
            }

            if (c <= 0xFF06)
            {
                buffer[pos++] = "#$%&"[c - 0xFF03];
                return true;
            }

            // （）＊＋，－．／
            if (c < 0xFF08)
            {
                return false;
            }

            if (c <= 0xFF0F)
            {
                buffer[pos++] = "()*+,-./"[c - 0xFF08];
                return true;
            }

            // ：；＜＝＞？＠
            if (c < 0xFF1A)
            {
                return false;
            }

            if (c <= 0xFF20)
            {
                buffer[pos++] = ":;<=>?@"[c - 0xFF1A];
                return true;
            }

            // ［
            if (c == 0xFF3B)
            {
                buffer[pos++] = '[';
                return true;
            }

            // ］＾＿｀
            if (c < 0xFF3D)
            {
                return false;
            }

            if (c <= 0xFF40)
            {
                buffer[pos++] = "]^_`"[c - 0xFF3D];
                return true;
            }

            // ｛｜｝
            if (c < 0xFF5B)
            {
                return false;
            }

            if (c <= 0xFF5D)
            {
                buffer[pos++] = "{|}"[c - 0xFF5B];
                return true;
            }

            // ￣
            if (c == 0xFFE3)
            {
                buffer[pos++] = '~';
                return true;
            }

            // ￥
            if (c == 0xFFE5)
            {
                buffer[pos++] = '\\';
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool AsciiToWide(char c, char[] buffer, ref int pos)
        {
            // !"#$%&'()*+,-./
            if (c < 0x0021)
            {
                return false;
            }

            if (c <= 0x002F)
            {
                buffer[pos++] = "！”＃＄％＆’（）＊＋，－．／"[c - 0x0021];
                return true;
            }

            // :;<=>?@
            if (c < 0x003A)
            {
                return false;
            }

            if (c <= 0x0040)
            {
                buffer[pos++] = "：；＜＝＞？＠"[c - 0x003A];
                return true;
            }

            // [\]^_`
            if (c < 0x005B)
            {
                return false;
            }

            if (c <= 0x0060)
            {
                buffer[pos++] = "［￥］＾＿｀"[c - 0x005B];
                return true;
            }

            // {|}~
            if (c < 0x007B)
            {
                return false;
            }

            if (c <= 0x007E)
            {
                buffer[pos++] = "｛｜｝￣"[c - 0x007B];
                return true;
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
            if (c < 0xFF21)
            {
                return false;
            }

            if (c <= 0xFF3A)
            {
                buffer[pos++] = (char)(c - 0xFF21 + 0x0041);    // TODO 事前計算
                return true;
            }

            // ａ-ｚ
            if (c < 0xFF41)
            {
                return false;
            }

            if (c <= 0xFF5A)
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
            if (c < 0x0041)
            {
                return false;
            }

            if (c <= 0x005A)
            {
                buffer[pos++] = (char)(c - 0x0041 + 0xFF21);    // TODO 事前計算
                return true;
            }

            // a-z
            if (c < 0x0061)
            {
                return false;
            }

            if (c <= 0x007A)
            {
                buffer[pos++] = (char)(c - 0x0061 + 0xFF41);    // TODO 事前計算
                return true;
            }

            return false;
        }

        // Hiragana/Katakana

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool KatakanaToHiragana(char c, char[] buffer, ref int pos)
        {
            // ァ-ヶ
            if (c < 0x30A1)
            {
                return false;
            }

            if (c <= 0x30F6)
            {
                buffer[pos++] = (char)(c - 0x30A1 + 0x3041);    // TODO 事前計算
                return true;
            }

            // 踊り字
            if ((c == 0x30FD) || (c == 0x30FE))
            {
                buffer[pos++] = (char)(c - 0x30FD + 0x309D);    // TODO 事前計算
                return true;
            }

            // ヷ(0x30F7)、ヺ(0x30FA)対象外

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool HiraganaToKatakana(char c, char[] buffer, ref int pos)
        {
            // ぁ-ゖ
            if (c < 0x3041)
            {
                return false;
            }

            if (c <= 0x3096)
            {
                buffer[pos++] = (char)(c - 0x3041 + 0x30A1);    // TODO 事前計算
                return true;
            }

            // 踊り字
            if ((c == 0x309D) || (c == 0x309E))
            {
                buffer[pos++] = (char)(c - 0x309D + 0x30FD);    // TODO 事前計算
                return true;
            }

            // ヷ(0x30F7)、ヺ(0x30FA)対象外

            return false;
        }

        // Hankana/Katakana

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool KatakanaToHankana(char c, char[] buffer, ref int pos)
        {
            // ァ-ヶ
            if ((c >= 0x30A1) && (c <= 0x30F6))
            {
                var index = c - 0x30A1;

                var c1 = ToHankana[index];
                if (c1 != ' ')
                {
                    buffer[pos++] = c1;
                }
                else
                {
                    buffer[pos++] = c;
                }

                var c2 = ToHankanaType[index];
                if (c2 != ' ')
                {
                    buffer[pos++] = c2;
                }

                return true;
            }

            // ワ
            if (c == 0x30F7)
            {
                buffer[pos++] = 'ﾜ';
                buffer[pos++] = 'ﾞ';
                return true;
            }

            // ヲ
            if (c == 0x30FA)
            {
                buffer[pos++] = 'ｦ';
                buffer[pos++] = 'ﾞ';
                return true;
            }

            return KanaSymbolToNarrow(c, buffer, ref pos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool HankanaToKatakana(char c, char next, ref int index, char[] buffer, ref int pos)
        {
            if (next == 'ﾞ')
            {
                // ｶ-ﾄ
                if ((c >= 0xFF76) && (c <= 0xFF84))
                {
                    buffer[pos++] = "ガギグゲゴザジズゼゾダヂヅデド"[c - 0xFF76];
                    index++;
                    return true;
                }

                // ﾊ-ﾎ
                if ((c >= 0xFF8A) && (c <= 0xFF8E))
                {
                    buffer[pos++] = "バビブベボ"[c - 0xFF8A];
                    index++;
                    return true;
                }

                // ｳ
                if (c == 0xFF73)
                {
                    buffer[pos++] = 'ヴ';
                    index++;
                    return true;
                }

                // ﾜ
                if (c == 0xFF9C)
                {
                    buffer[pos++] = '\u30f7';
                    index++;
                    return true;
                }

                // ｦ
                if (c == 0xFF66)
                {
                    buffer[pos++] = '\u30fa';
                    index++;
                    return true;
                }
            }

            if (next == 'ﾟ')
            {
                // ﾊ-ﾎ
                if ((c >= 0xFF8A) && (c <= 0xFF8E))
                {
                    buffer[pos++] = "パピプペポ"[c - 0xFF8A];
                    index++;
                    return true;
                }
            }

            // ｦ-ﾟ
            if ((c >= 0xFF66) & (c <= 0xFF9F))
            {
                buffer[pos++] = ToKatakana[c - 0xFF66];
                return true;
            }

            return HankanaSymbolToWide(c, buffer, ref pos);
        }

        // Hankana/Hiragana

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool HiraganaToHankana(char c, char[] buffer, ref int pos)
        {
            // ァ-ヶ
            if ((c >= 0x3041) && (c <= 0x3096))
            {
                var index = c - 0x3041;

                var c1 = ToHankana[index];
                if (c1 != ' ')
                {
                    buffer[pos++] = c1;
                }
                else
                {
                    buffer[pos++] = c;
                }

                var c2 = ToHankanaType[index];
                if (c2 != ' ')
                {
                    buffer[pos++] = c2;
                }

                return true;
            }

            // ワ(ひらがなオプションでも実行)
            if (c == 0x30F7)
            {
                buffer[pos++] = 'ﾜ';
                buffer[pos++] = 'ﾞ';
                return true;
            }

            // ヲ(ひらがなオプションでも実行)
            if (c == 0x30FA)
            {
                buffer[pos++] = 'ｦ';
                buffer[pos++] = 'ﾞ';
                return true;
            }

            return KanaSymbolToNarrow(c, buffer, ref pos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool HankanaToHiragana(char c, char next, ref int index, char[] buffer, ref int pos)
        {
            if (next == 'ﾞ')
            {
                // ｶ-ﾄ
                if ((c >= 0xFF76) && (c <= 0xFF84))
                {
                    buffer[pos++] = "がぎぐげござじずぜぞだぢづでど"[c - 0xFF76];
                    index++;
                    return true;
                }

                // ﾊ-ﾎ
                if ((c >= 0xFF8A) && (c <= 0xFF8E))
                {
                    buffer[pos++] = "ばびぶべぼ"[c - 0xFF8A];
                    index++;
                    return true;
                }

                // ｳ
                if (c == 0xFF73)
                {
                    buffer[pos++] = '\u3094';
                    index++;
                    return true;
                }

                // ﾜ(カタカナへ変換)
                if (c == 0xFF9C)
                {
                    buffer[pos++] = '\u30f7';
                    index++;
                    return true;
                }

                // ｦ(カタカナへ変換)
                if (c == 0xFF66)
                {
                    buffer[pos++] = '\u30fa';
                    index++;
                    return true;
                }
            }

            if (next == 'ﾟ')
            {
                // ﾊ-ﾎ
                if ((c >= 0xFF8A) && (c <= 0xFF8E))
                {
                    buffer[pos++] = "ぱぴぷぺぽ"[c - 0xFF8A];
                    index++;
                    return true;
                }
            }

            // ｦ-ﾟ
            if ((c >= 0xFF66) & (c <= 0xFF9F))
            {
                buffer[pos++] = ToHiragana[c - 0xFF66];
                return true;
            }

            return HankanaSymbolToWide(c, buffer, ref pos);
        }

        // Kana common

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool KanaSymbolToNarrow(char c, char[] buffer, ref int pos)
        {
            if (c == 'ー')
            {
                buffer[pos++] = 'ｰ';
                return true;
            }

            if (c == '。')
            {
                buffer[pos++] = '｡';
                return true;
            }

            if (c == '「')
            {
                buffer[pos++] = '｢';
                return true;
            }

            if (c == '」')
            {
                buffer[pos++] = '｣';
                return true;
            }

            if (c == '、')
            {
                buffer[pos++] = '､';
                return true;
            }

            if (c == '・')
            {
                buffer[pos++] = '･';
                return true;
            }

            if (c == '゛')
            {
                buffer[pos++] = 'ﾞ';
                return true;
            }

            if (c == '゜')
            {
                buffer[pos++] = 'ﾟ';
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool HankanaSymbolToWide(char c, char[] buffer, ref int pos)
        {
            if ((c >= 0xFF61) && (c <= 0xFF65))
            {
                buffer[pos++] = "。「」、・"[c - 0xFF61];
                return true;
            }

            return false;
        }
    }
}
