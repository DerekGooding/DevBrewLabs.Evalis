using System;

namespace DevBrewLabs.Evalis.Formulas
{
    public class SqrtFormula : Formula
    {
        public SqrtFormula() : base("SQRT") { }

        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            if (context.TryGetArg(0, out double num))
            {
                return EvaluationResult.WithValue(Math.Sqrt(num));
            }
            return EvaluationResult.WithValue(0d);
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name) { Description = "Returns square root." };
            info.AddArgument(new DoubleArgument("number", true));
            return info;
        }
    }
}
