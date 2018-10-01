namespace ConverterTest
{
    using Smart.Text.Japanese;

    using Xunit;

    public class KanaConverterTest
    {
        // Roman

        private const string RomanNarrow =
            "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        private const string RomanWide =
            "‚`‚a‚b‚c‚d‚e‚f‚g‚h‚i‚j‚k‚l‚m‚n‚o‚p‚q‚r‚s‚t‚u‚v‚w‚x‚y‚‚‚‚ƒ‚„‚…‚†‚‡‚ˆ‚‰‚Š‚‹‚Œ‚‚‚‚‚‘‚’‚“‚”‚•‚–‚—‚˜‚™‚š";

        // Numeric

        private const string NumericNarrow =
            "0123456789";

        private const string NumericWide =
            "‚O‚P‚Q‚R‚S‚T‚U‚V‚W‚X";

        // ASCII

        private const string AsciiNarrow =
            "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";

        private const string AsciiWide =
            "Ih”“•fij–{C|D^FGƒ„H—mnOQMobpP";

        // Space

        private const string SpaceNarrow =
            " ";

        private const string SpaceWide =
            "@";

        // Kana

        private const string Hankana =
            "§±¨²©³ª´«µ¶¶Ş··Ş¸¸Ş¹¹ŞººŞ»»Ş¼¼Ş½½Ş¾¾Ş¿¿ŞÀÀŞÁÁŞ¯ÂÂŞÃÃŞÄÄŞÅÆÇÈÉÊÊŞÊßËËŞËßÌÌŞÌßÍÍŞÍßÎÎŞÎßÏĞÑÒÓ¬Ô­Õ®Ö×ØÙÚÛÜ¦İ³ŞÜŞ¦Ş" +
            "Şß°" +
            "¡¢£¤¥";

        private const string Katakana =
            "ƒ@ƒAƒBƒCƒDƒEƒFƒGƒHƒIƒJƒKƒLƒMƒNƒOƒPƒQƒRƒSƒTƒUƒVƒWƒXƒYƒZƒ[ƒ\ƒ]ƒ^ƒ_ƒ`ƒaƒbƒcƒdƒeƒfƒgƒhƒiƒjƒkƒlƒmƒnƒoƒpƒqƒrƒsƒtƒuƒvƒwƒxƒyƒzƒ{ƒ|ƒ}ƒ~ƒ€ƒƒ‚ƒƒƒ„ƒ…ƒ†ƒ‡ƒˆƒ‰ƒŠƒ‹ƒŒƒƒƒ’ƒ“ƒ”\u30F7\u30FA" +
            "JK[" +
            "BuvAE";

        private const string Hiragana =
            "‚Ÿ‚ ‚¡‚¢‚£‚¤‚¥‚¦‚§‚¨‚©‚ª‚«‚¬‚­‚®‚¯‚°‚±‚²‚³‚´‚µ‚¶‚·‚¸‚¹‚º‚»‚¼‚½‚¾‚¿‚À‚Á‚Â‚Ã‚Ä‚Å‚Æ‚Ç‚È‚É‚Ê‚Ë‚Ì‚Í‚Î‚Ï‚Ğ‚Ñ‚Ò‚Ó‚Ô‚Õ‚Ö‚×‚Ø‚Ù‚Ú‚Û‚Ü‚İ‚Ş‚ß‚à‚á‚â‚ã‚ä‚å‚æ‚ç‚è‚é‚ê‚ë‚í‚ğ‚ñ\u3094\u30F7\u30FA" +
            "JK[" +
            "BuvAE";

        private const string KatakanaOdoriji =
            "RS";

        private const string HiraganaOdoriji =
            "TU";

        // ------------------------------------------------------------
        // Ascii
        // ------------------------------------------------------------

        // Roman

        [Fact]
        public void TestRomanWideToNarrow()
        {
            Assert.Equal(RomanNarrow, KanaConverter.Convert(RomanWide, KanaOption.RomanToNarrow));
        }

        [Fact]
        public void TestRomanNarrowToWide()
        {
            Assert.Equal(RomanWide, KanaConverter.Convert(RomanNarrow, KanaOption.RomanToWide));
        }

        // Numeric

        [Fact]
        public void TestNumericWideToNarrow()
        {
            Assert.Equal(NumericNarrow, KanaConverter.Convert(NumericWide, KanaOption.NumericToNarrow));
        }

        [Fact]
        public void TestNumericNarrowToWide()
        {
            Assert.Equal(NumericWide, KanaConverter.Convert(NumericNarrow, KanaOption.NumericToWide));
        }

        // Ascii

        [Fact]
        public void TestAsciiWideToNarrow()
        {
            Assert.Equal(AsciiNarrow, KanaConverter.Convert(AsciiWide, KanaOption.AsciiToNarrow));
        }

        [Fact]
        public void TestAsciiNarrowToWide()
        {
            Assert.Equal(AsciiWide, KanaConverter.Convert(AsciiNarrow, KanaOption.AsciiToWide));
        }

        // Space

        [Fact]
        public void TestSpaceWideToNarrow()
        {
            Assert.Equal(SpaceNarrow, KanaConverter.Convert(SpaceWide, KanaOption.SpaceToNarrow));
        }

        [Fact]
        public void TestSpaceNarrowToWide()
        {
            Assert.Equal(SpaceWide, KanaConverter.Convert(SpaceNarrow, KanaOption.SpaceToWide));
        }

        // ------------------------------------------------------------
        // Katakana/Hankana
        // ------------------------------------------------------------

        [Fact]
        public void TestKatakanaToHankana()
        {
            Assert.Equal(Hankana, KanaConverter.Convert(Katakana, KanaOption.KatakanaToHankana));
        }

        [Fact]
        public void TestHankanaToKatakana()
        {
            Assert.Equal(Katakana, KanaConverter.Convert(Hankana, KanaOption.HankanaToKatakana));
        }

        // ------------------------------------------------------------
        // Hiragana/Hankana
        // ------------------------------------------------------------

        [Fact]
        public void TestHiraganaToHankana()
        {
            Assert.Equal(Hankana, KanaConverter.Convert(Hiragana, KanaOption.HiraganaToHankana));
        }

        [Fact]
        public void TestHankanaToHiragana()
        {
            Assert.Equal(Hiragana, KanaConverter.Convert(Hankana, KanaOption.HankanaToHiragana));
        }

        // ------------------------------------------------------------
        // HiraganaWide/KanaWide
        // ------------------------------------------------------------

        [Fact]
        public void TestKatakanaToHiragana()
        {
            Assert.Equal(Hiragana, KanaConverter.Convert(Katakana, KanaOption.KatakanaToHiragana));
            Assert.Equal(HiraganaOdoriji, KanaConverter.Convert(KatakanaOdoriji, KanaOption.KatakanaToHiragana));
        }

        [Fact]
        public void TestHiraganaToKatakana()
        {
            Assert.Equal(Katakana, KanaConverter.Convert(Hiragana, KanaOption.HiraganaToKatakana));
            Assert.Equal(KatakanaOdoriji, KanaConverter.Convert(HiraganaOdoriji, KanaOption.HiraganaToKatakana));
        }
    }
}
