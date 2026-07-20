using AlphaX.FormulaEngine.Resources;
using System;

namespace AlphaX.FormulaEngine.Formulas
{
    internal class AbsFormula : Formula
    {
        public AbsFormula() : base("ABS")
        {
            
        }

        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            double value = context.GetDoubleArg(0);
            return EvaluationResult.WithValue(Math.Abs(value));
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns the absolute value of the decimal number."
            };
            info.AddArgument(new DoubleArgument("value", true)
            {
                Description = "A decimal number."
            });
            return info;
        }
    }
}
