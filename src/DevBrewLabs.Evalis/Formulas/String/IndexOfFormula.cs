using System;

namespace DevBrewLabs.Evalis.Formulas
{
    public class IndexOfFormula : Formula
    {
        public IndexOfFormula() : base("INDEXOF") { }

        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            if (context.TryGetArg(0, out string text) && context.TryGetArg(1, out string search))
            {
                return EvaluationResult.WithValue((double)text.IndexOf(search));
            }
            return EvaluationResult.WithValue(-1d);
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name) { Description = "Finds index of substring." };
            info.AddArgument(new StringArgument("text", true));
            info.AddArgument(new StringArgument("search", true));
            return info;
        }
    }
}
