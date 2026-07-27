using NUnit.Framework;

namespace DevBrewLabs.Evalis.Tests
{
    [TestFixture]
    public class ExpressionTest
    {
        [Test]
        public void Evaluate_ShouldHandleBasicArithmetic()
        {
            var engine = new FormulaEngine();

            var result = engine.Evaluate("1+1-(1+2)");

            Assert.That(result.Value, Is.EqualTo(-1d));
        }

        [Test]
        public void Evaluate_ShouldHandleNestedFunctions()
        {
            var engine = new FormulaEngine();

            var result = engine.Evaluate("AVERAGE(SUM(1,2) + SUM(2,4), 2)");

            Assert.That(result.Value, Is.EqualTo(5.5d));
        }

        [Test]
        public void Evaluate_ShouldHandleMixedArithmeticAndFunctions()
        {
            var engine = new FormulaEngine();

            var result = engine.Evaluate("((5 + 5) * 10) / AVERAGE(SUM(1, 4), 5) + 10");

            Assert.That(result.Value, Is.EqualTo(30d));
        }

        [Test]
        public void Evaluate_ShouldHandleFunctionsWithConditionalLogic()
        {
            var engine = new FormulaEngine();

            var result = engine.Evaluate("SUM(10, 20) * (AVERAGE(4, 8) + 2) - 100 / 4 + IF(1 > 0, 5, 0)");

            Assert.That(result.Value, Is.EqualTo(220d));
        }

        [Test]
        public void Evaluate_ShouldHandleVeryComplexExpression()
        {
            var engine = new FormulaEngine();

            var expression = @"((((125 * (38 + 17)) - (942 / (7 + 2))) + ((56 * (91 - 47)) / 8)) * (((18 + 24) * (63 - 29)) - ((144 / 12) + (75 * 3))) + (((999 - (17 * 23)) * ((81 / 9) + (14 * 5))) - ((72 + 18) * (44 - 11))) - ((((300 / 5) + (27 * 19)) * ((88 - 33) + (15 / 3))) - (((47 * 12) - (150 / 6)) * ((29 + 31) - (14 * 2))))) / (((15 + 5) * (12 - 4)) + ((18 * 9) - (72 / 6)))";

            var result = engine.Evaluate(expression);

            Assert.That(result.Value, Is.EqualTo(27284.596774193549d));
        }

        [Test]
        public void Evaluate_VeryComplexExpressionWithFormula()
        {
            var engine = new FormulaEngine();

            var complexExpr = @"((SUM(12,34,56,AVERAGE(10,20,30),SUM(5,15,25),(90-18)/3)*AVERAGE(SUM(10,20,30),80,150/5,SUM(3,7,11)))+(SUM(100,200,AVERAGE(25,35,45),SUM(11,22,33))*AVERAGE(40,SUM(5,15),60,AVERAGE(70,80,90)))-(SUM(50,AVERAGE(60,90),SUM(7,14,21),80/4)*AVERAGE(SUM(5,10),25,35,18*2)))/AVERAGE(SUM(10,20,30),40,AVERAGE(50,70),SUM(5,15))";

            var result = engine.Evaluate(complexExpr);
            Assert.That(result.Value, Is.EqualTo(532.9111111111111d));
        }
    }
}