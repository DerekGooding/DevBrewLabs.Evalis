using System;
using System.Collections;

namespace DevBrewLabs.Evalis.Formulas
{
    public class ArrayContainsFormula : Formula
    {
        public ArrayContainsFormula() : base("ARRAYCONTAINS")
        {
        }

        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            object[] sourceArray = context.GetArrayArg(0);
			object targetItem = context.GetObjectArg(1);
            return EvaluationResult.WithValue(Array.Exists(sourceArray, x => Comparer.Equals(x, targetItem)));
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
