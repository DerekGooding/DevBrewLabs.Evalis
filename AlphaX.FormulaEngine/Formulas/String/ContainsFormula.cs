using AlphaX.FormulaEngine.Utils;
using System;

namespace AlphaX.FormulaEngine.Formulas
{
    internal class ContainsFormula : Formula
    {
        public ContainsFormula() : base("CONTAINS") { }

        public override object Evaluate(params object[] args)
        {
            if (!args.TryGetArgument(0, out string source))
            {
                throw new ArgumentException("Invalid argument at index 0. Expected a string value.");
            }

            if (!args.TryGetArgument(1, out string value))
            {
                throw new ArgumentException("Invalid argument at index 1. Expected a string value.");
            }

            return source.Contains(value);
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Checks if the provided string contains the speicifed value."
            };
            info.AddArgument(new StringArgument("source", true)
            {
                Description = "The source string."
            });
            info.AddArgument(new StringArgument("value", true)
            {
                Description = "The value to check for."
            });
            return info;
        }
    }
}
