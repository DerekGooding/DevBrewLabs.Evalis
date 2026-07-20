using System.Threading.Tasks;

namespace AlphaX.FormulaEngine
{
    /// <summary>
    /// Abstract base class for asynchronous formula implementations. Override EvaluateAsync to provide the formula logic.
    /// </summary>
    public abstract class AsyncFormula : FormulaBase
    {
        /// <summary>
        /// Initializes a new asynchronous Formula.
        /// </summary>
        /// <param name="name">The unique formula name.</param>
        protected AsyncFormula(string name) : base(name, true)
        {
        }

        /// <summary>
        /// Gets the evaluated result.
        /// </summary>
        /// <param name="context">The formula context containing the resolved arguments.</param>
        /// <returns>A task that resolves to the evaluated result.</returns>
        public abstract Task<object> EvaluateAsync(IFormulaContext context);
    }
}
