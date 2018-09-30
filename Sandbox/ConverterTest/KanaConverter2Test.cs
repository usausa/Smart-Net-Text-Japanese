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
            "`abcdefghijklmnopqrstuvwxy";

        // Numeric

        private const string NumericNarrow =
            "0123456789";

        private const string NumericWide =
            "OPQRSTUVWX";

        // ASCII

        // TODO ê©ÈÉÏX
        private const string AsciiNarrow =
            "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~¡¢£¤¥";

        private const string AsciiWide =
            "Ihfij{C|D^FGHmnOQMobpPBuvAE";

        // Space

        private const string SpaceNarrow =
            " ";

        private const string SpaceWide =
            "@";

        //// Kana

        //private const string KanaNarrow =
        //    "§¨©ª«¬­®¯°±²³´µ¶·¸¹º»¼½¾¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜ¦ÝÞß";

        //private const string KanaWide =
        //    "@BDFHb[ACEGIJLNPRTVXZ\^`cegijklmnqtwz}~JK";

        //private const string HiraganaWide =
        //    "¡£¥§áãåÁ[ ¢¤¦¨©«­¯±³µ·¹»½¿ÂÄÆÈÉÊËÌÍÐÓÖÙÜÝÞßàâäæçèéêëíðñJK";

        //// KanaDakuon

        //private const string KanaDakuonNarrow =
        //    "¶Þ·Þ¸Þ¹ÞºÞ»Þ¼Þ½Þ¾Þ¿ÞÀÞÁÞÂÞÃÞÄÞÊÞËÞÌÞÍÞÎÞ³ÞÜÞ¦Þ";

        //private const string KanaDakuonWide =
        //    "KMOQSUWY[]_adfhorux{\u30f7\u30fa";

        //private const string HiraganaDakuonWide =
        //    "ª¬®°²´¶¸º¼¾ÀÃÅÇÎÑÔ×Ú\u30f7\u30fa";

        //// KanaHandakuon

        //private const string KanaHandakuonNarrow =
        //    "ÊßËßÌßÍßÎß";

        //private const string KanaHandakuonWide =
        //    "psvy|";

        //private const string HiraganaHandakuonWide =
        //    "ÏÒÕØÛ";

        private const string Katakana =
            "@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~";

        private const string Hiragana =
            " ¡¢£¤¥¦§¨©ª«¬­®¯°±²³´µ¶·¸¹º»¼½¾¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñ\u3094\u3095\u3096";

        // todo defaultøÂ«Ìv¶ñì¬A¢­Â©

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
