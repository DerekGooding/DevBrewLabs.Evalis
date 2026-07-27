using NUnit.Framework;

namespace DevBrewLabs.Evalis.Tests;

public class SequenceExpressionTests
{
    private IFormulaEngine _formulaEngine;

    [OneTimeSetUp]
    public void Setup()
    {
        _formulaEngine = new FormulaEngine();
    }

    [TestCase]
    public void SuccessTest()
    {
        var expr = SequencedExpressionBuilder
          .Create("Result1", "SUM(1,2,12)")
          .Next("Result2", "AVERAGE(1,2,$Result1)")
          .Next("Result3", "SUM(1,2,$Result2)");

        var result = _formulaEngine.Evaluate(expr);
        Assert.That(result.Value, Is.EqualTo(9));
    }

    [TestCase]
    public async Task SuccessAsyncTest()
    {
        var expr = SequencedExpressionBuilder
          .Create("Result1", "SUM(1,2,12)")
          .Next("Result2", "AVERAGE(1,2,$Result1)")
          .Next("Result3", "SUM(1,2,$Result2)");

        var result = await _formulaEngine.EvaluateAsync(expr);
        Assert.That(result.Value, Is.EqualTo(9));
    }

    [TestCase]
    public void FailureTest()
    {
        var expr = SequencedExpressionBuilder
                 .Create("Result1", "SUM(1,2,12)")
                 .Next("Result2", "AVERAGE(1,2,$xyz)")
                 .Next("Result3", "SUM(1,2,$Result2)");

        var result = _formulaEngine.Evaluate(expr);
        Assert.That(result.Error, Is.Not.Null);
    }
}