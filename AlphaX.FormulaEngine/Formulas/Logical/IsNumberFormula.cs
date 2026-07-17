using System;

namespace AlphaX.FormulaEngine.Formulas
{
    public class IsNumberFormula : Formula
    {
        public IsNumberFormula() : base("ISNUMBER") { }

        public override object Evaluate(IFormulaContext context)
        {
            if (context.Args.Length > 0 && context.Args[0] != null)
            {
                return double.TryParse(context.Args[0].ToString(), out _);
            }
            return false;
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name) { Description = "Checks if value is a number." };
            info.AddArgument(new ObjectArgument("value", true));
            return info;
        }
    }
}