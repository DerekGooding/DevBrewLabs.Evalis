using System;

namespace DevBrewLabs.Evalis.Formulas
{
    /// <summary>
    /// Represents the SIGN formula which returns the sign of a number (-1, 0, or 1).
    /// </summary>
    internal class SignFormula : Formula
    {
        /// <summary>
        /// Initializes a new SIGN formula.
        /// </summary>
        public SignFormula() : base("SIGN") { }

        /// <inheritdoc/>
        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            
            double number = context.GetDoubleArg(0);
            return EvaluationResult.WithValue((double)Math.Sign(number));
        }

        /// <inheritdoc/>
        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns the sign of a number: 1 if positive, -1 if negative, 0 if zero."
            };
            info.AddArgument(new DoubleArgument("number", true) { Description = "The number to evaluate." });
            return info;
        }
    }
}
