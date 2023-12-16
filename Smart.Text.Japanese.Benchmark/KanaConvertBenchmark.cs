namespace Smart.Text.Japanese.Benchmark;

using System.Text;

using BenchmarkDotNet.Attributes;

[Config(typeof(BenchmarkConfig))]
public sealed class KanaConvertBenchmark
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

    private string narrow1 = default!;
    private string wide1 = default!;

    private string narrow10 = default!;
    private string wide10 = default!;

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

        narrow1 = Narrow;
        wide1 = Wide;
        narrow10 = buffer1.ToString();
        wide10 = buffer2.ToString();
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
}
