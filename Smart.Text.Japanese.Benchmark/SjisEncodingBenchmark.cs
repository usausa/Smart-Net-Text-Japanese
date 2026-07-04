namespace Smart.Text.Japanese.Benchmark;

using System.Text;

using BenchmarkDotNet.Attributes;

[Config(typeof(BenchmarkConfig))]
public class SjisEncodingBenchmark
{
    private const int N = 1000;

    // Mixed 1-byte / 2-byte input for limit and split
    private const string Text =
        "ｱAあｲBいｳCうｴDえ0123ｶﾞｷﾞ｡｢｣あいうｦﾝｰ";

    private readonly byte[] buffer = new byte[8];

    public SjisEncodingBenchmark()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }

    [Benchmark(OperationsPerInvoke = N)]
    public void PadLeft()
    {
        for (var i = 0; i < N; i++)
        {
            SjisEncoding.GetFixedBytes("あA", 8, FixedAlignment.Left);
        }
    }

    [Benchmark(OperationsPerInvoke = N)]
    public void PadRight()
    {
        for (var i = 0; i < N; i++)
        {
            SjisEncoding.GetFixedBytes("あA", 8, FixedAlignment.Right);
        }
    }

    [Benchmark(OperationsPerInvoke = N)]
    public void PaddingCenter()
    {
        for (var i = 0; i < N; i++)
        {
            SjisEncoding.GetFixedBytes("あA", 8, FixedAlignment.Center);
        }
    }

    // Half-width kana input

    [Benchmark(OperationsPerInvoke = N)]
    public int PadLeftHankana()
    {
        var total = 0;
        for (var i = 0; i < N; i++)
        {
            total += SjisEncoding.GetFixedBytes("ｱｲｳ", 8, FixedAlignment.Left).Length;
        }

        return total;
    }

    // byte[]-returning overload vs Span<byte> output overload (allocation difference)

    [Benchmark(OperationsPerInvoke = N)]
    public int PadLeftSpan()
    {
        var output = buffer;
        var total = 0;
        for (var i = 0; i < N; i++)
        {
            SjisEncoding.GetFixedBytes("あA", output, FixedAlignment.Left);
            total += output[0];
        }

        return total;
    }

    // Limit / split family

    [Benchmark(OperationsPerInvoke = N)]
    public int LimitString()
    {
        var total = 0;
        for (var i = 0; i < N; i++)
        {
            total += SjisEncoding.GetLimitString(Text, 16).Length;
        }

        return total;
    }

    [Benchmark(OperationsPerInvoke = N)]
    public int LimitStringSpan()
    {
        var total = 0;
        for (var i = 0; i < N; i++)
        {
            total += SjisEncoding.GetLimitString(Text.AsSpan(), 16).Length;
        }

        return total;
    }

    [Benchmark(OperationsPerInvoke = N)]
    public int SplitLimit()
    {
        var total = 0;
        for (var i = 0; i < N; i++)
        {
            foreach (var segment in SjisEncoding.SplitLimitString(Text, 8))
            {
                total += segment.Length;
            }
        }

        return total;
    }
}
