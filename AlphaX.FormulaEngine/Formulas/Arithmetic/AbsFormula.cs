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

        public override object Evaluate(IFormulaContext context)
        {
            double value = context.GetDoubleArg(0);
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
