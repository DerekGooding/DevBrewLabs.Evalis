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
            object[] sourceArray = args.GetValueOrDefault(0, Array.Empty<object>());
            object[] targetArray = args.GetValueOrDefault(1, Array.Empty<object>());

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
