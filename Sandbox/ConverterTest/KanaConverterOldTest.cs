//namespace ConverterTest
//{
//    using Smart.Text.Japanese;

//    using Xunit;

//    public class KanaConverterOldTest
//    {
//        // Roman

//        private const string RomanNarrow =
//            "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

//        private const string RomanWide =
//            "‚`‚a‚b‚c‚d‚e‚f‚g‚h‚i‚j‚k‚l‚m‚n‚o‚p‚q‚r‚s‚t‚u‚v‚w‚x‚y‚‚‚‚ƒ‚„‚…‚†‚‡‚ˆ‚‰‚Š‚‹‚Œ‚‚‚‚‚‘‚’‚“‚”‚•‚–‚—‚˜‚™‚š";

//        // Numeric

//        private const string NumericNarrow =
//            "0123456789";

//        private const string NumericWide =
//            "‚O‚P‚Q‚R‚S‚T‚U‚V‚W‚X";

//        // ASCII

//        private const string AsciiNarrow =
//            "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~¡¢£¤¥";

//        private const string AsciiWide =
//            "Ih”“•fij–{C|D^FGƒ„H—mnOQMobpPBuvAE";

//        // Space

//        private const string SpaceNarrow =
//            " ";

//        private const string SpaceWide =
//            "@";

//        // Kana

//        private const string KanaNarrow =
//            "§¨©ª«¬­®¯°±²³´µ¶·¸¹º»¼½¾¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏĞÑÒÓÔÕÖ×ØÙÚÛÜ¦İŞß";

//        private const string KanaWide =
//            "ƒ@ƒBƒDƒFƒHƒƒƒ…ƒ‡ƒb[ƒAƒCƒEƒGƒIƒJƒLƒNƒPƒRƒTƒVƒXƒZƒ\ƒ^ƒ`ƒcƒeƒgƒiƒjƒkƒlƒmƒnƒqƒtƒwƒzƒ}ƒ~ƒ€ƒƒ‚ƒ„ƒ†ƒˆƒ‰ƒŠƒ‹ƒŒƒƒƒ’ƒ“JK";

//        private const string HiraganaWide =
//            "‚Ÿ‚¡‚£‚¥‚§‚á‚ã‚å‚Á[‚ ‚¢‚¤‚¦‚¨‚©‚«‚­‚¯‚±‚³‚µ‚·‚¹‚»‚½‚¿‚Â‚Ä‚Æ‚È‚É‚Ê‚Ë‚Ì‚Í‚Ğ‚Ó‚Ö‚Ù‚Ü‚İ‚Ş‚ß‚à‚â‚ä‚æ‚ç‚è‚é‚ê‚ë‚í‚ğ‚ñJK";

//        // KanaDakuon

//        private const string KanaDakuonNarrow =
//            "¶Ş·Ş¸Ş¹ŞºŞ»Ş¼Ş½Ş¾Ş¿ŞÀŞÁŞÂŞÃŞÄŞÊŞËŞÌŞÍŞÎŞ³ŞÜŞ¦Ş";

//        private const string KanaDakuonWide =
//            "ƒKƒMƒOƒQƒSƒUƒWƒYƒ[ƒ]ƒ_ƒaƒdƒfƒhƒoƒrƒuƒxƒ{ƒ”\u30f7\u30fa";

//        private const string HiraganaDakuonWide =
//            "‚ª‚¬‚®‚°‚²‚´‚¶‚¸‚º‚¼‚¾‚À‚Ã‚Å‚Ç‚Î‚Ñ‚Ô‚×‚Úƒ”\u30f7\u30fa";

//        // KanaHandakuon

//        private const string KanaHandakuonNarrow =
//            "ÊßËßÌßÍßÎß";

//        private const string KanaHandakuonWide =
//            "ƒpƒsƒvƒyƒ|";

//        private const string HiraganaHandakuonWide =
//            "‚Ï‚Ò‚Õ‚Ø‚Û";

//        // ------------------------------------------------------------
//        // Ascii
//        // ------------------------------------------------------------

//        // Roman

//        [Fact]
//        public void TestRomanWideToNarrow()
//        {
//            Assert.Equal(RomanNarrow, KanaConverterOld.Convert(RomanWide, KanaOption.RomanToNarrow));
//        }

//        [Fact]
//        public void TestRomanNarrowToWide()
//        {
//            Assert.Equal(RomanWide, KanaConverterOld.Convert(RomanNarrow, KanaOption.RomanToWide));
//        }

//        // Numeric

//        [Fact]
//        public void TestNumericWideToNarrow()
//        {
//            Assert.Equal(NumericNarrow, KanaConverterOld.Convert(NumericWide, KanaOption.NumericToNarrow));
//        }

//        [Fact]
//        public void TestNumericNarrowToWide()
//        {
//            Assert.Equal(NumericWide, KanaConverterOld.Convert(NumericNarrow, KanaOption.NumericToWide));
//        }

//        // Ascii

//        [Fact]
//        public void TestAsciiWideToNarrow()
//        {
//            Assert.Equal(AsciiNarrow, KanaConverterOld.Convert(AsciiWide, KanaOption.AsciiToNarrow));
//        }

//        [Fact]
//        public void TestAsciiNarrowToWide()
//        {
//            Assert.Equal(AsciiWide, KanaConverterOld.Convert(AsciiNarrow, KanaOption.AsciiToWide));
//        }

//        // Space

//        [Fact]
//        public void TestSpaceWideToNarrow()
//        {
//            Assert.Equal(SpaceNarrow, KanaConverterOld.Convert(SpaceWide, KanaOption.SpaceToNarrow));
//        }

//        [Fact]
//        public void TestSpaceNarrowToWide()
//        {
//            Assert.Equal(SpaceWide, KanaConverterOld.Convert(SpaceNarrow, KanaOption.SpaceToWide));
//        }

//        // ------------------------------------------------------------
//        // KanaWide/KanaNarrow
//        // ------------------------------------------------------------

//        [Fact]
//        public void TestKanaWideToKanaNarrow()
//        {
//            Assert.Equal(KanaNarrow, KanaConverterOld.Convert(KanaWide, KanaOption.KanaToNarrow));
//        }

//        [Fact]
//        public void TestKanaDakuonWideToKanaDakuonNarrow()
//        {
//            Assert.Equal(KanaDakuonNarrow, KanaConverterOld.Convert(KanaDakuonWide, KanaOption.KanaToNarrow));
//        }

//        [Fact]
//        public void TestKanaHandakuonWideToKanaHandakuonNarrow()
//        {
//            Assert.Equal(KanaHandakuonNarrow, KanaConverterOld.Convert(KanaHandakuonWide, KanaOption.KanaToNarrow));
//        }

//        [Fact]
//        public void TestKanaNarrowToKanaWide()
//        {
//            Assert.Equal(KanaWide, KanaConverterOld.Convert(KanaNarrow, KanaOption.KanaToWide));
//        }

//        [Fact]
//        public void TestKanaDakuonNarrowToKanaDakuonWide()
//        {
//            Assert.Equal(KanaDakuonWide, KanaConverterOld.Convert(KanaDakuonNarrow, KanaOption.KanaToWide));
//        }

//        [Fact]
//        public void TestKanaHandakuonNarrowToKanaHandakuonWide()
//        {
//            Assert.Equal(KanaHandakuonWide, KanaConverterOld.Convert(KanaHandakuonNarrow, KanaOption.KanaToWide));
//        }

//        // ------------------------------------------------------------
//        // HiraganaWide/KanaNarrow
//        // ------------------------------------------------------------

//        [Fact]
//        public void TestHiraganaWideToKanaNarrow()
//        {
//            Assert.Equal(KanaNarrow, KanaConverterOld.Convert(HiraganaWide, KanaOption.HiraganaToHankana));
//        }

//        [Fact]
//        public void TestHiraganaDakuonWideToKanaDakuonNarrow()
//        {
//            Assert.Equal(KanaDakuonNarrow, KanaConverterOld.Convert(HiraganaDakuonWide, KanaOption.HiraganaToHankana));
//        }

//        [Fact]
//        public void TestHiraganaHandakuonWideToKanaHandakuonNarrow()
//        {
//            Assert.Equal(KanaHandakuonNarrow, KanaConverterOld.Convert(HiraganaHandakuonWide, KanaOption.HiraganaToHankana));
//        }

//        [Fact]
//        public void TestKanaNarrowToHiraganaWide()
//        {
//            Assert.Equal(HiraganaWide, KanaConverterOld.Convert(KanaNarrow, KanaOption.HankanaToHiragana));
//        }

//        [Fact]
//        public void TestKanaDakuonNarrowToHiraganaDakuonWide()
//        {
//            Assert.Equal(HiraganaDakuonWide, KanaConverterOld.Convert(KanaDakuonNarrow, KanaOption.HankanaToHiragana));
//        }

//        [Fact]
//        public void TestKanaHandakuonNarrowToHiraganaHandakuonWide()
//        {
//            Assert.Equal(HiraganaHandakuonWide, KanaConverterOld.Convert(KanaHandakuonNarrow, KanaOption.HankanaToHiragana));
//        }

//        // ------------------------------------------------------------
//        // HiraganaWide/KanaWide
//        // ------------------------------------------------------------

//        [Fact]
//        public void TestKanaWideToHiraganaWide()
//        {
//            Assert.Equal(HiraganaWide, KanaConverterOld.Convert(KanaWide, KanaOption.KatakanaToHiragana));
//        }

//        [Fact]
//        public void TestKanaDakuonWideToHiraganaDakuonWide()
//        {
//            Assert.Equal(HiraganaDakuonWide, KanaConverterOld.Convert(KanaDakuonWide, KanaOption.KatakanaToHiragana));
//        }

//        [Fact]
//        public void TestKanaHandakuonWideToHiraganaHandakuonWide()
//        {
//            Assert.Equal(HiraganaHandakuonWide, KanaConverterOld.Convert(KanaHandakuonWide, KanaOption.KatakanaToHiragana));
//        }

//        [Fact]
//        public void TestHiraganaWideToKanaWide()
//        {
//            Assert.Equal(KanaWide, KanaConverterOld.Convert(HiraganaWide, KanaOption.HiraganaToKatakana));
//        }

//        [Fact]
//        public void TestHiraganaDakuonWideToKanaDakuonWide()
//        {
//            Assert.Equal(KanaDakuonWide, KanaConverterOld.Convert(HiraganaDakuonWide, KanaOption.HiraganaToKatakana));
//        }

//        [Fact]
//        public void TestHiraganaHandakuonWideToKanaHandakuonWide()
//        {
//            Assert.Equal(KanaHandakuonWide, KanaConverterOld.Convert(HiraganaHandakuonWide, KanaOption.HiraganaToKatakana));
//        }
//    }
//}
