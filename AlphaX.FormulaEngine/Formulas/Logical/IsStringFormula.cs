using System;

namespace AlphaX.FormulaEngine.Formulas
{
    public class IsStringFormula : Formula
    {
        public IsStringFormula() : base("ISSTRING") { }

        public override object Evaluate(IFormulaContext context)
        {
            if (context.Args.Length > 0 && context.Args[0] != null)
            {
                var val = context.Args[0].ToString();
                if (double.TryParse(val, out _)) return false;
                if (bool.TryParse(val, out _)) return false;
                return true;
            }
            return false;
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name) { Description = "Checks if value is a string." };
            info.AddArgument(new ObjectArgument("value", true));
            return info;
        }
    }
}