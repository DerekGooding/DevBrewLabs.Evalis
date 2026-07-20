using System;

namespace AlphaX.FormulaEngine.Formulas
{
    public class CoalesceFormula : Formula
    {
        public CoalesceFormula() : base("COALESCE") { }

        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            foreach (var arg in context.GetFlattenedArgs<object>())
            {
                if (arg != null && !string.IsNullOrEmpty(arg.ToString()))
                    return EvaluationResult.WithValue(arg);
            }
            return EvaluationResult.WithValue(null);
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name) { Description = "Returns first non-null argument." };
            info.AddArgument(new ArrayArgument("values", true));
            return info;
        }
    }
}
