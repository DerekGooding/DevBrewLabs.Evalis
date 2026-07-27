using System;

namespace DevBrewLabs.Evalis.Formulas
{
    public class PowerFormula : Formula
    {
        public PowerFormula() : base("POWER")
        {
        }

        public override IEvaluationResult Evaluate(IFormulaContext context) => context.TryGetArg(0, out double baseNum) && context.TryGetArg(1, out double exp)
                ? EvaluationResult.WithValue(Math.Pow(baseNum, exp))
                : EvaluationResult.WithValue(0d);

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name) { Description = "Returns the result of a number raised to a power." };
            info.AddArgument(new DoubleArgument("number", true));
            info.AddArgument(new DoubleArgument("power", true));
            return info;
        }
    }
}