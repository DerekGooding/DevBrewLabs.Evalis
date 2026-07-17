using NUnit.Framework;

namespace AlphaX.FormulaEngine.Tests
{
    public class ArithmeticFormulaTests
    {
        private IFormulaEngine _formulaEngine;

        [OneTimeSetUp]
        public void Setup()
        {
            _formulaEngine = new AlphaXFormulaEngine();
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
}
}