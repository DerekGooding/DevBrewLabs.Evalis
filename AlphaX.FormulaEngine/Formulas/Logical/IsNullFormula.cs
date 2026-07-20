using System;

namespace AlphaX.FormulaEngine.Formulas
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
        public override object Evaluate(IFormulaContext context)
        {
            ValidateArgumentCount(context.Args);
            
            if (context.TryGetArg(0, out object value))
            {
                return value == null;
            }

            return true; // if we couldn't get it, it evaluated to null
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
