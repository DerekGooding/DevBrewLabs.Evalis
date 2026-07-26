namespace DevBrewLabs.Evalis.Formulas
{
    internal abstract class StringFormula : Formula
    {
        protected StringFormula(string name) : base(name)
        {
        }

        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
			string value = context.GetStringArg(0);
            return EvaluationResult.WithValue(EvaluateString(value));
        }

        protected abstract object EvaluateString(string value);

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name);

            info.AddArgument(new StringArgument("value", true)
            {
                Description = "Input string value."
            });

            return info;
        }
    }
}
