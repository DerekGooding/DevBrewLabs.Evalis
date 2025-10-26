using AlphaX.FormulaEngine.Utils;
using System;

namespace AlphaX.FormulaEngine.Formulas
{
    internal class RoundFormula : Formula
    {
        public RoundFormula() : base("ROUND") { }

        public override object Evaluate(params object[] args)
        {
            ValidateArgumentCount(args);

            if (!args.TryGetArgument(0, out double value))
            {
                throw new ArgumentException("Invalid argument at index 0. Expected a decimal number.");
            }

            args.TryGetArgument(1, out int digits);
            return Math.Round(value, digits);
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
                Description = "The number of fractional places in return value",
            });
            return info;
        }
    }
}
