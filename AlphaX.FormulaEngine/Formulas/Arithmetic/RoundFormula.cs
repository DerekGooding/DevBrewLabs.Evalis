using System;

namespace AlphaX.FormulaEngine.Formulas
{
    public class RoundFormula : Formula
    {
        public RoundFormula() : base("ROUND") { }

        public override object Evaluate(IFormulaContext context)
        {
            if (context.TryGetArg(0, out double num))
            {
                if (context.Args.Length > 1 && context.TryGetArg(1, out double digits))
                {
                    return Math.Round(num, (int)digits);
                }
                return Math.Round(num);
            }
            return 0d;
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