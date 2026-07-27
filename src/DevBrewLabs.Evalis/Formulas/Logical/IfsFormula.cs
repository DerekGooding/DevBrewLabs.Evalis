using System.Linq;

namespace DevBrewLabs.Evalis.Formulas
{
    /// <summary>
    /// Represents the IFS formula which checks whether one or more conditions are met, and returns a value that corresponds to the first TRUE condition.
    /// </summary>
    internal class IfsFormula : Formula
    {
        /// <summary>
        /// Initializes a new IFS formula.
        /// </summary>
        public IfsFormula() : base("IFS") { }

        /// <inheritdoc/>
        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            // Expected pairs: cond1, val1, cond2, val2...
            object[] args = context.GetFlattenedArgs<object>().ToArray();

            if (args.Length % 2 != 0)
            {
                return EvaluationResult.WithError(Error.Value("IFS formula must have an even number of arguments (condition/value pairs)."));
            }

            for (int i = 0; i < args.Length; i += 2)
            {
                object conditionObj = args[i];
                if (conditionObj is bool condition && condition)
                {
                    return EvaluationResult.WithValue(args[i + 1]);
                }
            }

            // No conditions matched
            return EvaluationResult.WithError(Error.Value("No match found in IFS expression."));
        }

        /// <inheritdoc/>
        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Checks whether one or more conditions are met, and returns a value that corresponds to the first TRUE condition."
            };
            info.AddArgument(new ObjectArgument("args", true, isVariadic: true) { Description = "Condition and value pairs." });
            return info;
        }
    }
}