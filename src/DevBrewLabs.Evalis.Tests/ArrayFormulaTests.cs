using NUnit.Framework;

namespace DevBrewLabs.Evalis.Tests;

public class ArrayFormulaTests
{
    private IFormulaEngine _formulaEngine;

    [OneTimeSetUp]
    public void Setup() => _formulaEngine = new FormulaEngine();

    [TestCase("ARRAYCONTAINS(ARRAY(1,2,3,4), 4)", true)]
    [TestCase("ARRAYCONTAINS(ARRAY(1,4), 2)", false)]
    [TestCase("ARRAYCONTAINS(ARRAY(1,2,\"test\",4), \"test\")", true)]
    public void ArrayContainsFormula_SuccessTest(string input, bool output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }

    [TestCase("ARRAYINCLUDES(ARRAY(1,2,3,4), ARRAY(1,2))", true)]
    [TestCase("ARRAYINCLUDES(ARRAY(1,2), ARRAY(1,2,3,4))", false)]
    [TestCase("ARRAYINCLUDES(ARRAY(1,4), ARRAY(5))", false)]
    [TestCase("ARRAYINCLUDES(ARRAY(1,2,\"test\",4), ARRAY(\"test\", 2))", true)]
    public void ArrayIncludesFormula_SuccessTest(string input, bool output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }

    [TestCase("INDEX(ARRAY(1, 2, 3), 1)", 2)]
    [TestCase("INDEX(ARRAY(\"A\", \"B\", \"C\"), 2)", "C")]
    public void IndexFormula_SuccessTest(string input, object output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }

    [TestCase("JOIN(\", \", ARRAY(1, 2, 3))", "1, 2, 3")]
    [TestCase("JOIN(\"-\", ARRAY(\"A\", \"B\"))", "A-B")]
    public void JoinFormula_SuccessTest(string input, string output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }

    [TestCase("COUNT(ARRAY(1, 2, 3))", 3)]
    [TestCase("COUNT(ARRAY(\"A\"))", 1)]
    public void CountFormula_SuccessTest(string input, double output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }
}