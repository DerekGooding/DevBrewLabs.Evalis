using System;

namespace DevBrewLabs.Evalis.Formulas
{
    /// <summary>
    /// Represents the TRUNC formula which truncates a number to an integer by removing the fractional part of the number.
    /// </summary>
    internal class TruncFormula : Formula
    {
        /// <summary>
        /// Initializes a new TRUNC formula.
        /// </summary>
        public TruncFormula() : base("TRUNC") { }

        /// <inheritdoc/>
        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            double number = context.GetDoubleArg(0);

            if (context.TryGetArg(1, out double digits))
            {
                double multiplier = Math.Pow(10, digits);
                return EvaluationResult.WithValue(Math.Truncate(number * multiplier) / multiplier);
            }

            return EvaluationResult.WithValue(Math.Truncate(number));
        }

        /// <inheritdoc/>
        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Truncates a number to an integer by removing the fractional part of the number."
            };
            info.AddArgument(new DoubleArgument("number", true) { Description = "The number to truncate." });
            info.AddArgument(new DoubleArgument("digits", false) { Description = "Optional. The precision to truncate to." });
            return info;
        }
    }
}