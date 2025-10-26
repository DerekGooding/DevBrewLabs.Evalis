using AlphaX.FormulaEngine.Resources;
using AlphaX.FormulaEngine.Utils;
using System;

namespace AlphaX.FormulaEngine.Formulas
{
    internal class AbsFormula : Formula
    {
        public AbsFormula() : base("ABS")
        {
            
        }

        public override object Evaluate(params object[] args)
        {
            if (!args.TryGetArgument(0, out double value))
            {
                throw new ArgumentException(string.Format(FormulaResources.InvalidDecimalArgument, 0));
            }

            return Math.Abs(value);
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns the absolute value of the decimal number."
            };
            info.AddArgument(new DoubleArgument("value", true)
            {
                Description = "A decimal number."
            });
            return info;
        }
    }
}
