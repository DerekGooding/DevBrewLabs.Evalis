using System;

namespace AlphaX.FormulaEngine.Formulas
{
    public class TrimFormula : Formula
    {
        public TrimFormula() : base("TRIM") { }

        public override object Evaluate(IFormulaContext context)
        {
            if (context.TryGetArg(0, out string text))
            {
                return text.Trim();
            }
            return string.Empty;
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name) { Description = "Removes whitespace." };
            info.AddArgument(new StringArgument("text", true));
            return info;
        }
    }
}