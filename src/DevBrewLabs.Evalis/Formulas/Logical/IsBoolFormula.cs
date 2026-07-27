namespace DevBrewLabs.Evalis.Formulas
{
    /// <summary>
    /// Represents the ISBOOL formula which returns true if the value is a boolean.
    /// </summary>
    internal class IsBoolFormula : Formula
    {
        /// <summary>
        /// Initializes a new ISBOOL formula.
        /// </summary>
        public IsBoolFormula() : base("ISBOOL") { }

        /// <inheritdoc/>
        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            if (context.TryGetArg(0, out object value))
            {
                return EvaluationResult.WithValue(value is bool);
            }

            return EvaluationResult.WithValue(false);
        }

        /// <inheritdoc/>
        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns true if the value is a boolean."
            };
            info.AddArgument(new ObjectArgument("value", true) { Description = "The value to check." });
            return info;
        }
    }
}