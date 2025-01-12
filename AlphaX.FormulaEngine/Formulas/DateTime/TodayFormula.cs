using System;
using System.Globalization;

namespace AlphaX.FormulaEngine.Formulas
{
    internal class TodayFormula : Formula
    {
        public TodayFormula() : base("TODAY")
        {
        }

        public override object Evaluate(params object[] args)
        {
            return DateTime.Now.Date;
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns system current date."
            };

            return info;
        }
    }
}
