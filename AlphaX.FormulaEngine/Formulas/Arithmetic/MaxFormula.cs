using System;
using System.Linq;
using System.Collections.Generic;

namespace AlphaX.FormulaEngine.Formulas
{
    public class MaxFormula : Formula
    {
        public MaxFormula() : base("MAX") { }

        public override object Evaluate(IFormulaContext context)
        {
            var nums = new List<double>();
            for (int i = 0; i < context.Args.Length; i++)
            {
                if (context.TryGetArg(i, out double arg))
                    nums.Add(arg);
            }
            if (nums.Count == 0) return 0d;
            return nums.Max();
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name) { Description = "Returns the maximum value." };
            info.AddArgument(new ArrayArgument("values", true) { Description = "Numeric values" });
            return info;
        }
    }
}