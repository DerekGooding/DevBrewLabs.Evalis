using System;

namespace DevBrewLabs.Evalis.Formulas
{
    public class DayFormula : Formula
    {
        public DayFormula() : base("DAY")
        {
        }

        public override IEvaluationResult Evaluate(IFormulaContext context)
            => context.TryGetArg(0, out string dateStr) && DateTime.TryParse(dateStr, out DateTime date)
                ? EvaluationResult.WithValue((double)date.Day)
                : EvaluationResult.WithError(Error.Value("Invalid date."));

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name) { Description = "Extracts day." };
            info.AddArgument(new StringArgument("date", true));
            return info;
        }
    }
}