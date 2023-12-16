namespace Smart.Text.Japanese;

public sealed class KanaConverterTest
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
