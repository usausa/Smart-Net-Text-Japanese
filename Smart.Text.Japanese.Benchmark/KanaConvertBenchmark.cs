namespace Smart.Text.Japanese.Benchmark;

using System.Text;

using BenchmarkDotNet.Attributes;

[Config(typeof(BenchmarkConfig))]
public class KanaConvertBenchmark
{
    private const int N = 1000;

    // length 214

    private const string Narrow =
        "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz" +
        "0123456789" +
        "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~" +
        " " +
        "ｧｱｨｲｩｳｪｴｫｵｶｶﾞｷｷﾞｸｸﾞｹｹﾞｺｺﾞｻｻﾞｼｼﾞｽｽﾞｾｾﾞｿｿﾞﾀﾀﾞﾁﾁﾞｯﾂﾂﾞﾃﾃﾞﾄﾄﾞﾅﾆﾇﾈﾉﾊﾊﾞﾊﾟﾋﾋﾞﾋﾟﾌﾌﾞﾌﾟﾍﾍﾞﾍﾟﾎﾎﾞﾎﾟﾏﾐﾑﾒﾓｬﾔｭﾕｮﾖﾗﾘﾙﾚﾛﾜｦﾝｳﾞﾜﾞｦﾞ" +
        "ﾞﾟｰ" +
        "｡｢｣､･";

    private const string Wide =
        "ＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺａｂｃｄｅｆｇｈｉｊｋｌｍｎｏｐｑｒｓｔｕｖｗｘｙｚ" +
        "０１２３４５６７８９" +
        "！”＃＄％＆’（）＊＋，－．／：；＜＝＞？＠［￥］＾＿｀｛｜｝￣" +
        "　" +
        "ァアィイゥウェエォオカガキギクグケゲコゴサザシジスズセゼソゾタダチヂッツヅテデトドナニヌネノハバパヒビピフブプヘベペホボポマミムメモャヤュユョヨラリルレロワヲンヴ\u30F7\u30FA" +
        "゛゜ー" +
        "。「」、・";

    // Half-width kana heavy input (includes voiced/semi-voiced marks for the look-ahead path)

    private const string Hankana =
        "ｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜｦﾝ" +
        "ｶﾞｷﾞｸﾞｹﾞｺﾞｻﾞｼﾞｽﾞｾﾞｿﾞﾀﾞﾁﾞﾂﾞﾃﾞﾄﾞﾊﾞﾋﾞﾌﾞﾍﾞﾎﾞﾊﾟﾋﾟﾌﾟﾍﾟﾎﾟ" +
        "｡｢｣､･ｰ";

    // Non-converted majority input (already narrow ASCII => pass-through under Narrow)

    private const string Ascii =
        "The quick brown fox jumps over the lazy dog. " +
        "Pack my box with five dozen liquor jugs. 0123456789!?";

    private string narrow1 = default!;
    private string wide1 = default!;

    private string narrow10 = default!;
    private string wide10 = default!;

    private string hankana = default!;
    private string ascii = default!;

    private string narrowShort = default!;
    private string narrowLong = default!;

    private char[] outputBuffer = default!;

    [GlobalSetup]
    public void Setup()
    {
        var buffer1 = new StringBuilder(Narrow.Length * 10);
        var buffer2 = new StringBuilder(Wide.Length * 10);
        for (var i = 0; i < 10; i++)
        {
            buffer1.Append(Narrow);
            buffer2.Append(Wide);
        }

        var buffer3 = new StringBuilder(Narrow.Length * 100);
        for (var i = 0; i < 100; i++)
        {
            buffer3.Append(Narrow);
        }

        narrow1 = Narrow;
        wide1 = Wide;
        narrow10 = buffer1.ToString();
        wide10 = buffer2.ToString();

        hankana = Hankana;
        ascii = Ascii;
        narrowShort = Narrow[..4];
        narrowLong = buffer3.ToString();
        outputBuffer = new char[Math.Max(Narrow.Length, Wide.Length) * 2];
    }

    [Benchmark(OperationsPerInvoke = N)]
    public void NarrowToWide1()
    {
        var buffer = narrow1;
        for (var i = 0; i < N; i++)
        {
            KanaConverter.Convert(buffer, KanaOption.Wide);
        }
    }

    [Benchmark(OperationsPerInvoke = N)]
    public void NarrowToWide1Optimized()
    {
        var buffer = narrow1;
        for (var i = 0; i < N; i++)
        {
            KanaConverter.ConvertToWide(buffer);
        }
    }

    [Benchmark(OperationsPerInvoke = N)]
    public void NarrowToWide10()
    {
        var buffer = narrow10;
        for (var i = 0; i < N; i++)
        {
            KanaConverter.Convert(buffer, KanaOption.Wide);
        }
    }

    [Benchmark(OperationsPerInvoke = N)]
    public void WideToNarrow1()
    {
        var buffer = wide1;
        for (var i = 0; i < N; i++)
        {
            KanaConverter.Convert(buffer, KanaOption.Narrow);
        }
    }

    [Benchmark(OperationsPerInvoke = N)]
    public void WideToNarrow1Optimized()
    {
        var buffer = wide1;
        for (var i = 0; i < N; i++)
        {
            KanaConverter.ConvertToNarrow(buffer);
        }
    }

    [Benchmark(OperationsPerInvoke = N)]
    public void WideToNarrow10()
    {
        var buffer = wide10;
        for (var i = 0; i < N; i++)
        {
            KanaConverter.Convert(buffer, KanaOption.Narrow);
        }
    }

    // Half-width kana heavy

    [Benchmark(OperationsPerInvoke = N)]
    public int HankanaToWide()
    {
        var source = hankana;
        var total = 0;
        for (var i = 0; i < N; i++)
        {
            total += KanaConverter.Convert(source, KanaOption.Wide).Length;
        }

        return total;
    }

    // Mostly non-converted input (pass-through throughput)

    [Benchmark(OperationsPerInvoke = N)]
    public int NarrowPassThrough()
    {
        var source = ascii;
        var total = 0;
        for (var i = 0; i < N; i++)
        {
            total += KanaConverter.Convert(source, KanaOption.Narrow).Length;
        }

        return total;
    }

    // string-returning overload vs Span overload (allocation difference)

    [Benchmark(OperationsPerInvoke = N)]
    public int NarrowToWide1Span()
    {
        var source = narrow1;
        var output = outputBuffer;
        var total = 0;
        for (var i = 0; i < N; i++)
        {
            total += KanaConverter.Convert(source, output, KanaOption.Wide);
        }

        return total;
    }

    [Benchmark(OperationsPerInvoke = N)]
    public int WideToNarrow1Span()
    {
        var source = wide1;
        var output = outputBuffer;
        var total = 0;
        for (var i = 0; i < N; i++)
        {
            total += KanaConverter.Convert(source, output, KanaOption.Narrow);
        }

        return total;
    }

    // Extreme sizes (very short vs very long / ArrayPool path)

    [Benchmark(OperationsPerInvoke = N)]
    public int NarrowToWideShort()
    {
        var source = narrowShort;
        var total = 0;
        for (var i = 0; i < N; i++)
        {
            total += KanaConverter.Convert(source, KanaOption.Wide).Length;
        }

        return total;
    }

    [Benchmark(OperationsPerInvoke = N)]
    public int NarrowToWideLong()
    {
        var source = narrowLong;
        var total = 0;
        for (var i = 0; i < N; i++)
        {
            total += KanaConverter.Convert(source, KanaOption.Wide).Length;
        }

        return total;
    }
}
