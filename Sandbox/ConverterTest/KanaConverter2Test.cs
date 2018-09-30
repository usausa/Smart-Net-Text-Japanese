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

        // TODO 一部かなに変更
        private const string AsciiNarrow =
            "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~｡｢｣､･";

        private const string AsciiWide =
            "！”＃＄％＆’（）＊＋，－．／：；＜＝＞？＠［￥］＾＿｀｛｜｝￣。「」、・";

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

        //// KanaHandakuon

        //private const string KanaHandakuonNarrow =
        //    "ﾊﾟﾋﾟﾌﾟﾍﾟﾎﾟ";

        //private const string KanaHandakuonWide =
        //    "パピプペポ";

        //private const string HiraganaHandakuonWide =
        //    "ぱぴぷぺぽ";

        private const string Katakana =
            "ァアィイゥウェエォオカガキギクグケゲコゴサザシジスズセゼソゾタダチヂッツヅテデトドナニヌネノハバパヒビピフブプヘベペホボポマミムメモャヤュユョヨラリルレロヮワヰヱヲンヴヵヶ";

        private const string Hiragana =
            "ぁあぃいぅうぇえぉおかがきぎくぐけげこごさざしじすずせぜそぞただちぢっつづてでとどなにぬねのはばぱひびぴふぶぷへべぺほぼぽまみむめもゃやゅゆょよらりるれろゎわゐゑをん\u3094\u3095\u3096";

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

        //// ------------------------------------------------------------
        //// KanaWide/KanaNarrow
        //// ------------------------------------------------------------

        //[Fact]
        //public void TestKanaWideToKanaNarrow()
        //{
        //    Assert.Equal(KanaNarrow, KanaConverter2.Convert(KanaWide, KanaOption.KanaToNarrow));
        //}

        //[Fact]
        //public void TestKanaDakuonWideToKanaDakuonNarrow()
        //{
        //    Assert.Equal(KanaDakuonNarrow, KanaConverter2.Convert(KanaDakuonWide, KanaOption.KanaToNarrow));
        //}

        //[Fact]
        //public void TestKanaHandakuonWideToKanaHandakuonNarrow()
        //{
        //    Assert.Equal(KanaHandakuonNarrow, KanaConverter2.Convert(KanaHandakuonWide, KanaOption.KanaToNarrow));
        //}

        //[Fact]
        //public void TestKanaNarrowToKanaWide()
        //{
        //    Assert.Equal(KanaWide, KanaConverter2.Convert(KanaNarrow, KanaOption.KanaToWide));
        //}

        //[Fact]
        //public void TestKanaDakuonNarrowToKanaDakuonWide()
        //{
        //    Assert.Equal(KanaDakuonWide, KanaConverter2.Convert(KanaDakuonNarrow, KanaOption.KanaToWide));
        //}

        //[Fact]
        //public void TestKanaHandakuonNarrowToKanaHandakuonWide()
        //{
        //    Assert.Equal(KanaHandakuonWide, KanaConverter2.Convert(KanaHandakuonNarrow, KanaOption.KanaToWide));
        //}

        //// ------------------------------------------------------------
        //// HiraganaWide/KanaNarrow
        //// ------------------------------------------------------------

        //[Fact]
        //public void TestHiraganaWideToKanaNarrow()
        //{
        //    Assert.Equal(KanaNarrow, KanaConverter2.Convert(HiraganaWide, KanaOption.HiraganaToHankana));
        //}

        //[Fact]
        //public void TestHiraganaDakuonWideToKanaDakuonNarrow()
        //{
        //    Assert.Equal(KanaDakuonNarrow, KanaConverter2.Convert(HiraganaDakuonWide, KanaOption.HiraganaToHankana));
        //}

        //[Fact]
        //public void TestHiraganaHandakuonWideToKanaHandakuonNarrow()
        //{
        //    Assert.Equal(KanaHandakuonNarrow, KanaConverter2.Convert(HiraganaHandakuonWide, KanaOption.HiraganaToHankana));
        //}

        //[Fact]
        //public void TestKanaNarrowToHiraganaWide()
        //{
        //    Assert.Equal(HiraganaWide, KanaConverter2.Convert(KanaNarrow, KanaOption.HankanaToHiragana));
        //}

        //[Fact]
        //public void TestKanaDakuonNarrowToHiraganaDakuonWide()
        //{
        //    Assert.Equal(HiraganaDakuonWide, KanaConverter2.Convert(KanaDakuonNarrow, KanaOption.HankanaToHiragana));
        //}

        //[Fact]
        //public void TestKanaHandakuonNarrowToHiraganaHandakuonWide()
        //{
        //    Assert.Equal(HiraganaHandakuonWide, KanaConverter2.Convert(KanaHandakuonNarrow, KanaOption.HankanaToHiragana));
        //}

        //// ------------------------------------------------------------
        //// HiraganaWide/KanaWide
        //// ------------------------------------------------------------

        [Fact]
        public void TestKatakanaToHiragana()
        {
            Assert.Equal(Hiragana, KanaConverter2.Convert(Katakana, KanaOption.KatakanaToHiragana));

            // TODO
        }

        [Fact]
        public void TestHiraganaToKatakana()
        {
            Assert.Equal(Katakana, KanaConverter2.Convert(Hiragana, KanaOption.HiraganaToKatakana));

            // TODO
        }
    }
}
