using BenchmarkDotNet.Attributes;
using System.Linq;

namespace DevBrewLabs.Evalis.Benchmarkv2;

[MemoryDiagnoser]
[RankColumn]
public class FormulaEngineParseBenchmarks
{
    private readonly FormulaEngine _engine = new();

    [Params(50, 500, 5000)]
    public int ArgumentCount;

    private string _expression = string.Empty;

    [GlobalSetup]
    public void Setup() => _expression = $"SUM({string.Join(",", Enumerable.Range(1, ArgumentCount))})";

    [Benchmark(Baseline = true)]
    public object Parse()
        => _engine.Parse(_expression);
}