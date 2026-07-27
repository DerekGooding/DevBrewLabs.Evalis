using NUnit.Framework;

namespace DevBrewLabs.Evalis.Tests;

public class ArithmeticFormulaTests
{
    private IFormulaEngine _formulaEngine;

    [OneTimeSetUp]
    public void Setup()
    {
        _formulaEngine = new FormulaEngine();
    }

    [TestCase("AVERAGE(2,2,2,2)", 2)]
    [TestCase("AVERAGE(0 ,  12,  3,1)", 4)]
    [TestCase("AVERAGE(-1 ,  12,  3,2)", 4)]
    [TestCase("AVERAGE(1.4,1.4)", 1.4)]
    public void AverageFormula_SuccessTest(string input, double output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }

    [TestCase("CEILING(1.23)", 2)]
    [TestCase("CEILING(4)", 4)]
    [TestCase("CEILING(-1.23)", -1)]
    public void CeilingFormula_SuccessTest(string input, double output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }

    [TestCase("FLOOR(1.99)", 1)]
    [TestCase("FLOOR(4)", 4)]
    [TestCase("FLOOR(-1.23)", -2)]
    public void FloorFormula_SuccessTest(string input, double output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }

    [TestCase("AVERAGE(1,2,3 4)")]
    [TestCase("AVERAGE(0- ,  12,  3,1)")]
    [TestCase("AVERAGE,  12,  3,1)")]
    [TestCase("AVERAGE(..1,2.1, 3, 4.2)")]
    public void AverageFormula_FailureTest(string input)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Error, Is.Not.Null);
    }

    [TestCase("SUM(1,2,3,4)", 10)]
    [TestCase("SUM(0 ,  12,  3,1)", 16)]
    [TestCase("SUM(-1 ,  12,  3,1)", 15)]
    [TestCase("SUM(1.1,2.1, 3, 4.2)", 10.4)]
    [TestCase("SUM(1, SUM(1,2,SUM(2,2)), 4)", 12)]
    public void SumFormula_SuccessTest(string input, double output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }

    [TestCase("SUM(1,2,3 4)")]
    [TestCase("SUM(0- ,  12,  3,1)")]
    [TestCase("SUM,  12,  3,1)")]
    [TestCase("SUM(..1,2.1, 3, 4.2)")]
    public void SumFormula_FailureTest(string input)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Error, Is.Not.Null);
    }

    [TestCase("ABS(1.2)", 1.2)]
    [TestCase("ABS(12.2322)", 12.2322)]
    [TestCase("ABS(2)", 2)]
    public void AbsFormula_SuccessTest(string input, double output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }

    [TestCase("ABS(,12.2)", 1.2)]
    [TestCase("ABS(,12.2322)", 12.2322)]
    [TestCase("ABS(A)", 2)]
    public void AbsFormula_FailureTest(string input, double output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Error, Is.Not.Null);
    }

    [TestCase("MIN(10, 5, 20)", 5)]
    [TestCase("MIN(-1, -5)", -5)]
    [TestCase("MIN(0)", 0)]
    public void MinFormula_SuccessTest(string input, double output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }

    [TestCase("MAX(10, 5, 20)", 20)]
    [TestCase("MAX(-1, -5)", -1)]
    [TestCase("MAX(0)", 0)]
    public void MaxFormula_SuccessTest(string input, double output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }

    [TestCase("POWER(2, 3)", 8)]
    [TestCase("POWER(5, 0)", 1)]
    [TestCase("POWER(4, 0.5)", 2)]
    public void PowerFormula_SuccessTest(string input, double output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }

    [TestCase("ROUND(3.14159, 2)", 3.14)]
    [TestCase("ROUND(3.14159)", 3)]
    [TestCase("ROUND(3.5)", 4)]
    public void RoundFormula_SuccessTest(string input, double output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }

    [TestCase("SQRT(16)", 4)]
    [TestCase("SQRT(25)", 5)]
    [TestCase("SQRT(0)", 0)]
    public void SqrtFormula_SuccessTest(string input, double output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }

    [TestCase("MOD(10, 3)", 1)]
    [TestCase("MOD(15, 4)", 3)]
    public void ModTest(string input, double output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }

    [TestCase("TRUNC(3.14)", 3)]
    [TestCase("TRUNC(3.14159, 2)", 3.14)]
    public void TruncTest(string input, double output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(Math.Round((double)result.Value, 2), Is.EqualTo(output));
    }

    [TestCase("SIGN(15)", 1)]
    [TestCase("SIGN(-5)", -1)]
    [TestCase("SIGN(0)", 0)]
    public void SignTest(string input, double output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }

    [TestCase("LOG(100, 10)", 2)]
    [TestCase("LOG(8, 2)", 3)]
    public void LogTest(string input, double output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }

    [TestCase("LOG10(100)", 2)]
    public void Log10Test(string input, double output)
    {
        var result = _formulaEngine.Evaluate(input);
        if (result.Value == null) throw new Exception(result.Error);
        Assert.That(result.Value, Is.EqualTo(output));
    }

    [TestCase("EXP(2)", 7.38905609893065)]
    public void ExpTest(string input, double output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(Math.Round((double)result.Value, 5), Is.EqualTo(Math.Round(output, 5)));
    }

    [TestCase("PI()", Math.PI)]
    public void PiTest(string input, double output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }
}