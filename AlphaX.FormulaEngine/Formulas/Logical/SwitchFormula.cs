using System;
using System.Linq;

namespace AlphaX.FormulaEngine.Formulas
{
    /// <summary>
    /// Represents the SWITCH formula which evaluates an expression against a list of values and returns the result corresponding to the first matching value.
    /// </summary>
    internal class SwitchFormula : Formula
    {
        /// <summary>
        /// Initializes a new SWITCH formula.
        /// </summary>
        public SwitchFormula() : base("SWITCH") { }

        /// <inheritdoc/>
        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            object[] args = context.GetFlattenedArgs<object>().ToArray();
            
            if (args.Length < 3)
            {
                return EvaluationResult.WithError(Error.Value("SWITCH formula requires at least 3 arguments."));
            }

            object expression = args[0];

            // Loop through case/value pairs
            int i = 1;
            for (; i < args.Length - 1; i += 2)
            {
                object caseObj = args[i];
                if (object.Equals(expression, caseObj))
                {
                    return EvaluationResult.WithValue(args[i + 1]);
                }
            }

            // Check if there's a trailing default argument
            if (i < args.Length)
            {
                return EvaluationResult.WithValue(args[i]);
            }

            // No match and no default
            return EvaluationResult.WithError(Error.Value("No match found in SWITCH expression and no default value provided."));
        }

        /// <inheritdoc/>
        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Evaluates an expression against a list of values and returns the result corresponding to the first matching value."
            };
            info.AddArgument(new ObjectArgument("args", true, isVariadic: true) { Description = "Expression, followed by case/value pairs, and an optional default value at the end." });
            return info;
        }
    }
}
