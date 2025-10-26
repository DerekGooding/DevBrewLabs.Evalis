using AlphaX.FormulaEngine.Utils;
using System;
using System.Collections;

namespace AlphaX.FormulaEngine.Formulas
{
    public class ArrayIncludesFormula : Formula
    {
        public ArrayIncludesFormula() : base("ARRAYINCLUDES")
        {
        }

        public override object Evaluate(params object[] args)
        {
            if (!args.TryGetArgument(0, out object[] sourceArray))
            {
                throw new ArgumentException("Invalid argument at index 0. Expected an array.");
            }

            if (!args.TryGetArgument(1, out object[] targetArray))
            {
                throw new ArgumentException("Invalid argument at index 1. Expected an array.");
            }

            for(int index = 0; index < targetArray.Length; index++)
            {
                object item = targetArray[index];
                if(!Array.Exists(sourceArray, x => Comparer.Equals(x, item)))
                {
                    return false;
                }
            }

            return true;
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Checks if array includes all values."
            };
            info.AddArgument(new ArrayArgument("source", true)
            {
                Description = "Source array to search in."
            });
            info.AddArgument(new ArrayArgument("values", true)
            {
                Description = "Values to search for."
            });
            return info;
        }
    }
}
