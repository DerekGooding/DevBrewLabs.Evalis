namespace AlphaX.FormulaEngine
{
    /// <summary>
    /// Abstract base class for synchronous formula implementations. Override Evaluate to provide the formula logic.
    /// </summary>
    public abstract class Formula : FormulaBase
    {
        /// <summary>
        /// Initializes a new synchronous Formula.
        /// </summary>
        /// <param name="name">The unique formula name.</param>
        protected Formula(string name) : base(name, false)
        {
        }

        /// <summary>
        /// Gets the evaluated result.
        /// </summary>
        /// <param name="context">The formula context containing the resolved arguments.</param>
        /// <returns>The evaluated result.</returns>
        public abstract object Evaluate(IFormulaContext context);
    }
}
