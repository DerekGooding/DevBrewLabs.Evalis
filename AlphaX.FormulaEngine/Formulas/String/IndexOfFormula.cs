using System;

namespace AlphaX.FormulaEngine.Formulas
{
    public class IndexOfFormula : Formula
    {
        public IndexOfFormula() : base("INDEXOF") { }

        public override object Evaluate(IFormulaContext context)
        {
            if (context.TryGetArg(0, out string text) && context.TryGetArg(1, out string search))
            {
                return (double)text.IndexOf(search);
            }
            return -1d;
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name) { Description = "Finds index of substring." };
            info.AddArgument(new StringArgument("text", true));
            info.AddArgument(new StringArgument("search", true));
            return info;
        }
    }
}