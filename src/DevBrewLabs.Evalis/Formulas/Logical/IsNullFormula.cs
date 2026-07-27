namespace DevBrewLabs.Evalis.Formulas
{
    /// <summary>
    /// Represents the ISNULL formula which returns true if the value is null.
    /// </summary>
    internal class IsNullFormula : Formula
    {
        /// <summary>
        /// Initializes a new ISNULL formula.
        /// </summary>
        public IsNullFormula() : base("ISNULL") { }

        /// <inheritdoc/>
        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            if (context.TryGetArg(0, out object value))
            {
                return EvaluationResult.WithValue(value == null);
            }

            return EvaluationResult.WithValue(true);
        }

        /// <inheritdoc/>
        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns true if the value is null."
            };
            info.AddArgument(new ObjectArgument("value", true) { Description = "The value to check." });
            return info;
        }
    }
}