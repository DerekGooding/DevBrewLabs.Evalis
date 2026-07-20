using System;

namespace AlphaX.FormulaEngine.Formulas
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
        public override object Evaluate(IFormulaContext context)
        {
            ValidateArgumentCount(context.Args);
            
            if (context.TryGetArg(0, out string text))
            {
                return string.IsNullOrEmpty(text);
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
                Description = "Returns true if a string is null or an empty string."
            };
            info.AddArgument(new ObjectArgument("value", true) { Description = "The value to check." });
            return info;
        }
    }
}
