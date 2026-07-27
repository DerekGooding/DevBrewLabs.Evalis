namespace DevBrewLabs.Evalis.Formulas
{
    public class IsStringFormula : Formula
    {
        public IsStringFormula() : base("ISSTRING")
        {
        }

        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            if (context.Args.Length > 0 && context.Args[0] != null)
            {
                var val = context.Args[0].ToString();
                return double.TryParse(val, out _)
                    ? EvaluationResult.WithValue(false)
                    : bool.TryParse(val, out _) ? EvaluationResult.WithValue(false) : EvaluationResult.WithValue(true);
            }
            return EvaluationResult.WithValue(false);
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name) { Description = "Checks if value is a string." };
            info.AddArgument(new ObjectArgument("value", true));
            return info;
        }
    }
}