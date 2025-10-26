using AlphaX.FormulaEngine.Utils;
using System;
using System.Linq;

namespace AlphaX.FormulaEngine.Formulas
{
    internal class CeilingFormula : Formula
    {
        public CeilingFormula() : base("CEILING") { }

        public override object Evaluate(params object[] args)
        {
            if (!args.TryGetArgument(0, out double argument))
            {
                throw new ArgumentException("Invalid argument at index 0. Expected a decimal number.");
            }

            return Math.Ceiling(argument);
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns the smallest integral value that is greater than or equal to the specified decimal number."
            };
            info.AddArgument(new DoubleArgument("value", true)
            {
                Description = "A decimal number."
            });
            return info;
        }
    }
}
