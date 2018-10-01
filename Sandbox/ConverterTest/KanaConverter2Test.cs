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

        private const string AsciiNarrow =
            "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";

        private const string AsciiWide =
            "Ihfij{C|D^FGHmnOQMobpP";

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

        private const string Hankana =
            "§±¨²©³ª´«µ¶¶Þ··Þ¸¸Þ¹¹ÞººÞ»»Þ¼¼Þ½½Þ¾¾Þ¿¿ÞÀÀÞÁÁÞ¯ÂÂÞÃÃÞÄÄÞÅÆÇÈÉÊÊÞÊßËËÞËßÌÌÞÌßÍÍÞÍßÎÎÞÎßÏÐÑÒÓ¬Ô­Õ®Ö×ØÙÚÛÜ¦Ý³Þ" +
            "Þß°" +
            "¡¢£¤¥";

        private const string Katakana =
            "@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~" +
            "JK[" +
            "BuvAE";

        private const string Hiragana =
            " ¡¢£¤¥¦§¨©ª«¬­®¯°±²³´µ¶·¸¹º»¼½¾¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëíðñ\u3094" +
            "JK[" +
            "BuvAE";

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
