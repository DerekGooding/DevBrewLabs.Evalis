using System;
using System.Collections;

namespace AlphaX.FormulaEngine.Formulas
{
    /// <summary>
    /// Represents the ISARRAY formula which returns true if the value is an array or enumerable collection (excluding strings).
    /// </summary>
    internal class IsArrayFormula : Formula
    {
        /// <summary>
        /// Initializes a new ISARRAY formula.
        /// </summary>
        public IsArrayFormula() : base("ISARRAY") { }

        /// <inheritdoc/>
        public override object Evaluate(IFormulaContext context)
        {
            ValidateArgumentCount(context.Args);
            
            if (context.TryGetArg(0, out object value))
            {
                return value != null && !(value is string) && value is IEnumerable;
            }

            return false;
        }

        /// <inheritdoc/>
        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns true if the value is an array."
            };
            info.AddArgument(new ObjectArgument("value", true) { Description = "The value to check." });
            return info;
        }
    }
}
