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
            "�`�a�b�c�d�e�f�g�h�i�j�k�l�m�n�o�p�q�r�s�t�u�v�w�x�y����������������������������������������������������";

        // Numeric

        private const string NumericNarrow =
            "0123456789";

        private const string NumericWide =
            "�O�P�Q�R�S�T�U�V�W�X";

        // ASCII

        private const string AsciiNarrow =
            "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";

        private const string AsciiWide =
            "�I�h���������f�i�j���{�C�|�D�^�F�G�������H���m���n�O�Q�M�o�b�p�P";

        // Space

        private const string SpaceNarrow =
            " ";

        private const string SpaceWide =
            "�@";

        // Kana

        private const string Hankana =
            "������������޷�޸�޹�޺�޻�޼�޽�޾�޿�������ޯ�������������������������������������������Ӭԭծ������ܦݳ��ަ�" +
            "�߰" +
            "�����";

        private const string Katakana =
            "�@�A�B�C�D�E�F�G�H�I�J�K�L�M�N�O�P�Q�R�S�T�U�V�W�X�Y�Z�[�\�]�^�_�`�a�b�c�d�e�f�g�h�i�j�k�l�m�n�o�p�q�r�s�t�u�v�w�x�y�z�{�|�}�~������������������������������������\u30F7\u30FA" +
            "�J�K�[" +
            "�B�u�v�A�E";

        private const string Hiragana =
            "�����������������������������������������������������������������������ÂĂłƂǂȂɂʂ˂̂͂΂ςЂт҂ӂԂՂւׂ؂قڂۂ܂݂ނ߂�����������������\u3094\u30F7\u30FA" +
            "�J�K�[" +
            "�B�u�v�A�E";

        private const string KatakanaOdoriji =
            "�R�S";

        private const string HiraganaOdoriji =
            "�T�U";

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
