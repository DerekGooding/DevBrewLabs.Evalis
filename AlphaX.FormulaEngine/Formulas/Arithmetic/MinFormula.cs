using System;
using System.Linq;
using System.Collections.Generic;

namespace AlphaX.FormulaEngine.Formulas
{
    public class MinFormula : Formula
    {
        public MinFormula() : base("MIN") { }

        public override object Evaluate(IFormulaContext context)
        {
            var nums = new List<double>(context.GetFlattenedArgs<double>());
            if (nums.Count == 0) return 0d;
            return nums.Min();
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name) { Description = "Returns the minimum value." };
            info.AddArgument(new ArrayArgument("values", true) { Description = "Numeric values" });
            return info;
        }
    }
}