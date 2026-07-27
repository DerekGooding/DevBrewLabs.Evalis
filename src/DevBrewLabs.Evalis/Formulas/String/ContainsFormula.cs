namespace DevBrewLabs.Evalis.Formulas
{
    internal class ContainsFormula : Formula
    {
        public ContainsFormula() : base("CONTAINS")
        {
        }

        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            string source = context.GetStringArg(0);
            string value = context.GetStringArg(1);
            return EvaluationResult.WithValue(source.Contains(value));
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Checks if the provided string contains the speicifed value."
            };
            info.AddArgument(new StringArgument("source", true)
            {
                Description = "The source string."
            });
            info.AddArgument(new StringArgument("value", true)
            {
                Description = "The value to check for."
            });
            return info;
        }
    }
}