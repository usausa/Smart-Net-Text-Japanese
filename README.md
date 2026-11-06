# Smart.Text.Japanese .NET - japanese text library for .NET

[![NuGet](https://img.shields.io/nuget/v/Usa.Smart.Text.Japanese.svg)](https://www.nuget.org/packages/Usa.Smart.Text.Japanese)

## What is this?

Japanese full-width and half-width converter.

### Roman

```csharp
private const string RomanNarrow =
    "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

private const string RomanWide =
    "ＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺａｂｃｄｅｆｇｈｉｊｋｌｍｎｏｐｑｒｓｔｕｖｗｘｙｚ";

Assert.Equal(RomanNarrow, KanaConverter.Convert(RomanWide, KanaOption.RomanToNarrow));
Assert.Equal(RomanWide, KanaConverter.Convert(RomanNarrow, KanaOption.RomanToWide));

```

### Numeric

```csharp
private const string NumericNarrow =
    "0123456789";

private const string NumericWide =
    "０１２３４５６７８９";

Assert.Equal(NumericNarrow, KanaConverter.Convert(NumericWide, KanaOption.NumericToNarrow));
Assert.Equal(NumericWide, KanaConverter.Convert(NumericNarrow, KanaOption.NumericToWide));

```

### ASCII

```csharp
private const string AsciiNarrow =
    "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";

private const string AsciiWide =
    "！”＃＄％＆’（）＊＋，－．／：；＜＝＞？＠［￥］＾＿｀｛｜｝～";

Assert.Equal(AsciiNarrow, KanaConverter.Convert(AsciiWide, KanaOption.AsciiToNarrow));
Assert.Equal(AsciiWide, KanaConverter.Convert(AsciiNarrow, KanaOption.AsciiToWide));

// To narrow also takes the other full-width forms of the same characters
Assert.Equal("\"'\\~", KanaConverter.Convert("＂＇＼～", KanaOption.AsciiToNarrow));
Assert.Equal("\"\"''", KanaConverter.Convert("“”‘’", KanaOption.AsciiToNarrow));
Assert.Equal("~-", KanaConverter.Convert("〜−", KanaOption.AsciiToNarrow));
Assert.Equal("~", KanaConverter.Convert("￣", KanaOption.AsciiToNarrow));

// To wide also takes the half-width yen sign
Assert.Equal("￥", KanaConverter.Convert("¥", KanaOption.AsciiToWide));

```

### Space

```csharp
private const string SpaceNarrow =
    " ";

private const string SpaceWide =
    "　";

Assert.Equal(SpaceNarrow, KanaConverter.Convert(SpaceWide, KanaOption.SpaceToNarrow));
Assert.Equal(SpaceWide, KanaConverter.Convert(SpaceNarrow, KanaOption.SpaceToWide));

```

### Kana

```csharp
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

// Katakana/Hankana
Assert.Equal(Hankana, KanaConverter.Convert(Katakana, KanaOption.KatakanaToHankana));
Assert.Equal(Katakana, KanaConverter.Convert(Hankana, KanaOption.HankanaToKatakana));

// Hiragana/Hankana
Assert.Equal(Hankana, KanaConverter.Convert(Hiragana, KanaOption.HiraganaToHankana));
Assert.Equal(Hiragana, KanaConverter.Convert(Hankana, KanaOption.HankanaToHiragana));

// Hiragana/Katakana
Assert.Equal(Hiragana, KanaConverter.Convert(Katakana, KanaOption.KatakanaToHiragana));
Assert.Equal(HiraganaOdoriji, KanaConverter.Convert(KatakanaOdoriji, KanaOption.KatakanaToHiragana));
Assert.Equal(Katakana, KanaConverter.Convert(Hiragana, KanaOption.HiraganaToKatakana));
Assert.Equal(KatakanaOdoriji, KanaConverter.Convert(HiraganaOdoriji, KanaOption.HiraganaToKatakana));

// To half-width also takes combining marks
Assert.Equal("ｶﾞ", KanaConverter.Convert("カ\u3099", KanaOption.KatakanaToHankana));

```

### All

```csharp
private const string Narrow = RomanNarrow + NumericNarrow + AsciiNarrow + SpaceNarrow + Hankana;

private const string Wide = RomanWide + NumericWide + AsciiWide + SpaceWide + Katakana;

Assert.Equal(Narrow, KanaConverter.Convert(Wide, KanaOption.Narrow));
Assert.Equal(Wide, KanaConverter.Convert(Narrow, KanaOption.Wide));
```

### WidthFold

ASCII to half-width and kana to full-width, the direction that evens out text.

```csharp
Assert.Equal("A1! ガ", KanaConverter.Convert("Ａ１！　ｶﾞ", KanaOption.WidthFold));
```

### NFKC

`KanaOption.Nfkc` normalizes to NFKC before the other conversions. The result is the same as `KanaConverter.Convert(text.Normalize(NormalizationForm.FormKC), option)`, and it is faster on Japanese text: common characters are handled in one pass, and only text with other characters goes to `Normalize`.

```csharp
Assert.Equal("(株)A1 ガ", KanaConverter.Convert("㈱Ａ１　ｶﾞ", KanaOption.Nfkc));
Assert.Equal("(株)A1 ガ", KanaConverter.Convert("㈱Ａ１　ｶﾞ", KanaOption.Nfkc | KanaOption.WidthFold));
```

- `Normalize` does nothing with `InvariantGlobalization`, so text that goes to it stays as it is before the other conversions.
- NFKC can make text longer, so `Convert` with a buffer needs room for the result.
- To use `Normalize` yourself, call it first. Converting first adds a space before a dakuten that does not compose: `゛` becomes a space and U+3099 in NFKC.

### Combining options

Each character is converted once, so options do not chain. Use `HankanaToHiragana` together with `KatakanaToHiragana` to get hiragana from both kinds of katakana.

```csharp
Assert.Equal("ア", KanaConverter.Convert("ｱ", KanaOption.HankanaToKatakana | KanaOption.KatakanaToHiragana));
Assert.Equal("あ", KanaConverter.Convert("ｱ", KanaOption.HankanaToHiragana | KanaOption.KatakanaToHiragana));
```
