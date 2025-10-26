using AlphaX.FormulaEngine.Utils;
using System;

namespace AlphaX.FormulaEngine.Formulas
{
    internal class IFFormula : Formula
    {
        public IFFormula() : base("IF")
        {

        }

        public override object Evaluate(params object[] args)
        {
            if (!args.TryGetArgument(0, out bool condition))
            {
                throw new ArgumentException("Invalid argument at index 0. Expected a boolean.");
            }

            if (!args.TryGetArgument(1, out object trueValue))
            {
                throw new ArgumentException("Invalid argument at index 1. Expected a value.");
            }

            if (!args.TryGetArgument(2, out object falseValue))
            {
                throw new ArgumentException("Invalid argument at index 2. Expected a value.");
            }

            return condition ? trueValue : falseValue;
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Checks whether condition is met. Returns first value if true and return second value if false."
            };
            info.AddArgument(new BooleanArgument("condition", true)
            {
                Description = "Condition to evaluate."
            });
            info.AddArgument(new ObjectArgument("value1", true)
            {
                Description = "Value to return if condition is true."
            });
            info.AddArgument(new ObjectArgument("value2", true)
            {
                Description = "Value to return if condition is false."
            });
            return info;
        }
    }
}
