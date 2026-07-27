using System;

namespace DevBrewLabs.Evalis.Formulas
{
    public class SqrtFormula : Formula
    {
        public SqrtFormula() : base("SQRT")
        {
        }

        public override IEvaluationResult Evaluate(IFormulaContext context) => context.TryGetArg(0, out double num) ? EvaluationResult.WithValue(Math.Sqrt(num)) : EvaluationResult.WithValue(0d);

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name) { Description = "Returns square root." };
            info.AddArgument(new DoubleArgument("number", true));
            return info;
        }
    }
}