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
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns the first non-null value from the provided arguments."
            };
            info.AddArgument(new ObjectArgument("values", true, isVariadic: true) { Description = "Values to evaluate." });
            return info;
        }
    }
}
