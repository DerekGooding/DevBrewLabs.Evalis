using BenchmarkDotNet.Attributes;
using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace DevBrewLabs.Evalis.Benchmarkv2;

[MemoryDiagnoser]
[RankColumn]
public class FormulaEngineBenchmarks
{
    private readonly Random _random = Random.Shared;
    private readonly IFormulaEngine _engine = new FormulaEngine();

    [Params(50, 500, 5000)]
    public int ArgumentCount;

    private FormulaExpression _sumIntExpression;
    private FormulaExpression _sumDoubleExpression;
    private FormulaExpression _averageIntExpression;
    private FormulaExpression _averageDoubleExpression;
    private FormulaExpression _upperExpression;
    private FormulaExpression _lowerExpression;

    [GlobalSetup]
    public void Setup()
    {
        _sumIntExpression = new(
            BuildNumericExpression("SUM", ArgumentCount, false),
            ArgumentCount,
            0);

        _sumDoubleExpression = new(
            BuildNumericExpression("SUM", ArgumentCount, true),
            ArgumentCount,
            0d);

        _averageIntExpression = new(
            BuildNumericExpression("AVERAGE", ArgumentCount, false),
            ArgumentCount,
            0d);

        _averageDoubleExpression = new(
            BuildNumericExpression("AVERAGE", ArgumentCount, true),
            ArgumentCount,
            0d);

        _upperExpression = new(
            BuildStringExpression("UPPER", ArgumentCount, true),
            ArgumentCount,
            string.Empty);

        _lowerExpression = new(
            BuildStringExpression("LOWER", ArgumentCount, false),
            ArgumentCount,
            string.Empty);
    }

    [Benchmark(Baseline = true)]
    public object SumIntegers()
        => _engine.Evaluate(_sumIntExpression.Value).Value;

    [Benchmark]
    public object SumDoubles()
        => _engine.Evaluate(_sumDoubleExpression.Value).Value;

    [Benchmark]
    public object AverageIntegers()
        => _engine.Evaluate(_averageIntExpression.Value).Value;

    [Benchmark]
    public object AverageDoubles()
        => _engine.Evaluate(_averageDoubleExpression.Value).Value;

    [Benchmark]
    public object Upper()
        => _engine.Evaluate(_upperExpression.Value).Value;

    [Benchmark]
    public object Lower()
        => _engine.Evaluate(_lowerExpression.Value).Value;

    private string BuildNumericExpression(string function, int argumentCount, bool useDoubles)
    {
        var values = useDoubles
            ? Enumerable.Range(1, argumentCount)
                .Select(_ => Math.Round(_random.Next(1, 10000) * _random.NextDouble(), 2)
                    .ToString(CultureInfo.InvariantCulture))
            : Enumerable.Range(1, argumentCount)
                .Select(_ => _random.Next(1, 10000)
                    .ToString(CultureInfo.InvariantCulture));

        return $"{function}({string.Join(",", values)})";
    }

    private string BuildStringExpression(string function, int length, bool lowerCase)
    {
        return $"{function}(\"{RandomString(length, lowerCase)}\")";
    }

    private string RandomString(int size, bool lowerCase)
    {
        var builder = new StringBuilder(size);

        var offset = lowerCase ? 'a' : 'A';

        for (var i = 0; i < size; i++)
        {
            builder.Append((char)_random.Next(offset, offset + 26));
        }

        return builder.ToString();
    }
}