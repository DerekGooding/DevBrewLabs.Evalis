using System.Collections.Generic;
using System.Linq;

namespace DevBrewLabs.Evalis.Formulas
{
    public class MaxFormula : Formula
    {
        public MaxFormula() : base("MAX")
        {
        }

        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            var nums = new List<double>(context.GetFlattenedArgs<double>());
            return nums.Count == 0 ? EvaluationResult.WithValue(0d) : EvaluationResult.WithValue(nums.Max());
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name) { Description = "Returns the maximum value." };
            info.AddArgument(new DoubleArgument("values", true, isVariadic: true)
            {
                Description = "Numeric values or arrays to find maximum"
            });
            return info;
        }
    }
}