using System;

namespace AlphaX.FormulaEngine.Formulas
{
    /// <summary>
    /// Represents the LOG formula which returns the logarithm of a number to the base you specify.
    /// </summary>
    internal class LogFormula : Formula
    {
        /// <summary>
        /// Initializes a new LOG formula.
        /// </summary>
        public LogFormula() : base("LOG") { }

        /// <inheritdoc/>
        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            ValidateArgumentCount(context.Args);
            double number = context.GetDoubleArg(0);
            
            if (context.TryGetArg(1, out double baseValue))
            {
                return EvaluationResult.WithValue(Math.Log(number, baseValue));
            }

            return EvaluationResult.WithValue(Math.Log10(number));
        }

        /// <inheritdoc/>
        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns the logarithm of a number to the specified base (default 10)."
            };
            info.AddArgument(new DoubleArgument("number", true) { Description = "The positive real number for which you want the logarithm." });
            info.AddArgument(new DoubleArgument("base", false) { Description = "Optional. The base of the logarithm." });
            return info;
        }
    }
}
