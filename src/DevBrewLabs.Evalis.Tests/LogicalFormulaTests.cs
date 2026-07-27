using NUnit.Framework;

namespace DevBrewLabs.Evalis.Tests;

public class LogicalFormulaTests
{
    private IFormulaEngine _formulaEngine;

    [OneTimeSetUp]
    public void Setup()
    {
        _formulaEngine = new FormulaEngine();
    }

    #region Condition Tests

    [TestCase("1 < 2", true)]
    [TestCase("1 <= 2", true)]
    [TestCase("1.23545 <= 2.24555", true)]
    [TestCase("5.65 > 2.987", true)]
    [TestCase("23478 >= 2234", true)]
    [TestCase("1 > 2", false)]
    [TestCase("1000 = 1000", true)]
    [TestCase("1.5 = 1.5", true)]
    [TestCase("1.5 != 1.5", false)]
    [TestCase("\"string\" = \"string\"", true)]
    [TestCase("\"string\" = \"String\"", false)]
    [TestCase("\"string\" != \"String\"", true)]
    [TestCase("\"string\" = \"1\"", false)]
    [TestCase("\"string\" = \"1\"", false)]
    [TestCase("true = true", true)]
    [TestCase("true = false", false)]
    [TestCase("true && true", true)]
    [TestCase("true || true", true)]
    [TestCase("true && true && false", false)]
    [TestCase("true && true && true", true)]
    [TestCase("false || false || true", true)]
    [TestCase("false || true && false", false)]
    [TestCase("1 > SUM(1,2,3)", false)]
    public void ConditionBasic_SuccessTest(string input, object output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }

    [TestCase("SUM(1,2) > SUM(0)", true)]
    [TestCase("SUM(1,20) > 5.5", true)]
    [TestCase("SUM(1, SUM(5,3)) = 9", true)]
    [TestCase("SUM(1, SUM(5,3)) != 9", false)]
    [TestCase("SUM(1, SUM(5,3)) = SUM(1, SUM(5,3))", true)]
    [TestCase("SUM(1, SUM(5,3,SUM(5,9.42))) = SUM(1, SUM(5,3,SUM(5,9.42)))", true)]
    [TestCase("1.323 <= SUM(1.23)", false)]
    [TestCase("1.323 > SUM(1.23)", true)]
    public void ConditionComplex_SuccessTest(string input, object output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }

    [TestCase("== SUM(1,2)")]
    [TestCase(">= SUM(1,2)")]
    [TestCase("<= SUM(1,2)")]
    [TestCase("<= SUM(1,2)")]
    [TestCase("==1")]
    [TestCase("!==1")]
    [TestCase("<=2")]
    public void Condition_FailureTest(string input)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Error, Is.Not.Null);
    }

    #endregion Condition Tests

    [TestCase("IF(1 > 2, true, false)", false)]
    [TestCase("IF(1 = 1, true, false)", true)]
    [TestCase("IF(UPPER(UPPER(\"GraPecity\")) = UPPER(\"Grapecity\"), true, false)", true)]
    [TestCase("IF(1 != 1, true, false)", false)]
    [TestCase("IF(5 <= 6, \"true\", \"false\")", "true")]
    [TestCase("IF(\"test\" = \"test\", \"true\", \"false\")", "true")]
    [TestCase("IF(true && true, true, false)", true)]
    [TestCase("IF(true && false, true, false)", false)]
    [TestCase("IF(SUM(1,2) = SUM(2,1), true, false)", true)]
    public void IFFormula_SuccessTest(string input, object output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }

    [TestCase("IF(1s > 2, true, false)", false)]
    [TestCase("IF(1s = \"sdsd\", true, false)", false)]
    [TestCase("IF(1 + 1, true, false)", true)]
    [TestCase("IF(5 << 6, \"true\", \"false\")", "sad")]
    [TestCase("IF(\"test\" sd \"test\", \"true\", \"false\")", "asd")]
    public void IFFormula_FailureTest(string input, object output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Error, Is.Not.Null);
    }

    [TestCase("COALESCE(null, \"first\", \"second\")", "first")]
    [TestCase("COALESCE(\"one\", \"two\")", "one")]
    public void CoalesceFormula_SuccessTest(string input, string output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }

    [TestCase("ISNUMBER(10)", true)]
    [TestCase("ISNUMBER(\"10\")", true)]
    [TestCase("ISNUMBER(\"abc\")", false)]
    public void IsNumberFormula_SuccessTest(string input, bool output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }

    [TestCase("ISSTRING(\"abc\")", true)]
    [TestCase("ISSTRING(10)", false)]
    public void IsStringFormula_SuccessTest(string input, bool output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }

    [TestCase("IFS(true, 1, false, 2)", 1)]
    [TestCase("IFS(false, 1, true, 2)", 2)]
    public void IfsTest(string input, object output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }

    [TestCase("SWITCH(2, 1, \"A\", 2, \"B\", \"C\")", "B")]
    [TestCase("SWITCH(3, 1, \"A\", 2, \"B\", \"C\")", "C")]
    public void SwitchTest(string input, object output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }

    [TestCase("IFERROR(10, 20)", 10)]
    public void IfErrorTest(string input, object output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }

    [TestCase("IFBLANK(\"\", 10)", 10)]
    [TestCase("IFBLANK(\"   \", 10)", 10)]
    [TestCase("IFBLANK(\"a\", 10)", "a")]
    public void IfBlankTest(string input, object output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }

    [TestCase("ISBOOL(true)", true)]
    [TestCase("ISBOOL(10)", false)]
    public void IsBoolTest(string input, bool output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }

    [TestCase("ISDATE(TODAY())", true)]
    [TestCase("ISDATE(10)", false)]
    public void IsDateTest(string input, bool output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }

    [TestCase("ISARRAY(ARRAY(1, 2))", true)]
    [TestCase("ISARRAY(10)", false)]
    public void IsArrayTest(string input, bool output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }

    [TestCase("ISNULL(\"\")", false)]
    public void IsNullTest(string input, bool output)
    {
        var result = _formulaEngine.Evaluate(input);
        Assert.That(result.Value, Is.EqualTo(output));
    }
}