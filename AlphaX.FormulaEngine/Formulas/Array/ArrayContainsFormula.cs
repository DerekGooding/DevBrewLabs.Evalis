using AlphaX.FormulaEngine.Utils;
using System;
using System.Collections;

namespace AlphaX.FormulaEngine.Formulas
{
    public class ArrayContainsFormula : Formula
    {
        public ArrayContainsFormula() : base("ARRAYCONTAINS")
        {
        }

        public override object Evaluate(params object[] args)
        {
            if (!args.TryGetArgument(0, out object[] sourceArray))
            {
                throw new ArgumentException("Invalid argument at index 0. Expected an array.");
            }

            if (!args.TryGetArgument(1, out object targetItem))
            {
                throw new ArgumentException("Invalid argument at index 1. Expected a value.");
            }

            return Array.Exists(sourceArray, x => Comparer.Equals(x, targetItem));
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Checks if array contains a value."
            };
            info.AddArgument(new ArrayArgument("source", true)
            {
                Description = "Source array to search in."
            });
            info.AddArgument(new ObjectArgument("value", true)
            {
                Description = "Value to search for."
            });
            return info;
        }
    }
}
