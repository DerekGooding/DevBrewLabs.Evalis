using System;

namespace AlphaX.FormulaEngine.Formulas
{
    /// <summary>
    /// Represents the EXP formula which returns e raised to the power of number.
    /// </summary>
    internal class ExpFormula : Formula
    {
        /// <summary>
        /// Initializes a new EXP formula.
        /// </summary>
        public ExpFormula() : base("EXP") { }

        /// <inheritdoc/>
        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            double number = context.GetDoubleArg(0);
            return EvaluationResult.WithValue(Math.Exp(number));
        }

        /// <inheritdoc/>
        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns e raised to the power of a given number."
            };
            info.AddArgument(new DoubleArgument("number", true) { Description = "The exponent applied to the base e." });
            return info;
        }
    }
}
