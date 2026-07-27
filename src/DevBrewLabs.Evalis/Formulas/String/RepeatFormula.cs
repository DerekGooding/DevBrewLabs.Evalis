using System.Text;

namespace DevBrewLabs.Evalis.Formulas
{
    /// <summary>
    /// Represents the REPEAT formula which repeats text a given number of times.
    /// </summary>
    internal class RepeatFormula : Formula
    {
        /// <summary>
        /// Initializes a new REPEAT formula.
        /// </summary>
        public RepeatFormula() : base("REPEAT") { }

        /// <inheritdoc/>
        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            string text = context.GetStringArg(0);
            int count = (int)context.GetDoubleArg(1);

            if (string.IsNullOrEmpty(text) || count <= 0)
                return EvaluationResult.WithValue(string.Empty);

            StringBuilder sb = new StringBuilder(text.Length * count);
            for (int i = 0; i < count; i++)
                sb.Append(text);

            return EvaluationResult.WithValue(sb.ToString());
        }

        /// <inheritdoc/>
        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Repeats text a given number of times."
            };
            info.AddArgument(new StringArgument("text", true) { Description = "The text to repeat." });
            info.AddArgument(new DoubleArgument("number_times", true) { Description = "The number of times to repeat the text." });
            return info;
        }
    }
}