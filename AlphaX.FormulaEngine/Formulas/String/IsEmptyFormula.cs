using System;

namespace AlphaX.FormulaEngine.Formulas
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
        public override object Evaluate(IFormulaContext context)
        {
            ValidateArgumentCount(context.Args);
            
            if (context.TryGetArg(0, out string text))
            {
                return string.IsNullOrWhiteSpace(text);
            }

            if (!context.TryGetArg(0, out object obj))
            {
                return true; // null or undefined is considered empty
            }

            return obj == null;
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
