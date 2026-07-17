using System.Globalization;
using NUnit.Framework;

namespace AlphaX.FormulaEngine.Tests
{
    public class DateTimeFormulaTests
    {
        private IFormulaEngine _formulaEngine;

        [OneTimeSetUp]
        public void Setup()
        {
            _formulaEngine = new AlphaXFormulaEngine();
        }

        [TestCase("TODAY()")]
        public void Today_SuccessTest(string input)
        {
            var result = _formulaEngine.Evaluate(input);
            Assert.That(result.Value, Is.EqualTo(DateTime.Now.Date));
        }

        [TestCase("NOW()")]
        public void Now_SuccessTest(string input)
        {
            var result = _formulaEngine.Evaluate(input);
            Assert.That(((DateTime)result.Value).Date, Is.EqualTo(DateTime.Now.Date));
        }
    
        [TestCase("YEAR(\"2023-10-15\")", 2023)]
        public void YearFormula_SuccessTest(string input, double output)
        {
            var result = _formulaEngine.Evaluate(input);
            Assert.That(result.Value, Is.EqualTo(output));
        }

        [TestCase("MONTH(\"2023-10-15\")", 10)]
        public void MonthFormula_SuccessTest(string input, double output)
        {
            var result = _formulaEngine.Evaluate(input);
            Assert.That(result.Value, Is.EqualTo(output));
        }

        [TestCase("DAY(\"2023-10-15\")", 15)]
        public void DayFormula_SuccessTest(string input, double output)
        {
            var result = _formulaEngine.Evaluate(input);
            Assert.That(result.Value, Is.EqualTo(output));
        }
}
}
