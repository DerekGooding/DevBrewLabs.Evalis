namespace DevBrewLabs.Evalis.Formulas
{
    /// <summary>
    /// Represents the IFERROR formula which returns a value you specify if a formula evaluates to an error; otherwise, returns the result of the formula.
    /// </summary>
    internal class IfErrorFormula : Formula
    {
        /// <summary>
        /// Initializes a new IFERROR formula.
        /// </summary>
        public IfErrorFormula() : base("IFERROR") => HandlesErrors = true;

        /// <inheritdoc/>
        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            var result = context.Args[0] as IEvaluationResult;
            return result?.Error != null
                ? EvaluationResult.WithValue(context.Args[1])
                : result ?? EvaluationResult.WithValue(context.Args[0]);
        }

        /// <inheritdoc/>
        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns a value you specify if a formula evaluates to an error; otherwise, returns the result of the formula."
            };
            info.AddArgument(new ObjectArgument("value", true) { Description = "The value to check for an error." });
            info.AddArgument(new ObjectArgument("value_if_error", true) { Description = "The value to return if the formula evaluates to an error." });
            return info;
        }
    }
}