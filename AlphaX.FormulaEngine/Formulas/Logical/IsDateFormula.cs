using System;

namespace AlphaX.FormulaEngine.Formulas
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
        public override object Evaluate(IFormulaContext context)
        {
            ValidateArgumentCount(context.Args);
            
            if (context.TryGetArg(0, out object value))
            {
                return value is DateTime;
            }

            return false;
        }

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
