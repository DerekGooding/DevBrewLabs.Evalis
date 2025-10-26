using AlphaX.FormulaEngine.Resources;
using AlphaX.FormulaEngine.Utils;
using System;

namespace AlphaX.FormulaEngine.Formulas
{
    internal class FloorFormula : Formula
    {
        public FloorFormula() : base("FLOOR") { }

        public override object Evaluate(IFormulaContext context)
        {
			double value = context.GetDoubleArg(0);
			return Math.Floor(value);
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns the largest integral value that is less than or equal to the specified decimal number."
            };
            info.AddArgument(new DoubleArgument("value", true)
            {
                Description = "A decimal number."
            });
            return info;
        }
    }
}