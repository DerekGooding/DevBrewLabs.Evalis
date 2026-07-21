using System;

namespace AlphaX.FormulaEngine.Formulas
{
    public class DayFormula : Formula
    {
        public DayFormula() : base("DAY") { }

        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            if (context.TryGetArg(0, out string dateStr) && DateTime.TryParse(dateStr, out DateTime date))
            {
                return EvaluationResult.WithValue((double)date.Day);
            }
            return EvaluationResult.WithError(Error.Value("Invalid date."));
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name) { Description = "Extracts day." };
            info.AddArgument(new StringArgument("date", true));
            return info;
        }
    }
}
