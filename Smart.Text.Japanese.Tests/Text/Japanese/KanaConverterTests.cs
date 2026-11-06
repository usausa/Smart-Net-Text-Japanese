namespace Smart.Text.Japanese;

public sealed class KanaConverterTests
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
        "！”＃＄％＆’（）＊＋，－．／：；＜＝＞？＠［￥］＾＿｀｛｜｝～";

    // Space

    private const string SpaceNarrow =
        " ";

    private const string SpaceWide =
        "　";

    // Kana

    private const string Hankana =
        "ｧｱｨｲｩｳｪｴｫｵｶｶﾞｷｷﾞｸｸﾞｹｹﾞｺｺﾞｻｻﾞｼｼﾞｽｽﾞｾｾﾞｿｿﾞﾀﾀﾞﾁﾁﾞｯﾂﾂﾞﾃﾃﾞﾄﾄﾞﾅﾆﾇﾈﾉﾊﾊﾞﾊﾟﾋﾋﾞﾋﾟﾌﾌﾞﾌﾟﾍﾍﾞﾍﾟﾎﾎﾞﾎﾟﾏﾐﾑﾒﾓｬﾔｭﾕｮﾖﾗﾘﾙﾚﾛﾜｦﾝｳﾞﾜﾞｦﾞ" +
        "ﾞﾟｰ" +
        "｡｢｣､･";

    private const string Katakana =
        "ァアィイゥウェエォオカガキギクグケゲコゴサザシジスズセゼソゾタダチヂッツヅテデトドナニヌネノハバパヒビピフブプヘベペホボポマミムメモャヤュユョヨラリルレロワヲンヴ\u30F7\u30FA" +
        "゛゜ー" +
        "。「」、・";

    private const string Hiragana =
        "ぁあぃいぅうぇえぉおかがきぎくぐけげこごさざしじすずせぜそぞただちぢっつづてでとどなにぬねのはばぱひびぴふぶぷへべぺほぼぽまみむめもゃやゅゆょよらりるれろわをん\u3094\u30F7\u30FA" +
        "゛゜ー" +
        "。「」、・";

    private const string KatakanaOdoriji =
        "ヽヾ";

    private const string HiraganaOdoriji =
        "ゝゞ";

    // All

    private const string Narrow = RomanNarrow + NumericNarrow + AsciiNarrow + SpaceNarrow + Hankana;

    private const string Wide = RomanWide + NumericWide + AsciiWide + SpaceWide + Katakana;

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

    // Characters without a wide form of their own

    [Fact]
    public void TestEveryFullwidthFormToNarrow()
    {
        for (var c = '！'; c <= '～'; c++)
        {
            Assert.Equal(((char)(c - 0xFEE0)).ToString(), KanaConverter.Convert(c.ToString(), KanaOption.Narrow));
        }
    }

    [Fact]
    public void TestFullwidthSymbolsToNarrow()
    {
        Assert.Equal("\"'\\~", KanaConverter.Convert("＂＇＼～", KanaOption.AsciiToNarrow));
        Assert.Equal("\"'\\~", KanaConverter.ConvertToNarrow("＂＇＼～"));
    }

    [Fact]
    public void TestQuotesToNarrow()
    {
        Assert.Equal("\"\"''", KanaConverter.Convert("“”‘’", KanaOption.AsciiToNarrow));
        Assert.Equal("\"\"''", KanaConverter.ConvertToNarrow("“”‘’"));
    }

    [Fact]
    public void TestOtherFormsOfJisCharactersToNarrow()
    {
        Assert.Equal("~-", KanaConverter.Convert("〜−", KanaOption.AsciiToNarrow));
        Assert.Equal("~-", KanaConverter.ConvertToNarrow("〜−"));
    }

    [Fact]
    public void TestYenAndOverlineToNarrow()
    {
        Assert.Equal("\\~", KanaConverter.Convert("￥￣", KanaOption.AsciiToNarrow));
    }

    [Fact]
    public void TestAsciiToWideKeepsJisCharacters()
    {
        Assert.Equal("”’￥～", KanaConverter.Convert("\"'\\~", KanaOption.AsciiToWide));
    }

    [Fact]
    public void TestYenToWide()
    {
        Assert.Equal("￥", KanaConverter.Convert("¥", KanaOption.AsciiToWide));
        Assert.Equal("￥", KanaConverter.ConvertToWide("¥"));
        Assert.Equal("¥", KanaConverter.Convert("¥", KanaOption.AsciiToNarrow));
    }

    [Fact]
    public void TestCombiningMarksToNarrow()
    {
        Assert.Equal("ｶﾞﾊﾟｱﾞ", KanaConverter.Convert("カ\u3099ハ\u309Aア\u3099", KanaOption.KatakanaToHankana));
        Assert.Equal("ｶﾞﾊﾟ", KanaConverter.Convert("か\u3099は\u309A", KanaOption.HiraganaToHankana));
        Assert.Equal("ｶﾞﾊﾟ", KanaConverter.ConvertToNarrow("カ\u3099ハ\u309A"));
    }

    // WidthFold

    [Fact]
    public void TestWidthFold()
    {
        Assert.Equal("A1! ガ", KanaConverter.Convert("Ａ１！　ｶﾞ", KanaOption.WidthFold));
        Assert.Equal("東京都あいうアイウ", KanaConverter.Convert("東京都あいうｱｲｳ", KanaOption.WidthFold));
    }

    // Characters no conversion touches

    [Fact]
    public void TestUntouchedCharacters()
    {
        const string text = "ABC abc 123 東京都 あいう アイウ";

        Assert.Equal(text, KanaConverter.Convert(text, default));
        Assert.Equal("ＡＢＣ ａｂｃ １２３ 東京都 あいう アイウ", KanaConverter.Convert(text, KanaOption.RomanToWide | KanaOption.NumericToWide));
        Assert.Equal("ABC abc 123 東京都 アイウ アイウ", KanaConverter.Convert(text, KanaOption.HiraganaToKatakana));
        Assert.Equal("ABC abc 123 東京都 あいう あいう", KanaConverter.Convert(text, KanaOption.KatakanaToHiragana));
        Assert.Equal("ABC abc 123 東京都 あいう ｱｲｳ", KanaConverter.Convert(text, KanaOption.KatakanaToHankana));
        Assert.Equal("ABC abc 123 東京都 ｱｲｳ アイウ", KanaConverter.Convert(text, KanaOption.HiraganaToHankana));
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
    // HiraganaWide/KatakanaWide
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

    // ------------------------------------------------------------
    // All
    // ------------------------------------------------------------

    [Fact]
    public void TestToNarrow()
    {
        Assert.Equal(Narrow, KanaConverter.Convert(Wide, KanaOption.Narrow));
        Assert.Equal(Narrow, KanaConverter.ConvertToNarrow(Wide));
    }

    [Fact]
    public void TestToWide()
    {
        Assert.Equal(Wide, KanaConverter.Convert(Narrow, KanaOption.Wide));
        Assert.Equal(Wide, KanaConverter.ConvertToWide(Narrow));
    }
}
