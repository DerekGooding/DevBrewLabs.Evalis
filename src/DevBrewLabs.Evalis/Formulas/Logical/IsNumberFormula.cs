namespace DevBrewLabs.Evalis.Formulas
{
    public class IsNumberFormula : Formula
    {
        public IsNumberFormula() : base("ISNUMBER")
        {
        }

        public override IEvaluationResult Evaluate(IFormulaContext context) => context.Args.Length > 0 && context.Args[0] != null
                ? EvaluationResult.WithValue(double.TryParse(context.Args[0].ToString(), out _))
                : EvaluationResult.WithValue(false);

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name) { Description = "Checks if value is a number." };
            info.AddArgument(new ObjectArgument("value", true));
            return info;
        }
    }
}