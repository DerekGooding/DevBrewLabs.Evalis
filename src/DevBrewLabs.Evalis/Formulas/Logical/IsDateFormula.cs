using System;

namespace DevBrewLabs.Evalis.Formulas
{
    /// <summary>
    /// Represents the ISDATE formula which returns true if the value is a DateTime.
    /// </summary>
    internal class IsDateFormula : Formula
    {
        /// <summary>
        /// Initializes a new ISDATE formula.
        /// </summary>
        public IsDateFormula() : base("ISDATE") { }

        /// <inheritdoc/>
        public override IEvaluationResult Evaluate(IFormulaContext context) => context.TryGetArg(0, out object value) ? EvaluationResult.WithValue(value is DateTime) : EvaluationResult.WithValue(false);

        /// <inheritdoc/>
        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns true if the value is a DateTime."
            };
            info.AddArgument(new ObjectArgument("value", true) { Description = "The value to check." });
            return info;
        }
    }
}