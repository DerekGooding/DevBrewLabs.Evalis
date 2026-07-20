using System;

namespace AlphaX.FormulaEngine.Formulas
{
    public class MonthFormula : Formula
    {
        public MonthFormula() : base("MONTH") { }

        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            if (context.TryGetArg(0, out string dateStr) && DateTime.TryParse(dateStr, out DateTime date))
            {
                return EvaluationResult.WithValue((double)date.Month);
            }
            throw new ArgumentException("Invalid date.");
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name) { Description = "Extracts month." };
            info.AddArgument(new StringArgument("date", true));
            return info;
        }
    }
}
