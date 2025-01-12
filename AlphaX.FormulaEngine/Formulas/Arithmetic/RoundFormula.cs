using AlphaX.FormulaEngine.Utils;
using System;

namespace AlphaX.FormulaEngine.Formulas
{
    internal class RoundFormula : Formula
    {
        public RoundFormula() : base("ROUND") { }

        public override object Evaluate(params object[] args)
        {
            double value = args.GetValueOrDefault(0, 0d);

            if (args.Length == 2)
            {
                int digits = args.GetValueOrDefault(1, 0);
                return Math.Round(value, digits);
            }

            return Math.Round(value);        
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Rounds a double-precision floating-point value to a specified number of fractional digits."
            };
            info.AddArgument(new DoubleArgument("value", true)
            {
                Description = "A decimal number."
            });
            info.AddArgument(new DoubleArgument("digits", false)
            {
                Description = "The number of fractional places in return value"
            });
            return info;
        }
    }
}
