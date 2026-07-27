using System;

namespace DevBrewLabs.Evalis.Formulas
{
    public class MonthFormula : Formula
    {
        public MonthFormula() : base("MONTH")
        {
        }

        public override IEvaluationResult Evaluate(IFormulaContext context) => context.TryGetArg(0, out string dateStr) && DateTime.TryParse(dateStr, out DateTime date)
                ? EvaluationResult.WithValue((double)date.Month)
                : EvaluationResult.WithError(Error.Value("Invalid date."));

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name) { Description = "Extracts month." };
            info.AddArgument(new StringArgument("date", true));
            return info;
        }
    }
}