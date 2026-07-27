using BenchmarkDotNet.Running;

namespace DevBrewLabs.Evalis.Benchmarkv2;

internal static class Program
{
    static void Main()
    {
        BenchmarkRunner.Run<FormulaEngineBenchmarks>();
        BenchmarkRunner.Run<NCalcBenchmarks>();
    }
}
