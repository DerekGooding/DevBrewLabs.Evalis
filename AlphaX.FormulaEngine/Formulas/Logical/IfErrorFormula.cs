using System;

namespace AlphaX.FormulaEngine.Formulas
{
    /// <summary>
    /// Represents the IFERROR formula which returns a value you specify if a formula evaluates to an error; otherwise, returns the result of the formula.
    /// </summary>
    internal class IfErrorFormula : Formula
    {
        /// <summary>
        /// Initializes a new IFERROR formula.
        /// </summary>
        public IfErrorFormula() : base("IFERROR") { }

        /// <inheritdoc/>
        public override object Evaluate(IFormulaContext context)
        {
            ValidateArgumentCount(context.Args);
            
            try
            {
                // This will return the value if the AST didn't error (though engine currently throws eagerly).
                object val = context.GetObjectArg(0);
                return val;
            }
            catch
            {
                // Return the fallback value if the first argument errors out
                return context.GetObjectArg(1);
            }
        }

        /// <inheritdoc/>
        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns a value you specify if a formula evaluates to an error; otherwise, returns the result of the formula."
            };
            info.AddArgument(new ObjectArgument("value", true) { Description = "The value to check for an error." });
            info.AddArgument(new ObjectArgument("value_if_error", true) { Description = "The value to return if the formula evaluates to an error." });
            return info;
        }
    }
}
