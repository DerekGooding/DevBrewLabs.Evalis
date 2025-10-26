using Microsoft.VisualStudio.TestPlatform.Utilities;
using NUnit.Framework;

namespace AlphaX.FormulaEngine.Tests
{
    public class SequenceExpressionTests
    {
        private IFormulaEngine _formulaEngine;

        [OneTimeSetUp]
        public void Setup()
        {
            _formulaEngine = new AlphaXFormulaEngine();
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
        public void FailureTest()
        {      
            Assert.Throws<InvalidOperationException>(() =>
            {
                var expr = SequencedExpressionBuilder
                     .Create("Result1", "SUM(1,2,12)")
                     .Next("Result2", "AVERAGE(1,2,$Result2)")
                     .Next("Result3", "SUM(1,2,$Result2)");
            });
        }
    }
}