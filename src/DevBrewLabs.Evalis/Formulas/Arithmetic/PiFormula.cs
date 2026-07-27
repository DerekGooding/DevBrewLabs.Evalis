using System;

namespace DevBrewLabs.Evalis.Formulas
{
    /// <summary>
    /// Represents the PI formula which returns the mathematical constant pi.
    /// </summary>
    internal class PiFormula : Formula
    {
        /// <summary>
        /// Initializes a new PI formula.
        /// </summary>
        public PiFormula() : base("PI") { }

        /// <inheritdoc/>
        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            return EvaluationResult.WithValue(Math.PI);
        }

        /// <inheritdoc/>
        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns the value of pi (3.14159265358979)."
            };
            return info;
        }
    }
}