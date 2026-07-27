using System;

namespace DevBrewLabs.Evalis.Formulas
{
    internal class TodayFormula : Formula
    {
        public TodayFormula() : base("TODAY")
        {
        }

        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            return EvaluationResult.WithValue(DateTime.Now.Date);
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns system current date."
            };

            return info;
        }
    }
}