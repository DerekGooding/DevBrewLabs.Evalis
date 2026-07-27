using BenchmarkDotNet.Attributes;
using NCalc;
using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace DevBrewLabs.Evalis.Benchmarkv2;

[MemoryDiagnoser]
[RankColumn]
public class NCalcBenchmarks
{
    private readonly Random _random = Random.Shared;

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
            0d);

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
        => CreateExpression(_sumIntExpression.Value).Evaluate();

    [Benchmark]
    public object SumDoubles()
        => CreateExpression(_sumDoubleExpression.Value).Evaluate();

    [Benchmark]
    public object AverageIntegers()
        => CreateExpression(_averageIntExpression.Value).Evaluate();

    [Benchmark]
    public object AverageDoubles()
        => CreateExpression(_averageDoubleExpression.Value).Evaluate();

    [Benchmark]
    public object Upper()
        => CreateExpression(_upperExpression.Value).Evaluate();

    [Benchmark]
    public object Lower()
        => CreateExpression(_lowerExpression.Value).Evaluate();


    private static Expression CreateExpression(string expression)
    {
        var e = new Expression(expression);
        NCalc.Handlers.FunctionData thing = null;

        e.Functions["SUM"] = args =>
        {
            double total = 0;

            for (var i = 0; i < args.Count; i++)
            {
                total += Convert.ToDouble(
                    args.Evaluate(i),
                    CultureInfo.InvariantCulture);
            }

            return total;
        };

        e.Functions["AVERAGE"] = args =>
        {
            double total = 0;

            for (var i = 0; i < args.Count; i++)
            {
                total += Convert.ToDouble(
                    args.Evaluate(i),
                    CultureInfo.InvariantCulture);
            }

            return total / args.Count;
        };

        e.Functions["UPPER"] = args => args.Evaluate(0)!
                .ToString()!
                .ToUpperInvariant();

        e.Functions["LOWER"] = args => args.Evaluate(0)!
                .ToString()!
                .ToLowerInvariant();

        return e;
    }

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

    private string BuildStringExpression(string function, int length, bool lowerCase) => $"{function}(\"{RandomString(length, lowerCase)}\")";

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