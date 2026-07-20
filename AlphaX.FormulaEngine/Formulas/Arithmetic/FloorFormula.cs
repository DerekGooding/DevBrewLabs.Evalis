using System;

namespace AlphaX.FormulaEngine.Formulas
{
    internal class FloorFormula : Formula
    {
        public FloorFormula() : base("FLOOR") { }

        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
			double value = context.GetDoubleArg(0);
			return EvaluationResult.WithValue(Math.Floor(value));
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns the largest integral value that is less than or equal to the specified decimal number."
            };
            info.AddArgument(new DoubleArgument("value", true)
            {
                Description = "A decimal number."
            });
            return info;
        }
    }
}
