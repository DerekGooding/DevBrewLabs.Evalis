using AlphaX.FormulaEngine.Utils;
using System;

namespace AlphaX.FormulaEngine.Formulas
{
    internal class AverageFormula : Formula
    {
        public AverageFormula() : base("AVERAGE") { }

        public override object Evaluate(params object[] args)
        {
            double sum = 0;
            int totalArguments = 0;

            for (int index = 0; index < args.Length; index++)
            {
                if(args.TryGetArgument(index, out double argument))
                {
                    sum += argument;
                    totalArguments++;
                }
            }

            return sum / totalArguments;
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns average of provided values."
            };
            info.AddArgument(new ArrayArgument("values", true)
            {
                Description = "Array of numeric values."
            });
            return info;
        }
    }
}
