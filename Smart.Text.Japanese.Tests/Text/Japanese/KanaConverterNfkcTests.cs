namespace Smart.Text.Japanese;

using System.Text;

public sealed class KanaConverterNfkcTests
{
    private static readonly KanaOption[] Options =
    [
        default,
        KanaOption.RomanToNarrow | KanaOption.NumericToNarrow | KanaOption.AsciiToNarrow | KanaOption.SpaceToNarrow,
        KanaOption.WidthFold,
        KanaOption.WidthFold | KanaOption.HiraganaToKatakana,
        KanaOption.KatakanaToHiragana,
        KanaOption.Narrow,
        KanaOption.Wide
    ];

    private static readonly char[] Marks = ['ﾞ', 'ﾟ', '\u3099', '\u309A', '\u0301', '\u0323', '゛', '゜', '\u1161', '\u11A8'];

    // Japanese business text, and characters that take the other path
    private const string Alphabet =
        "ABCabc012 -/.,:()\"'\\~あいうかがきはばぱわをんゝゞアイウカガキハバパワヲンヴヷヽヾー亜東京都千代田区髙﨑神ＡＢＣａｂｃ０１２　－／．，：（）＂＇＼～！？＠" +
        "ｱｲｳｶｷﾊﾜｦｯｰﾞﾟ｡｢｣､･。「」、・①②Ⅳ㈱㌔㎏℃№…‥”’“‘￥￣￠※〒〜−‐―゛゜\u3099\u309A\u0301éÉ¥¯¢한ᄒ";

    private static string[] EveryCharacter() =>
    [
        .. Enumerable.Range(0, 0x10000)
            .Where(static x => x is < 0xD800 or > 0xDFFF)
            .Select(static x => ((char)x).ToString())
            .Where(IsValid)
    ];

    // Unicode noncharacters make Normalize throw
    private static bool IsValid(string text)
    {
        try
        {
            _ = text.Normalize(NormalizationForm.FormKC);
            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }

    private static string TwoSteps(string text, KanaOption option)
    {
        var normalized = text.Normalize(NormalizationForm.FormKC);
        return option == default ? normalized : KanaConverter.Convert(normalized, option);
    }

    private static void AssertSameAsTwoSteps(IEnumerable<string> texts)
    {
        foreach (var text in texts)
        {
            foreach (var option in Options)
            {
                Assert.Equal(TwoSteps(text, option), KanaConverter.Convert(text, option | KanaOption.Nfkc));
            }
        }
    }

    // A fixed sequence, so that a failure can be reproduced
    private static IEnumerable<string> RandomTexts(int count, int minimumLength, int maximumLength)
    {
        var value = 20261106u;
        for (var i = 0; i < count; i++)
        {
            value = (value * 1103515245u) + 12345u;
            var length = minimumLength + (int)((value >> 16) % (uint)(maximumLength - minimumLength + 1));
            var builder = new StringBuilder(length);
            for (var j = 0; j < length; j++)
            {
                value = (value * 1103515245u) + 12345u;
                builder.Append(Alphabet[(int)((value >> 16) % (uint)Alphabet.Length)]);
            }

            yield return builder.ToString();
        }
    }

    [Fact]
    public void TestEveryCharacter()
    {
        AssertSameAsTwoSteps(EveryCharacter());
    }

    [Fact]
    public void TestEveryCharacterWithMark()
    {
        foreach (var character in EveryCharacter())
        {
            foreach (var mark in Marks)
            {
                var text = character + mark;
                Assert.Equal(TwoSteps(text, default), KanaConverter.Convert(text, KanaOption.Nfkc));
            }
        }
    }

    [Fact]
    public void TestKanaWithMark()
    {
        var kana = Enumerable.Range(0x3041, 0x56)
            .Concat(Enumerable.Range(0x309D, 2))
            .Concat(Enumerable.Range(0x30A1, 0x5E))
            .Concat(Enumerable.Range(0xFF66, 0x38));

        AssertSameAsTwoSteps(kana.SelectMany(static c => Marks.Select(m => $"{(char)c}{m}")));
    }

    [Fact]
    public void TestRandomText()
    {
        AssertSameAsTwoSteps(RandomTexts(20000, 1, 12));
    }

    [Fact]
    public void TestLongText()
    {
        AssertSameAsTwoSteps(RandomTexts(20, 2000, 3000));
        AssertSameAsTwoSteps(Enumerable.Range(2040, 20).Select(static x => new string('東', x) + "ＡＢｶﾞ"));
    }

    [Fact]
    public void TestBuffer()
    {
        var buffer = new char[64];
        foreach (var text in RandomTexts(2000, 1, 12))
        {
            foreach (var option in Options)
            {
                var length = KanaConverter.Convert(text, buffer, option | KanaOption.Nfkc);
                Assert.Equal(TwoSteps(text, option), new string(buffer, 0, length));
            }
        }
    }

    [Fact]
    public void TestExamples()
    {
        Assert.Equal("東京都千代田区丸の内1-2-3 ABCビル", KanaConverter.Convert("東京都千代田区丸の内１－２－３　ＡＢＣビル", KanaOption.Nfkc));
        Assert.Equal("トウキョウトチヨダク", KanaConverter.Convert("ﾄｳｷｮｳﾄﾁﾖﾀﾞｸ", KanaOption.Nfkc));
        Assert.Equal("(株)ABC1キロ", KanaConverter.Convert("㈱ＡＢＣ①㌔", KanaOption.Nfkc));
        Assert.Equal("ガ", KanaConverter.Convert("カﾞ", KanaOption.Nfkc));
        Assert.Equal("¥1,000", KanaConverter.Convert("￥１，０００", KanaOption.Nfkc | KanaOption.WidthFold));
        Assert.Equal("￥１，０００", KanaConverter.Convert("￥１，０００", KanaOption.Nfkc | KanaOption.Wide));
        Assert.Equal("ｱﾞ", KanaConverter.Convert("ｱﾞ", KanaOption.Nfkc | KanaOption.Narrow));
        Assert.Equal(string.Empty, KanaConverter.Convert(string.Empty, KanaOption.Nfkc));
    }

    [Fact]
    public void TestInvalidSurrogate()
    {
        Assert.Throws<ArgumentException>(static () => KanaConverter.Convert("\uD83Da", KanaOption.Nfkc));
    }
}
