using System;

namespace AlphaX.FormulaEngine.Formulas
{
    public class CoalesceFormula : Formula
    {
        public CoalesceFormula() : base("COALESCE") { }

        public override object Evaluate(IFormulaContext context)
        {
            for (int i = 0; i < context.Args.Length; i++)
            {
                var arg = context.Args[i];
                if (arg != null && !string.IsNullOrEmpty(arg.ToString()))
                {
                    return arg;
                }
            }
            return null;
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name) { Description = "Returns first non-null argument." };
            info.AddArgument(new ArrayArgument("values", true));
            return info;
        }
    }
}