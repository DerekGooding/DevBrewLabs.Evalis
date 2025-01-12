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
            object[] sourceArray = args.GetValueOrDefault(0, Array.Empty<object>());
            object targetItem = args[1];
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
