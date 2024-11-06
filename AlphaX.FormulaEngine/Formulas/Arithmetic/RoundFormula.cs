using System;

namespace AlphaX.FormulaEngine.Formulas
{
    internal class RoundFormula : Formula
    {
        public RoundFormula() : base("ROUND") { }

        public override object Evaluate(params object[] args)
        {
            double value = (double)args[0];

            if (args.Length == 2)
            {            
                int digits = (int)args[1];
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
