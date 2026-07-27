namespace DevBrewLabs.Evalis.Formulas
{
    /// <summary>
    /// Represents the IFBLANK formula which returns a specified value if the expression is null, empty, or consists only of whitespace.
    /// </summary>
    internal class IfBlankFormula : Formula
    {
        /// <summary>
        /// Initializes a new IFBLANK formula.
        /// </summary>
        public IfBlankFormula() : base("IFBLANK") { }

        /// <inheritdoc/>
        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            bool isBlank = false;
            object val = null;

            if (context.TryGetArg(0, out string text))
            {
                isBlank = string.IsNullOrWhiteSpace(text);
                val = text;
            }
            else if (context.TryGetArg(0, out object obj))
            {
                isBlank = obj == null;
                val = obj;
            }
            else
            {
                isBlank = true;
            }

            if (isBlank)
            {
                return EvaluationResult.WithValue(context.GetObjectArg(1));
            }

            return EvaluationResult.WithValue(val);
        }

        /// <inheritdoc/>
        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns a specified value if the expression is null or empty, otherwise returns the expression."
            };
            info.AddArgument(new ObjectArgument("value", true) { Description = "The value to check." });
            info.AddArgument(new ObjectArgument("value_if_blank", true) { Description = "The value to return if the first argument is blank." });
            return info;
        }
    }
}