using System;

namespace AlphaX.FormulaEngine.Formulas
{
    public class YearFormula : Formula
    {
        public YearFormula() : base("YEAR") { }

        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            if (context.TryGetArg(0, out string dateStr) && DateTime.TryParse(dateStr, out DateTime date))
            {
                return EvaluationResult.WithValue((double)date.Year);
            }
            throw new ArgumentException("Invalid date.");
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name) { Description = "Extracts year." };
            info.AddArgument(new StringArgument("date", true));
            return info;
        }
    }
}
