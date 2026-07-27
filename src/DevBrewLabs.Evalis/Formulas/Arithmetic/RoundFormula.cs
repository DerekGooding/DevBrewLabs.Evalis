using System;

namespace DevBrewLabs.Evalis.Formulas
{
    public class RoundFormula : Formula
    {
        public RoundFormula() : base("ROUND")
        {
        }

        public override IEvaluationResult Evaluate(IFormulaContext context) => context.TryGetArg(0, out double num)
                ? context.Args.Length > 1 && context.TryGetArg(1, out double digits)
                    ? EvaluationResult.WithValue(Math.Round(num, (int)digits))
                    : EvaluationResult.WithValue(Math.Round(num))
                : EvaluationResult.WithValue(0d);

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name) { Description = "Rounds a number." };
            info.AddArgument(new DoubleArgument("number", true));
            info.AddArgument(new DoubleArgument("digits", false));
            return info;
        }
    }
}