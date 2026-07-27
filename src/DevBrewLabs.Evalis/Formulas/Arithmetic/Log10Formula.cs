using System;

namespace DevBrewLabs.Evalis.Formulas
{
    /// <summary>
    /// Represents the LOG10 formula which returns the base-10 logarithm of a number.
    /// </summary>
    internal class Log10Formula : Formula
    {
        /// <summary>
        /// Initializes a new LOG10 formula.
        /// </summary>
        public Log10Formula() : base("LOG10") { }

        /// <inheritdoc/>
        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            double number = context.GetDoubleArg(0);
            return EvaluationResult.WithValue(Math.Log10(number));
        }

        /// <inheritdoc/>
        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns the base-10 logarithm of a number."
            };
            info.AddArgument(new DoubleArgument("number", true) { Description = "The positive real number for which you want the base-10 logarithm." });
            return info;
        }
    }
}