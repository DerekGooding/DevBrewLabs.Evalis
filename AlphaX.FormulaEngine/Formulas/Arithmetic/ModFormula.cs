using System;

namespace AlphaX.FormulaEngine.Formulas
{
    /// <summary>
    /// Represents the MOD formula which returns the remainder after a number is divided by a divisor.
    /// </summary>
    internal class ModFormula : Formula
    {
        /// <summary>
        /// Initializes a new MOD formula.
        /// </summary>
        public ModFormula() : base("MOD") { }

        /// <inheritdoc/>
        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            
            double number = context.GetDoubleArg(0);
            double divisor = context.GetDoubleArg(1);
            if (divisor == 0)
                return EvaluationResult.WithError(Error.Value("Division by zero in MOD formula."));
            return EvaluationResult.WithValue(number % divisor);
        }

        /// <inheritdoc/>
        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns the remainder after a number is divided by a divisor."
            };
            info.AddArgument(new DoubleArgument("number", true) { Description = "The number for which to find the remainder." });
            info.AddArgument(new DoubleArgument("divisor", true) { Description = "The number by which to divide the number." });
            return info;
        }
    }
}
