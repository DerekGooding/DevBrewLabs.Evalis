namespace AlphaX.FormulaEngine
{
    public abstract class Formula : FormulaBase
    {
        protected Formula(string name) : base(name, false)
        {
        }

        /// <summary>
        /// Gets the evaluated result.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public abstract object Evaluate(IFormulaContext context);
    }
}
