namespace ConverterTest
{
    using Smart.Text.Japanese;

    using Xunit;

    public class KanaConverter2Test
    {
        // Roman

        private const string RomanNarrow =
            "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        private const string RomanWide =
            "ＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺａｂｃｄｅｆｇｈｉｊｋｌｍｎｏｐｑｒｓｔｕｖｗｘｙｚ";

        // Numeric

        private const string NumericNarrow =
            "0123456789";

        private const string NumericWide =
            "０１２３４５６７８９";

        // ASCII

        private const string AsciiNarrow =
            "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";

        private const string AsciiWide =
            "！”＃＄％＆’（）＊＋，－．／：；＜＝＞？＠［￥］＾＿｀｛｜｝￣";

        // Space

        private const string SpaceNarrow =
            " ";

        private const string SpaceWide =
            "　";

        //// Kana

        //private const string KanaNarrow =
        //    "ｧｨｩｪｫｬｭｮｯｰｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜｦﾝﾞﾟ";

        //private const string KanaWide =
        //    "ァィゥェォャュョッーアイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワヲン゛゜";

        //private const string HiraganaWide =
        //    "ぁぃぅぇぉゃゅょっーあいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもやゆよらりるれろわをん゛゜";

        //// KanaDakuon
        //private const string KanaDakuonNarrow =
        //    "ｶﾞｷﾞｸﾞｹﾞｺﾞｻﾞｼﾞｽﾞｾﾞｿﾞﾀﾞﾁﾞﾂﾞﾃﾞﾄﾞﾊﾞﾋﾞﾌﾞﾍﾞﾎﾞｳﾞﾜﾞｦﾞ";
        //private const string KanaDakuonWide =
        //    "ガギグゲゴザジズゼゾダヂヅデドバビブベボヴ\u30f7\u30fa";
        //private const string HiraganaDakuonWide =
        //    "がぎぐげござじずぜぞだぢづでどばびぶべぼヴ\u30f7\u30fa";

        private const string Hankana =
            "ｧｱｨｲｩｳｪｴｫｵｶｶﾞｷｷﾞｸｸﾞｹｹﾞｺｺﾞｻｻﾞｼｼﾞｽｽﾞｾｾﾞｿｿﾞﾀﾀﾞﾁﾁﾞｯﾂﾂﾞﾃﾃﾞﾄﾄﾞﾅﾆﾇﾈﾉﾊﾊﾞﾊﾟﾋﾋﾞﾋﾟﾌﾌﾞﾌﾟﾍﾍﾞﾍﾟﾎﾎﾞﾎﾟﾏﾐﾑﾒﾓｬﾔｭﾕｮﾖﾗﾘﾙﾚﾛﾜｦﾝｳﾞ" +
            "ﾞﾟｰ" +
            "｡｢｣､･";

        private const string Katakana =
            "ァアィイゥウェエォオカガキギクグケゲコゴサザシジスズセゼソゾタダチヂッツヅテデトドナニヌネノハバパヒビピフブプヘベペホボポマミムメモャヤュユョヨラリルレロワヲンヴ" +
            "゛゜ー" +
            "。「」、・";

        private const string Hiragana =
            "ぁあぃいぅうぇえぉおかがきぎくぐけげこごさざしじすずせぜそぞただちぢっつづてでとどなにぬねのはばぱひびぴふぶぷへべぺほぼぽまみむめもゃやゅゆょよらりるれろわをん\u3094" +
            "゛゜ー" +
            "。「」、・";

        // todo default引数つきの合計文字列作成、いくつか

        // ------------------------------------------------------------
        // Ascii
        // ------------------------------------------------------------

        // Roman

        [Fact]
        public void TestRomanWideToNarrow()
        {
            Assert.Equal(RomanNarrow, KanaConverter2.Convert(RomanWide, KanaOption.RomanToNarrow));
        }

        [Fact]
        public void TestRomanNarrowToWide()
        {
            Assert.Equal(RomanWide, KanaConverter2.Convert(RomanNarrow, KanaOption.RomanToWide));
        }

        // Numeric

        [Fact]
        public void TestNumericWideToNarrow()
        {
            Assert.Equal(NumericNarrow, KanaConverter2.Convert(NumericWide, KanaOption.NumericToNarrow));
        }

        [Fact]
        public void TestNumericNarrowToWide()
        {
            Assert.Equal(NumericWide, KanaConverter2.Convert(NumericNarrow, KanaOption.NumericToWide));
        }

        // Ascii

        [Fact]
        public void TestAsciiWideToNarrow()
        {
            Assert.Equal(AsciiNarrow, KanaConverter2.Convert(AsciiWide, KanaOption.AsciiToNarrow));
        }

        [Fact]
        public void TestAsciiNarrowToWide()
        {
            Assert.Equal(AsciiWide, KanaConverter2.Convert(AsciiNarrow, KanaOption.AsciiToWide));
        }

        // Space

        [Fact]
        public void TestSpaceWideToNarrow()
        {
            Assert.Equal(SpaceNarrow, KanaConverter2.Convert(SpaceWide, KanaOption.SpaceToNarrow));
        }

        [Fact]
        public void TestSpaceNarrowToWide()
        {
            Assert.Equal(SpaceWide, KanaConverter2.Convert(SpaceNarrow, KanaOption.SpaceToWide));
        }

        // ------------------------------------------------------------
        // Katakana/Hankana
        // ------------------------------------------------------------

        [Fact]
        public void TestKatakanaToHankana()
        {
            Assert.Equal(Hankana, KanaConverter2.Convert(Katakana, KanaOption.KatakanaToHankana));
        }

        // TODO

        // ------------------------------------------------------------
        // Hiragana/Hankana
        // ------------------------------------------------------------

        [Fact]
        public void TestHiraganaToHankana()
        {
            System.Diagnostics.Debug.WriteLine(Hankana);
            System.Diagnostics.Debug.WriteLine(KanaConverter2.Convert(Hiragana, KanaOption.HiraganaToHankana));

            Assert.Equal(Hankana, KanaConverter2.Convert(Hiragana, KanaOption.HiraganaToHankana));
        }

        // TODO

        // ------------------------------------------------------------
        // HiraganaWide/KanaWide
        // ------------------------------------------------------------

        [Fact]
        public void TestKatakanaToHiragana()
        {
            Assert.Equal(Hiragana, KanaConverter2.Convert(Katakana, KanaOption.KatakanaToHiragana));
        }

        [Fact]
        public void TestHiraganaToKatakana()
        {
            Assert.Equal(Katakana, KanaConverter2.Convert(Hiragana, KanaOption.HiraganaToKatakana));
        }
    }
}
