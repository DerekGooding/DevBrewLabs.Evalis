using System.Threading.Tasks;

namespace AlphaX.FormulaEngine
{
    public abstract class AsyncFormula : FormulaBase
    {
        protected AsyncFormula(string name) : base(name, true)
        {
        }

        /// <summary>
        /// Gets the evaluated result.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public abstract Task<object> EvaluateAsync(IFormulaContext context);
    }
}
