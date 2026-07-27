namespace DevBrewLabs.Evalis.Formulas
{
    /// <summary>
    /// Represents the ISEMPTY formula which checks if a string is null or consists only of white-space characters.
    /// </summary>
    internal class IsEmptyFormula : Formula
    {
        /// <summary>
        /// Initializes a new ISEMPTY formula.
        /// </summary>
        public IsEmptyFormula() : base("ISEMPTY") { }

        /// <inheritdoc/>
        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            if (context.TryGetArg(0, out string text))
            {
                return EvaluationResult.WithValue(string.IsNullOrWhiteSpace(text));
            }

            if (!context.TryGetArg(0, out object obj))
            {
                return EvaluationResult.WithValue(true);
            }

            return EvaluationResult.WithValue(obj == null);
        }

        /// <inheritdoc/>
        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns true if a string is null, empty, or consists only of whitespace."
            };
            info.AddArgument(new ObjectArgument("value", true) { Description = "The value to check." });
            return info;
        }
    }
}