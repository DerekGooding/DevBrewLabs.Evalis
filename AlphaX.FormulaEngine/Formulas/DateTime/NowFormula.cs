using AlphaX.FormulaEngine.Utils;
using System;
using System.Globalization;

namespace AlphaX.FormulaEngine.Formulas
{
    internal class NowFormula : Formula
    {
        public NowFormula() : base("NOW")
        {
        }

        public override object Evaluate(IFormulaContext context)
        {
            return DateTime.Now;
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns system current date time."
            };
            return info;
        }
    }
}
