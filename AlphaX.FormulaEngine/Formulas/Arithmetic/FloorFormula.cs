using AlphaX.FormulaEngine.Utils;
using System;

namespace AlphaX.FormulaEngine.Formulas
{
    internal class FloorFormula : Formula
    {
        public FloorFormula() : base("FLOOR") { }

        public override object Evaluate(params object[] args)
        {
            if (!args.TryGetArgument(0, out double argument))
            {
                throw new ArgumentException("Invalid argument at index 0. Expected a decimal number.");
            }

            return Math.Floor(argument);
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