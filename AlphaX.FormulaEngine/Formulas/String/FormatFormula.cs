using System;

namespace AlphaX.FormulaEngine.Formulas
{
    /// <summary>
    /// Represents the FORMAT formula which formats a value according to a standard .NET format string.
    /// </summary>
    internal class FormatFormula : Formula
    {
        /// <summary>
        /// Initializes a new FORMAT formula.
        /// </summary>
        public FormatFormula() : base("FORMAT") { }

        /// <inheritdoc/>
        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            
            
            if (!context.TryGetArg(0, out object value) || value == null)
            {
                return EvaluationResult.WithValue(string.Empty);
            }

            string format = context.GetStringArg(1);

            if (value is IFormattable formattable)
            {
                return EvaluationResult.WithValue(formattable.ToString(format, null));
            }

            return EvaluationResult.WithValue(value.ToString());
        }

        /// <inheritdoc/>
        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Formats a value according to a format string (e.g., 'C' for currency, 'yyyy-MM-dd' for dates)."
            };
            info.AddArgument(new ObjectArgument("value", true) { Description = "The value to format." });
            info.AddArgument(new StringArgument("format", true) { Description = "The format string." });
            return info;
        }
    }
}
