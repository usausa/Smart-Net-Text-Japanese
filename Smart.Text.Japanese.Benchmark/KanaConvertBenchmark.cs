namespace Smart.Text.Japanese.Benchmark
{
    using System.Diagnostics.CodeAnalysis;
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

        [AllowNull]
        private string narrow1;
        [AllowNull]
        private string wide1;

        [AllowNull]
        private string narrow10;
        [AllowNull]
        private string wide10;

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
            for (var i = 0; i < N; i++)
            {
                KanaConverter.Convert(narrow1, KanaOption.Wide);
            }
        }

        [Benchmark(OperationsPerInvoke = N)]
        public void NarrowToWide10()
        {
            for (var i = 0; i < N; i++)
            {
                KanaConverter.Convert(narrow10, KanaOption.Wide);
            }
        }

        [Benchmark(OperationsPerInvoke = N)]
        public void WideToNarrow1()
        {
            for (var i = 0; i < N; i++)
            {
                KanaConverter.Convert(wide1, KanaOption.Narrow);
            }
        }

        [Benchmark(OperationsPerInvoke = N)]
        public void WideToNarrow10()
        {
            for (var i = 0; i < N; i++)
            {
                KanaConverter.Convert(wide10, KanaOption.Narrow);
            }
        }
    }
}
