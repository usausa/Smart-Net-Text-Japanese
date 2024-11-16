# Smart.Text.Japanese .NET - japanese text library for .NET

[![NuGet](https://img.shields.io/nuget/v/Usa.Smart.Text.Japanese.svg)](https://www.nuget.org/packages/Usa.Smart.Text.Japanese)

## What is this?

`mb_convert_kana` like converter.

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
    "！”＃＄％＆’（）＊＋，－．／：；＜＝＞？＠［￥］＾＿｀｛｜｝￣";

Assert.Equal(AsciiNarrow, KanaConverter.Convert(AsciiWide, KanaOption.AsciiToNarrow));
Assert.Equal(AsciiWide, KanaConverter.Convert(AsciiNarrow, KanaOption.AsciiToWide));

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

```

### All

```csharp
private const string Narrow = RomanNarrow + NumericNarrow + AsciiNarrow + SpaceNarrow + Hankana;

private const string Wide = RomanWide + NumericWide + AsciiWide + SpaceWide + Katakana;

Assert.Equal(Narrow, KanaConverter.Convert(Wide, KanaOption.Narrow));
Assert.Equal(Wide, KanaConverter.Convert(Narrow, KanaOption.Wide));
```
