using System;

namespace DevBrewLabs.Evalis.Formulas
{
    internal class NowFormula : Formula
    {
        public NowFormula() : base("NOW")
        {
        }

        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            return EvaluationResult.WithValue(DateTime.Now);
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns system current date time."
            };
            return info;
        }
    }
}