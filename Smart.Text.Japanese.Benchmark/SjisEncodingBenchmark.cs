namespace Smart.Text.Japanese.Benchmark;

using System.Text;

using BenchmarkDotNet.Attributes;

[Config(typeof(BenchmarkConfig))]
public class SjisEncodingBenchmark
{
    private const int N = 1000;

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
}
