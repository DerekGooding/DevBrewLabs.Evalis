using AlphaX.FormulaEngine.Utils;
using System;

namespace AlphaX.FormulaEngine.Formulas
{
    internal class SumFormula : Formula
    {
        public SumFormula() : base("SUM") { }

        public override object Evaluate(params object[] args)
        {
            double sum = 0;

            for (int index = 0; index < args.Length; index++)
            {
                if (args.TryGetArgument(index, out double argument))
                {
                    sum += argument;
                }
            }

            return sum;
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns sum of provided values"
            };
            info.AddArgument(new ArrayArgument("values", true)
            {
                Description = "Array of numeric values"
            });
            return info;
        }
    }
}
