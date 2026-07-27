namespace DevBrewLabs.Evalis.Formulas
{
    /// <summary>
    /// Represents the ISNULLOREMPTY formula which checks if a string is null or an empty string.
    /// </summary>
    internal class IsNullOrEmptyFormula : Formula
    {
        /// <summary>
        /// Initializes a new ISNULLOREMPTY formula.
        /// </summary>
        public IsNullOrEmptyFormula() : base("ISNULLOREMPTY") { }

        /// <inheritdoc/>
        public override IEvaluationResult Evaluate(IFormulaContext context) => context.TryGetArg(0, out string text)
                ? EvaluationResult.WithValue(string.IsNullOrEmpty(text))
                : !context.TryGetArg(0, out object obj) ? EvaluationResult.WithValue(true) : EvaluationResult.WithValue(obj == null);

        /// <inheritdoc/>
        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns true if a string is null or an empty string."
            };
            info.AddArgument(new ObjectArgument("value", true) { Description = "The value to check." });
            return info;
        }
    }
}