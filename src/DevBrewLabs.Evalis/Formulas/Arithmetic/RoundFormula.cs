using System;

namespace DevBrewLabs.Evalis.Formulas
{
    public class RoundFormula : Formula
    {
        public RoundFormula() : base("ROUND") { }

        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            if (context.TryGetArg(0, out double num))
            {
                if (context.Args.Length > 1 && context.TryGetArg(1, out double digits))
                {
                    return EvaluationResult.WithValue(Math.Round(num, (int)digits));
                }
                return EvaluationResult.WithValue(Math.Round(num));
            }
            return EvaluationResult.WithValue(0d);
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name) { Description = "Rounds a number." };
            info.AddArgument(new DoubleArgument("number", true));
            info.AddArgument(new DoubleArgument("digits", false));
            return info;
        }
    }
}
