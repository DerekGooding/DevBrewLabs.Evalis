namespace DevBrewLabs.Evalis.Formulas
{
    internal class NotFormula : Formula
    {
        public NotFormula() : base("NOT")
        {
        }

        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            bool value = context.GetBooleanArg(0);
            return EvaluationResult.WithValue(!value);
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Inverse a boolean value."
            };
            info.AddArgument(new BooleanArgument("value", true)
            {
                Description = "Value to inverse."
            });
            return info;
        }
    }
}
