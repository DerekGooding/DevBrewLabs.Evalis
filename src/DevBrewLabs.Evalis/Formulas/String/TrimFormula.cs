namespace DevBrewLabs.Evalis.Formulas
{
    public class TrimFormula : Formula
    {
        public TrimFormula() : base("TRIM")
        {
        }

        public override IEvaluationResult Evaluate(IFormulaContext context) => context.TryGetArg(0, out string text) ? EvaluationResult.WithValue(text.Trim()) : EvaluationResult.WithValue(string.Empty);

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name) { Description = "Removes whitespace." };
            info.AddArgument(new StringArgument("text", true));
            return info;
        }
    }
}