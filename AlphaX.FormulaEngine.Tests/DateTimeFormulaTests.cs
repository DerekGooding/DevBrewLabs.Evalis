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
    }
}
