using System;
using System.Linq;
using System.Collections.Generic;

namespace AlphaX.FormulaEngine.Formulas
{
    public class MinFormula : Formula
    {
        public MinFormula() : base("MIN") { }

        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            var nums = new List<double>(context.GetFlattenedArgs<double>());
            if (nums.Count == 0) return EvaluationResult.WithValue(0d);
            return EvaluationResult.WithValue(nums.Min());
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name) { Description = "Returns the minimum value." };
            info.AddArgument(new DoubleArgument("values", true, isVariadic: true)
            {
                Description = "Numeric values or arrays to find minimum"
            });
            return info;
        }
    }
}
