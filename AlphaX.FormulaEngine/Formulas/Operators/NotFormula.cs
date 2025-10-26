using AlphaX.FormulaEngine.Utils;
using System;

namespace AlphaX.FormulaEngine.Formulas
{
    internal class NotFormula : Formula
    {
        public NotFormula() : base("NOT")
        {
        }

        public override object Evaluate(params object[] args)
        {
            if (!args.TryGetArgument(0, out bool value))
            {
                throw new ArgumentException("Invalid argument at index 0. Expected a boolean.");
            }

            return !value;
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Inverse a boolean value."
            };
            info.AddArgument(new BooleanArgument("value", true)
            {
                Description = "Value to inverse."
            });
            return info;
        }
    }
}
